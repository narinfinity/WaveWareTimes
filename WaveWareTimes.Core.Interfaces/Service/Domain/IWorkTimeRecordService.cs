using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using WaveWareTimes.Core.Entities.Domain;

namespace WaveWareTimes.Core.Interfaces.Service.Domain
{
    public interface IWorkTimeRecordService
    {
        void Dispose();
        IEnumerable<WorkTimeRecord> GetListForUser(bool userIsInAdminRole, string userName);
        WorkTimeRecord Get(int id);
        WorkTimeRecord GetWithUser(int id);
        void Add(WorkTimeRecord workTimeRecord);
        void Update(WorkTimeRecord workTimeRecord);
        void Delete(int id);        
    }
}
