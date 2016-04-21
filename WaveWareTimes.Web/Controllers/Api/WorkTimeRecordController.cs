using System.Collections.Generic;
using Microsoft.AspNet.Identity.Owin;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using WaveWareTimes.Web.Models.Api;
using WaveWareTimes.Core.Interfaces.Service.Domain;
using System.Linq;
using System.Threading.Tasks;
using WaveWareTimes.Core.Entities.Domain;
using System.Web.Http.ModelBinding;

namespace WaveWareTimes.Web.Controllers.Api
{
    [Authorize]
    [RoutePrefix("api/WorkTimeRecord")]
    public class WorkTimeRecordController : ApiController
    {
        private IWorkTimeRecordService workTimeRecordService;
        private UserManager userManager;

        public WorkTimeRecordController(UserManager userManager, IWorkTimeRecordService workTimeRecordService)
        {
            this.userManager = userManager;
            this.workTimeRecordService = workTimeRecordService;
        }
        protected override void Dispose(bool disposing)
        {
            //Clean up managable resources
            if (disposing)
            {
                if (userManager != null)
                {
                    userManager.Dispose();
                    userManager = null;
                }
                if (workTimeRecordService != null)
                {
                    workTimeRecordService.Dispose();
                    workTimeRecordService = null;
                }
            }
            //Clean up unmanagable resources
            //
            base.Dispose(disposing);
        }
        ~WorkTimeRecordController()
        {
            Dispose(false);
        }

        // GET api/WorkTimeRecord
        //[Route("All")]
        public IEnumerable<WorkTimeRecordViewModel> Get()
        {
            var userName = User.Identity.Name;
            var userIsInAdminRole = User.IsInRole("Admin");
            return workTimeRecordService.GetListForUser(userIsInAdminRole, userName).Select(e => new WorkTimeRecordViewModel
            {
                Id = e.Id,
                Start = e.Start,
                End = e.End,
                Description = e.Description,
                UserName = string.Format("{0} {1}", e.User.FirstName, e.User.LastName)
            });
        }

        // GET api/WorkTimeRecord/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/WorkTimeRecord
        public IEnumerable<string> Post([FromBody]WorkTimeRecordViewModel model)
        {
            var currentUser = userManager.Users.FirstOrDefault(e => e.UserName == User.Identity.Name);
            if (ModelState.IsValid && currentUser != null)
            {
                if (model.Id > 0)
                {
                    var record = workTimeRecordService.Get(model.Id);
                    
                    record.Id = model.Id;
                    record.Description = model.Description;
                    record.Start = model.Start;
                    record.End = model.End;
                    if (record.UserId == currentUser.Id) record.UserId = currentUser.Id;
                    workTimeRecordService.Update(record);
                }
                else
                {
                    workTimeRecordService.Add(new WorkTimeRecord
                    {
                        Description = model.Description,
                        Start = model.Start,
                        End = model.End,
                        UserId = currentUser.Id
                    });
                }

            }
            return ModelState.SelectMany(e => e.Value.Errors.Select(m => m.ErrorMessage));
        }

        // DELETE api/WorkTimeRecord/5
        public void Delete(int id)
        {
            var record = workTimeRecordService.GetWithUser(id);
            if (record != null && record.User.UserName == User.Identity.Name)
                workTimeRecordService.Delete(id);
        }
    }
}