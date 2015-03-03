using GeoCV.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace GeoCV.Controllers
{
    public class ProController : Controller
    {
        private cvEntities1 db = new cvEntities1();

        // GET: Work
        public ActionResult Index()
        {
            string UserId = User.Identity.GetUserId();

            var Item = from a in db.CVVersjon
                       where a.AspNetUserId.Equals(UserId)
                       select a;

            return View(Item.FirstOrDefault());
        }

        [HttpGet]
        public ActionResult GetProjects()
        {
            var Item = from a in db.Prosjekt
                       select a.Navn;

            return Json(Item, JsonRequestBehavior.AllowGet);
        }
    }
}