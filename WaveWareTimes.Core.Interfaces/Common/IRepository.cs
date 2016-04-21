using System;
using WaveWareTimes.Core.Entities;

namespace WaveWareTimes.Core.Interfaces.Common
{
    public interface IRepository<TEntity, TKey> : IReader<TEntity, TKey>, IFilteredOrderedPagedReader<TEntity, TKey>
    where TEntity : class, IEntityIdentity<TKey>
    {
        TEntity Create(TEntity entity = null);
        void Update(TEntity entity);
        void Delete(TKey id);
        void Save();
    }
}
