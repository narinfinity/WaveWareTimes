using System.Linq;
using WaveWareTimes.Core.Entities;

namespace WaveWareTimes.Core.Interfaces.Common
{
    public interface IReader<TEntity, TKey>
       where TEntity : class, IEntityIdentity<TKey>
    {
        TEntity Get(TKey id, bool tracking = false);
        IQueryable<TEntity> GetList();
    }
}
