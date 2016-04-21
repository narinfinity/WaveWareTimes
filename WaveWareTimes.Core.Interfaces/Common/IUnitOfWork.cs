using System;
using WaveWareTimes.Core.Entities;
using WaveWareTimes.Core.Interfaces.Repository;

namespace WaveWareTimes.Core.Interfaces.Common
{
    public interface IUnitOfWork
    {
        void Dispose();        
        void Save();
        IRepository<TEntity, TKey> Repository<TEntity, TKey>() where TEntity : class, IEntityIdentity<TKey>;
        IWorkTimeRecordRepository WorkTimeRecordRepository { get; }
    }
}
