using GeoCV.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;

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