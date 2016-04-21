using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using WaveWareTimes.Core.Entities;
using WaveWareTimes.Core.Interfaces.Common;
using WaveWareTimes.Core.Interfaces.Repository;
using WaveWareTimes.Core.Interfaces.Service.Domain;
using WaveWareTimes.Infrastructure.Data.Common;
using WaveWareTimes.Infrastructure.Data.Repository;

namespace WaveWareTimes.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private IDataContext context;
        private IDictionary<string, object> baseRepositories;
        private IDictionary<string, object> repositories;

        public UnitOfWork(IDataContext context)
        {
            this.context = context;
            baseRepositories = new Dictionary<string, object>();
            repositories = new Dictionary<string, object>();
        }

        public void Save()
        {
            context.Save();
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
                if (baseRepositories != null)
                {
                    foreach (var key in baseRepositories.Keys)
                        if (baseRepositories[key] != null)                        
                            ((IDisposable)baseRepositories[key]).Dispose();  
                    baseRepositories = null;
                }

                if (repositories != null)
                {
                    foreach (var key in repositories.Keys)
                        if (repositories[key] != null)                        
                            ((IDisposable)repositories[key]).Dispose();
                    repositories = null;
                }
                if (context != null)
                {
                    context = null;
                }
            }
            //Clean up unmanagable resources

        }

        public IRepository<TEntity, TKey> Repository<TEntity, TKey>() where TEntity : class, IEntityIdentity<TKey>
        {
            var key = typeof(TEntity).Name;
            if (!baseRepositories.ContainsKey(key)) baseRepositories.Add(key, new Repository<TEntity, TKey>(context));
            return (IRepository<TEntity, TKey>)baseRepositories[key];
        }

        public IWorkTimeRecordRepository WorkTimeRecordRepository
        {
            get
            {
                var key = typeof(IWorkTimeRecordRepository).Name;
                if (!repositories.ContainsKey(key)) repositories.Add(key, new WorkTimeRecordRepository(context));
                return (IWorkTimeRecordRepository)repositories[key];
            }
        }




    }
}
