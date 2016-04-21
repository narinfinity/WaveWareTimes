using WaveWareTimes.Core.Entities;
using WaveWareTimes.Core.Interfaces.Common;
using WaveWareTimes.Infrastructure.Data.Repository;

namespace WaveWareTimes.Infrastructure.Data.Common
{
    public class Repository<TEntity, TKey> : BaseRepository<TEntity, TKey, DataContext>
       where TEntity : class, IEntityIdentity<TKey>
    {
        public Repository(IDataContext context) : base((DataContext)context) {}
        
        protected override void Dispose(bool disposing)
        {
            
            //Clean up managable resources
            if (disposing)
            {
                
            }
            //Clean up unmanagable resources
            
            base.Dispose(disposing);
        }
        ~Repository()
        {
            Dispose(false);
        }
    }
}
