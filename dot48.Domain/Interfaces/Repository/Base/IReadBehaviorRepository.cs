using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System;

namespace dot48.Domain.Interfaces.Repository.Base
{
    public interface IReadBehaviorRepository<TEntity, TKey>
        where TEntity : IEntityBase<TKey>
        where TKey : struct
    {
       TEntity GetByKey(TKey key);
        TEntity GetByPredicate(Expression<Func<TEntity, bool>> predicate, params string[] includeProperties);
        ICollection<TEntity> GetAll(params string[] includeProperties);
        ICollection<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate, params string[] includeProperties);
        ICollection<TEntity> GetAll(int skip, int take, out int total);
        ICollection<TEntity> GetAll(int skip, int take, out int total, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy);
        ICollection<TEntity> Find(int skip, int take, out int total, params string[] includeProperties);
        ICollection<TEntity> Find(Expression<Func<TEntity, bool>> expression, int skip, int take, out int total);
        ICollection<TEntity> Find(
            Expression<Func<TEntity, bool>> expression,
            int skip,
            int take,
            out int total,
            params string[] includeProperties);
        ICollection<TEntity> Find(
            Expression<Func<TEntity, bool>> expression,
            int skip,
            int take,
            out int total,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy,
            params string[] includeProperties);
    }
}
