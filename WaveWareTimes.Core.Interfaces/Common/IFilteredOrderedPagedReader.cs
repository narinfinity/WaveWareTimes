using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using WaveWareTimes.Core.Entities;

namespace WaveWareTimes.Core.Interfaces.Common
{
    public interface IFilteredOrderedPagedReader<TEntity, TKey>
       where TEntity : class, IEntityIdentity<TKey>
    {
        IQueryable<TEntity> GetList(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            List<Expression<Func<TEntity, object>>> includeProperties = null,
            int? page = null,
            int? pageSize = null);
    }
}
