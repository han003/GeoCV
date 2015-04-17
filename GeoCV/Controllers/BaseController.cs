using GeoCV.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace GeoCV.Controllers
{
    public class BaseController : Controller
    {
        public cvEntities db = new cvEntities();

        public CVVersjon GetUserCV()
        {
            string UserId = (Session["ShadowUser"] != null) ? Session["ShadowUser"].ToString() : User.Identity.GetUserId();

            var Data = from a in db.CVVersjon
                       where a.AspNetUserId.Equals(UserId)
                       select a;

            return Data.FirstOrDefault();
        }
    }
}