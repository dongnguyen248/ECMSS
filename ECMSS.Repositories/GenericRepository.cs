using ECMSS.Data;
using ECMSS.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace ECMSS.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly ECMEntities _dbContext;
        private readonly DbSet<TEntity> _dbSet;

        public GenericRepository(ECMEntities dbContext)
        {
            _dbContext = dbContext;
            _dbContext.Configuration.LazyLoadingEnabled = false;
            _dbContext.Configuration.AutoDetectChangesEnabled = false;
            _dbSet = _dbContext.Set<TEntity>();
        }

        public TEntity Add(TEntity entity)
        {
            return _dbSet.Add(entity);
        }

        public TEntity Delete(object id)
        {
            TEntity entity = _dbSet.Find(id);
            return _dbSet.Remove(entity);
        }

        public TEntity Delete(TEntity entity)
        {
            if (_dbContext.Entry(entity).State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }
            return _dbSet.Remove(entity);
        }

        public void Update(TEntity entity)
        {
            _dbSet.Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
        }

        public void Update(TEntity entity, params Expression<Func<TEntity, object>>[] properties)
        {
            _dbSet.Attach(entity);
            var dbEntry = _dbContext.Entry(entity);
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
            var query = _dbSet.AsQueryable();
            return includes.Aggregate(query, (current, includeProperty) => current.Include(includeProperty)).AsNoTracking();
        }

        public IEnumerable<TEntity> GetMany(Expression<Func<TEntity, bool>> condition)
        {
            return _dbSet.Where(condition).AsNoTracking();
        }

        public IEnumerable<TEntity> GetMany(Expression<Func<TEntity, bool>> condition, params Expression<Func<TEntity, object>>[] includes)
        {
            var query = _dbSet.Where(condition);
            return includes.Aggregate(query, (current, includeProperty) => current.Include(includeProperty)).AsNoTracking();
        }

        public IEnumerable<TEntity> GetRandom(Expression<Func<TEntity, bool>> condition, int rows)
        {
            return _dbSet.Where(condition).OrderBy(r => Guid.NewGuid()).Take(rows).AsNoTracking();
        }

        public IEnumerable<TEntity> GetRandom(Expression<Func<TEntity, bool>> condition, int rows, params Expression<Func<TEntity, object>>[] includes)
        {
            var query = _dbSet.Where(condition).OrderBy(r => Guid.NewGuid()).Take(rows);
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
            var query = _dbSet.Where(condition);
            return includes.Aggregate(query, (current, includeProperty) => current.Include(includeProperty)).AsNoTracking().FirstOrDefault();
        }

        public IEnumerable<TEntity> ExecuteQuery(string query, params object[] parameters)
        {
            return _dbSet.SqlQuery(query, parameters).AsNoTracking();
        }
    }
}