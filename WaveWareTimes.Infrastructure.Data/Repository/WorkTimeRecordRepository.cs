using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using WaveWareTimes.Core.Entities.Domain;
using WaveWareTimes.Core.Interfaces.Common;
using WaveWareTimes.Core.Interfaces.Repository;
using WaveWareTimes.Infrastructure.Data.Common;

namespace WaveWareTimes.Infrastructure.Data.Repository
{
    public class WorkTimeRecordRepository : Repository<WorkTimeRecord, int>, IWorkTimeRecordRepository
    {
        public WorkTimeRecordRepository(IDataContext context) : base(context) { }

        protected override void Dispose(bool disposing)
        {
            //Clean up managable resources
            if (disposing)
            {
                
            }
            //Clean up unmanagable resources

            base.Dispose(disposing);
        }
        ~WorkTimeRecordRepository()
        {
            Dispose(false);
        }

        public IQueryable<WorkTimeRecord> GetListWithUser(Expression<Func<WorkTimeRecord, bool>> predicate = null)
        {
            var includeProperties = new List<Expression<Func<WorkTimeRecord, object>>> { e => e.User };
            return base.GetList(predicate, null, includeProperties);
        }


    }
}
