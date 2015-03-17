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
            var Item = from a in db.ProgrammeringsspråkListe
                       orderby a.Programmeringsspråk ascending
                       select a.Programmeringsspråk;

            return Json(Item, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetFrameworks()
        {
            var Item = from a in db.RammeverkListe
                       orderby a.Rammeverk ascending
                       select a.Rammeverk;

            return Json(Item, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetWebTechnologies()
        {
            var Item = from a in db.WebTeknologiListe
                       orderby a.WebTeknologi ascending
                       select a.WebTeknologi;

            return Json(Item, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetDatabaseSystems()
        {
            var Item = from a in db.DatabasesystemListe
                       orderby a.Databasesystem ascending
                       select a.Databasesystem;

            return Json(Item, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetServerside()
        {
            var Item = from a in db.ServersideListe
                       orderby a.Serverside ascending
                       select a.Serverside;

            return Json(Item, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetOperatingSystems()
        {
            var Item = from a in db.OperativsystemListe
                       orderby a.Operativsystem ascending
                       select a.Operativsystem;

            return Json(Item, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public void InsertItem(string Insert, string Value)
        {
            if (Insert == "Programmeringsspråk")
            {
                ProgrammeringsspråkListe NewItem = new ProgrammeringsspråkListe();
                NewItem.Programmeringsspråk = Value;
                db.ProgrammeringsspråkListe.Add(NewItem);
            }
            else if (Insert == "Rammeverk")
            {
                RammeverkListe NewItem = new RammeverkListe();
                NewItem.Rammeverk = Value;
                db.RammeverkListe.Add(NewItem);
            }
            else if (Insert == "WebTeknologier")
            {
                WebTeknologiListe NewItem = new WebTeknologiListe();
                NewItem.WebTeknologi = Value;
                db.WebTeknologiListe.Add(NewItem);
            }
            else if (Insert == "Databasesystemer")
            {
                DatabasesystemListe NewItem = new DatabasesystemListe();
                NewItem.Databasesystem = Value;
                db.DatabasesystemListe.Add(NewItem);
            }
            else if (Insert == "Serverside")
            {
                ServersideListe NewItem = new ServersideListe();
                NewItem.Serverside = Value;
                db.ServersideListe.Add(NewItem);
            }
            else if (Insert == "Operativsystemer")
            {
                OperativsystemListe NewItem = new OperativsystemListe();
                NewItem.Operativsystem = Value;
                db.OperativsystemListe.Add(NewItem);
            }

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