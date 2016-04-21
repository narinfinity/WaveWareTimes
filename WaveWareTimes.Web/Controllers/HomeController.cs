using System.Web.Mvc;

namespace WaveWareTimes.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        //IWorkTimeRecordService workTimeRecordService;
        public HomeController()//IWorkTimeRecordService workTimeRecordService)
        {
            //this.workTimeRecordService = workTimeRecordService;
        }
        protected override void Dispose(bool disposing)
        {
            //Clean up managable resources
            if (disposing)
            {
                //if (workTimeRecordService != null)
                //{
                //    workTimeRecordService.Dispose();
                //    workTimeRecordService = null;
                //}
            }
            //Clean up unmanagable resources

            base.Dispose(disposing);
        }
        ~HomeController()
        {
            Dispose(false);
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}
