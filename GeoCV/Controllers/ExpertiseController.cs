using GeoCV.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace GeoCV.Controllers
{
    [Authorize]
    public class ExpertiseController : Controller
    {
        private cvEntities db = new cvEntities();

        // GET: Expertise
        public ActionResult Index()
        {
            string UserId = User.Identity.GetUserId();

            var Item = from a in db.CVVersjon
                       where a.AspNetUserId.Equals(UserId)
                       select a;

            return View(Item.FirstOrDefault());
        }


        [HttpGet]
        public ActionResult GetProgrammingLanguages()
        {
            var Item = from a in db.ListeKatalog
                       where a.Katalog == "ProgrammeringsSpråk"
                       orderby a.Element ascending
                       select a.Element;

            return Json(Item, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetFrameworks()
        {
            var Item = from a in db.ListeKatalog
                       where a.Katalog == "Rammeverk"
                       orderby a.Element ascending
                       select a.Element;

            return Json(Item, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetWebTechnologies()
        {
            var Item = from a in db.ListeKatalog
                       where a.Katalog == "Webteknologi"
                       orderby a.Element ascending
                       select a.Element;

            return Json(Item, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetDatabaseSystems()
        {
            var Item = from a in db.ListeKatalog
                       where a.Katalog == "Databasesystem"
                       orderby a.Element ascending
                       select a.Element;

            return Json(Item, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetServerside()
        {
            var Item = from a in db.ListeKatalog
                       where a.Katalog == "Serverside"
                       orderby a.Element ascending
                       select a.Element;

            return Json(Item, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetOperatingSystems()
        {
            var Item = from a in db.ListeKatalog
                       where a.Katalog == "Operativsystem"
                       orderby a.Element ascending
                       select a.Element;

            return Json(Item, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public void InsertItem(string Insert, string Value)
        {
            ListeKatalog NewItem = new ListeKatalog();
            NewItem.Katalog = Insert;
            NewItem.Element = Value;
            db.ListeKatalog.Add(NewItem);

            db.SaveChanges();
        }

        [HttpPost]
        public void Update(string Update, string Value)
        {

            string UserId = User.Identity.GetUserId();

            var Item = from a in db.CVVersjon
                       where a.AspNetUserId.Equals(UserId)
                       select a;

            CVVersjon Cv = Item.FirstOrDefault();

            switch (Update)
            {
                case "Programmeringsspråk":
                    Cv.Kompetanse.Programmeringsspråk = Value;
                    break;

                case "Rammeverk":
                    Cv.Kompetanse.Rammeverk = Value;
                    break;

                case "WebTeknologier":
                    Cv.Kompetanse.WebTeknologier = Value;
                    break;

                case "Databasesystemer":
                    Cv.Kompetanse.Databasesystemer = Value;
                    break;

                case "Serverside":
                    Cv.Kompetanse.Serverside = Value;
                    break;

                case "Operativsystemer":
                    Cv.Kompetanse.Operativsystemer = Value;
                    break;
            }


            db.SaveChanges();
        }

    }
}