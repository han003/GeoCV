using System.Web.Mvc;

namespace GeoCV.Controllers
{

    [Authorize]
    public class DashboardController : BaseController
    {
        public ActionResult Index()
        {
            return View(GetBrukerCv(GetAspNetBrukerID()));
        }



        public ActionResult EndSession()
        {
            Session.Abandon();

            return RedirectToAction("Index", "Dashboard");
        }

    }
}