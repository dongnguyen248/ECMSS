﻿using ECMSS.Data;
using ECMSS.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;

namespace ECMSS.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly DbSet<TEntity> _dbSet;
        private readonly IDbFactory _dbFactory;
        private ECMEntities _dbContext;
        protected ECMEntities DbContext => _dbContext ?? (_dbContext = _dbFactory.Init());

        public GenericRepository(IDbFactory dbFactory)
        {
            _dbFactory = dbFactory;
            _dbSet = DbContext.Set<TEntity>();
            DbContext.Configuration.LazyLoadingEnabled = false;
            DbContext.Configuration.AutoDetectChangesEnabled = false;
        }

        public TEntity Add(TEntity entity)
        {
            return _dbSet.Add(entity);
        }

        public IEnumerable<TEntity> AddRange(IEnumerable<TEntity> entities)
        {
            return _dbSet.AddRange(entities);
        }

        public TEntity Remove(object id)
        {
            TEntity entity = _dbSet.Find(id);
            return _dbSet.Remove(entity);
        }

        public TEntity Remove(TEntity entity)
        {
            if (_dbContext.Entry(entity).State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }
            return _dbSet.Remove(entity);
        }

        public IEnumerable<TEntity> RemoveRange(IEnumerable<TEntity> entities)
        {
            foreach (TEntity entity in entities)
            {
                if (_dbContext.Entry(entity).State == EntityState.Detached)
                {
                    _dbSet.Attach(entity);
                }
            }
            return _dbSet.RemoveRange(entities);
        }

        public void RemoveMulti(Expression<Func<TEntity, bool>> condition)
        {
            IEnumerable<TEntity> objects = _dbSet.Where<TEntity>(condition).AsEnumerable();
            foreach (TEntity obj in objects)
            {
                _dbSet.Remove(obj);
            }
        }

        public void Update(TEntity entity)
        {
            _dbSet.Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
        }

        public void Update(TEntity entity, params Expression<Func<TEntity, object>>[] properties)
        {
            _dbSet.Attach(entity);
            DbEntityEntry<TEntity> dbEntry = _dbContext.Entry(entity);
            foreach (var includeProperty in properties)
            {
                dbEntry.Property(includeProperty).IsModified = true;
            }
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _dbSet.AsNoTracking();
        }

        public IEnumerable<TEntity> GetAll(params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = _dbSet.AsQueryable();
            return includes.Aggregate(query, (current, includeProperty) => current.Include(includeProperty)).AsNoTracking();
        }

        public IEnumerable<TEntity> GetMany(Expression<Func<TEntity, bool>> condition)
        {
            return _dbSet.Where(condition).AsNoTracking();
        }

        public IEnumerable<TEntity> GetMany(Expression<Func<TEntity, bool>> condition, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = _dbSet.Where(condition);
            return includes.Aggregate(query, (current, includeProperty) => current.Include(includeProperty)).AsNoTracking();
        }

        public IEnumerable<TEntity> GetRandom(Expression<Func<TEntity, bool>> condition, int rows)
        {
            return _dbSet.Where(condition).OrderBy(r => Guid.NewGuid()).Take(rows).AsNoTracking();
        }

        public IEnumerable<TEntity> GetRandom(Expression<Func<TEntity, bool>> condition, int rows, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = _dbSet.Where(condition).OrderBy(r => Guid.NewGuid()).Take(rows);
            return includes.Aggregate(query, (current, includeProperty) => current.Include(includeProperty)).AsNoTracking();
        }

        public TEntity GetSingleById(object id)
        {
            return _dbSet.Find(id);
        }

        public TEntity GetSingle(Expression<Func<TEntity, bool>> condition)
        {
            return _dbSet.Where(condition).AsNoTracking().FirstOrDefault();
        }

        public TEntity GetSingle(Expression<Func<TEntity, bool>> condition, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = _dbSet.Where(condition);
            return includes.Aggregate(query, (current, includeProperty) => current.Include(includeProperty)).AsNoTracking().FirstOrDefault();
        }

        public IEnumerable<TEntity> ExecuteQuery(string query, params object[] parameters)
        {
            return _dbSet.SqlQuery(query, parameters).AsNoTracking();
        }

        public bool CheckContains(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.Any(predicate);
        }

        public IEnumerable<TEntity> Find(Func<TEntity, bool> condition, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = _dbSet.Include(includes[0]);
            foreach (var include in includes.Skip(1))
            {
                query = query.Include(include);
            }
            return query.Where(condition).AsQueryable();
        }
    }
}