using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using WaveWareTimes.Core.Entities;
using WaveWareTimes.Core.Interfaces.Common;

namespace WaveWareTimes.Infrastructure.Data.Repository
{
    public abstract class BaseRepository<TEntity, TKey, TContext> : IRepository<TEntity, TKey>, IDisposable
     where TEntity : class, IEntityIdentity<TKey>
     where TContext : System.Data.Entity.DbContext, IDataContext

    {
        protected TContext context;

        protected BaseRepository(TContext context) { this.context = context; }

        //Method implementations
        public IQueryable<TEntity> GetList()
        {
            return context.GetSet<TEntity>();
        }
        public IQueryable<TEntity> GetList(Expression<Func<TEntity, bool>> filter = null,
                    Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                    List<Expression<Func<TEntity, object>>> includeProperties = null, int? page = null, int? pageSize = null)
        {
            IQueryable<TEntity> query = this.context.GetSet<TEntity>();

            includeProperties?.ForEach(i => { query = query.Include(i); });

            if (filter != null)
                query = query.Where(filter);

            if (orderBy != null)
                query = orderBy(query);

            if (page != null && pageSize != null)
                query = query
                    .Skip((page.Value - 1) * pageSize.Value)
                    .Take(pageSize.Value);

            return query;
        }

        public TEntity Get(TKey id, bool tracking = false)
        {
            var entity = context.Set<TEntity>().Find(id);
            if (entity != null && !tracking) context.Detach(entity);
            return entity;
        }

        public void Update(TEntity entity)
        {
            context.Attach<TEntity>(entity);
        }

        public virtual TEntity Create(TEntity entity = null)
        {
            return context.Create(entity);
        }

        public void Save()
        {
            context.Save();
        }

        public void Delete(TKey id)
        {
            var entity = this.Get(id, true);
            context.Delete(entity);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            //Clean up managable resources
            if (disposing)
            {
                if (context != null)
                {
                    context.Dispose();
                    context = null;
                }
            }
            //Clean up unmanagable resources

        }

    }


}
