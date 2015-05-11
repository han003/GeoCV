using GeoCV.Models;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Mvc;

namespace GeoCV.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
        public CvPortalEntities db = new CvPortalEntities();

        public string GetAspNetBrukerID()
        {
            return (Session["ShadowUser"] != null) ? Session["ShadowUser"].ToString() : User.Identity.GetUserId();
        }

        public CVVersjon GetBrukerCv(string AspNetId)
        {
            var Data = from a in db.CVVersjon
                       where a.AspNetUserId.Equals(AspNetId)
                       select a;

            return Data.FirstOrDefault();
        }
    }
}