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
        public cvEntities db = new cvEntities();

        public string GetAspNetUserID()
        {
            return (Session["ShadowUser"] != null) ? Session["ShadowUser"].ToString() : User.Identity.GetUserId();
        }

        public CVVersjon GetUserCV()
        {
            string UserId = GetAspNetUserID();

            var Data = from a in db.CVVersjon
                       where a.AspNetUserId.Equals(UserId)
                       select a;

            return Data.FirstOrDefault();
        }

    }
}