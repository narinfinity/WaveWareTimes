using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WaveWareTimes.Core.Entities.Domain;
using WaveWareTimes.Core.Interfaces.Common;
using WaveWareTimes.Core.Interfaces.Service.Domain;

namespace WaveWareTimes.Service.Domain
{
    public class WorkTimeRecordService : IWorkTimeRecordService, IDisposable
    {
        IUnitOfWork unitOfWork;
        public WorkTimeRecordService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
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
                if (unitOfWork != null)
                {
                    unitOfWork.Dispose();
                    unitOfWork = null;
                }
            }
            //Clean up unmanagable resources

        }

        public WorkTimeRecord Get(int id)
        {
            return unitOfWork.WorkTimeRecordRepository.Get(id, true);            
        }
        public void Add(WorkTimeRecord workTimeRecord)
        {
            unitOfWork.WorkTimeRecordRepository.Create(workTimeRecord);
            unitOfWork.Save();
        }
        public void Update(WorkTimeRecord workTimeRecord)
        {
            unitOfWork.WorkTimeRecordRepository.Update(workTimeRecord);
            unitOfWork.Save();
        }
        public void Delete(int id)
        {
            unitOfWork.WorkTimeRecordRepository.Delete(id);
            unitOfWork.Save();
        }
        public WorkTimeRecord GetWithUser(int id)
        {
            var includeProperties = new List<Expression<Func<WorkTimeRecord, object>>> { e => e.User };
            return unitOfWork.WorkTimeRecordRepository.GetList(
                e => e.Id == id,
                o => o.OrderBy(e => e.Start),
                includeProperties).FirstOrDefault();
        }

        public IEnumerable<WorkTimeRecord> GetListForUser(bool userIsInAdminRole, string userName)
        {
            //return unitOfWork.WorkTimeRecordRepository.GetListWithUser(e => userIsInAdminRole || e.User.UserName == userName);
            var includeProperties = new List<Expression<Func<WorkTimeRecord, object>>> { e => e.User };
            return unitOfWork.WorkTimeRecordRepository.GetList(
                e => userIsInAdminRole || e.User.UserName == userName, 
                o => o.OrderBy(e => e.Start), 
                includeProperties);
        }        

    }

}
