﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ECMSS.Repositories.Interfaces
{
    public interface IGenericRepository<TEntity>
    {
        TEntity Add(TEntity entity);

        IEnumerable<TEntity> AddRange(IEnumerable<TEntity> entities);

        TEntity Remove(object id);

        TEntity Remove(TEntity entity);

        IEnumerable<TEntity> RemoveRange(IEnumerable<TEntity> entities);

        void RemoveMulti(Expression<Func<TEntity, bool>> condition);

        void Update(TEntity entity);

        void Update(TEntity entity, params Expression<Func<TEntity, object>>[] properties);

        IEnumerable<TEntity> GetAll();

        IEnumerable<TEntity> GetAll(params Expression<Func<TEntity, object>>[] includes);

        IEnumerable<TEntity> GetMany(Expression<Func<TEntity, bool>> condition);

        IQueryable<TEntity> GetMany(Expression<Func<TEntity, bool>> condition, params Expression<Func<TEntity, object>>[] includes);

        IQueryable<TEntity> Find(Func<TEntity, bool> condition, params Expression<Func<TEntity, object>>[] includes);

        TEntity GetSingleById(object id);

        TEntity GetSingle(Expression<Func<TEntity, bool>> condition);

        TEntity GetSingle(Expression<Func<TEntity, bool>> condition, params Expression<Func<TEntity, object>>[] includes);

        IEnumerable<TEntity> ExecuteQuery(string query, params object[] parameters);

        bool CheckContains(Expression<Func<TEntity, bool>> predicate);
    }
}