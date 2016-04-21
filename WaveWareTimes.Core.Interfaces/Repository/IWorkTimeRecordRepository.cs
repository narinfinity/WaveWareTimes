using System;
using System.Linq;
using System.Linq.Expressions;
using WaveWareTimes.Core.Entities.Domain;
using WaveWareTimes.Core.Interfaces.Common;

namespace WaveWareTimes.Core.Interfaces.Repository
{
    public interface IWorkTimeRecordRepository : IRepository<WorkTimeRecord, int>
    {        
        IQueryable<WorkTimeRecord> GetListWithUser(Expression<Func<WorkTimeRecord, bool>> predicate = null);
    }
}
