using dot48.Domain.Interfaces;
using dot48.Domain.Interfaces.Repository.Base;
using dot48.Infra.Persistency.Contexts;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace dot48.Infra.Persistency.Repositories
{
    public class BaseRepository<TEntity, TKey> : IBaseRepository<TEntity, TKey>, IDisposable
       where TEntity : class, IEntityBase<TKey>
       where TKey : struct
    {
        private bool disposedValue;
        public BaseRepository(BaseContext context)
        {
            this.Context = context;
        }

        protected BaseContext Context { get; }
        public void Delete(TEntity entity)
        {
            this.Context.Set<TEntity>().Remove(entity);
        }

        public void Dispose()
        {
            this.Dispose(true);

            GC.SuppressFinalize(this);
        }
        public ICollection<TEntity> Find(int skip, int take, out int total, params string[] includeProperties)
        {
            IQueryable<TEntity> query = this.Context.Set<TEntity>();

            if (includeProperties != null && includeProperties.Length > 0)
            {
                query = this.IncludePropertiesInQuery(query, includeProperties);
            }

            total = query.Count();

            return query.OrderBy(e => e.Id).Skip(skip).Take(take).ToList();
        }
        public ICollection<TEntity> Find(Expression<Func<TEntity, bool>> expression, int skip, int take, out int total)
        {
            IQueryable<TEntity> query = this.Context.Set<TEntity>();

            total = query.Count();

            return query.OrderBy(e => e.Id).Skip(skip).Take(take).ToList();
        }
        public ICollection<TEntity> Find(Expression<Func<TEntity, bool>> expression, int skip, int take, out int total, params string[] includeProperties)
        {
            return this.Find(expression, skip, take, out total, null, includeProperties);
        }
        public ICollection<TEntity> Find(
            Expression<Func<TEntity, bool>> expression,
            int skip,
            int take,
            out int total,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy,
            params string[] includeProperties)
        {
            var query = this.Context.Set<TEntity>().Where(expression);

            total = query.Count();

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }

            query = query.Skip(skip).Take(take);

            if (includeProperties != null && includeProperties.Length > 0)
            {
                foreach (var p in includeProperties)
                {
                    query = query.Include(p);
                }
            }

            return query.ToList();
        }
        public ICollection<TEntity> GetAll(int skip, int take, out int total)
        {
            return this.GetAll(skip, take, out total, null);
        }
        public ICollection<TEntity> GetAll(int skip, int take, out int total, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy)
        {
            IQueryable<TEntity> entities = this.Context.Set<TEntity>();

            total = entities.Count();

            entities = entities.Skip(skip).Take(take);

            if (orderBy != null)
            {
                return orderBy(entities).ToList();
            }

            return entities.ToList();
        }
        public ICollection<TEntity> GetAll(params string[] includeProperties)
        {
            return this.GetAll(null, includeProperties);
        }
        public ICollection<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate, params string[] includeProperties)
        {
            var query = this.CreateQuery();

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (includeProperties != null && includeProperties.Length > 0)
            {
                query = this.IncludePropertiesInQuery(query, includeProperties);
            }

            return query.ToList();
        }

        public TEntity GetByKey(TKey key)
        {
            return this.Context.Set<TEntity>().Find(key);
        }
        public TEntity GetByPredicate(Expression<Func<TEntity, bool>> predicate, params string[] includeProperties)
        {
            var query = this.CreateQuery();

            if (includeProperties != null && includeProperties.Length > 0)
            {
                query = this.IncludePropertiesInQuery(query, includeProperties);
            }

            return query.FirstOrDefault(predicate);
        }
        
        public TEntity Include(TEntity entity)
        {
            return this.Context.Set<TEntity>().Add(entity);
        }

        public TEntity SaveOrUpdate(TEntity entity)
        {
            if (entity.Id.Equals(default(TKey)))
            {
                return this.Include(entity);
            }

            return this.Update(entity);
        }
        public TEntity Update(TEntity entity)
        {
            var entry = this.Context.Entry(entity);

            entry.State = EntityState.Modified;

            return entry.Entity;
        }
        protected IQueryable<TEntity> CreateQuery()
        {
            return this.Context.Set<TEntity>();
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (disposing)
                {
                    this.Context.Dispose();
                }

                this.disposedValue = true;
            }
        }
        protected IQueryable<TEntity> IncludePropertiesInQuery(IQueryable<TEntity> query, string[] includeProperties)
        {
            return includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        }
    }
}
