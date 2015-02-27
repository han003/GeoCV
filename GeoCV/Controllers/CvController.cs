using GeoCV.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace CV.Controllers
{

    [Authorize]
    public class CvController : Controller
    {

        private cvEntities1 db = new cvEntities1();

        public ActionResult Index()
        {
            string UserId = User.Identity.GetUserId();

            var Item = from a in db.CVVersjon
                       where a.AspNetUserId.Equals(UserId)
                       select a;

            return View(Item.FirstOrDefault());
        }

        public void AddUtdannelse(string Beskrivelse, short Fra, short Til)
        {
            string UserId = User.Identity.GetUserId();

            var Item = from a in db.CVVersjon
                       where a.AspNetUserId.Equals(UserId)
                       select a;

            CVVersjon Cv = Item.FirstOrDefault();

            // Utdannelse variables
            Utdannelse CvUtdannelse = new Utdannelse();
            CvUtdannelse.Beskrivelse = Beskrivelse;
            CvUtdannelse.Fra = Fra;
            CvUtdannelse.Til = Til;

            // Add
            Cv.Utdannelse.Add(CvUtdannelse);
            db.SaveChanges();
        }

        public void AddArbeid(string Arbeidsplass, string Rolle, string Beskrivelse, short Fra, short Til)
        {
            string UserId = User.Identity.GetUserId();

            var Item = from a in db.CVVersjon
                       where a.AspNetUserId.Equals(UserId)
                       select a;

            CVVersjon Cv = Item.FirstOrDefault();

            // Utdannelse variables
            Arbeidserfaring CvArbeid = new Arbeidserfaring();
            CvArbeid.Arbeidsplass = Arbeidsplass;
            CvArbeid.Stilling = Rolle;
            CvArbeid.Beskrivelse = Beskrivelse;
            CvArbeid.Fra = Fra;
            CvArbeid.Til = Til;

            // Add
            Cv.Arbeidserfaring.Add(CvArbeid);
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
                case "Fornavn":
                    Cv.Person.Fornavn = Value;
                    break;

                case "Etternavn":
                    Cv.Person.Etternavn = Value;
                    break;
                case "Stilling":
                    Cv.Person.Stilling = Value;
                    break;

                case "ÅrErfaring":
                    Cv.Person.ÅrErfaring = Int16.Parse(Value);
                    break;

                case "Språk":
                    Cv.Person.Språk = Value;
                    break;

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

        [HttpPost]
        public void InsertItem(string Insert, string Value)
        {
            if (Insert == "Språk")
            {
                SpråkListe NewItem = new SpråkListe();
                NewItem.Språk = Value;
                db.SpråkListe.Add(NewItem);
            }
            else if (Insert == "Programmeringsspråk")
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


        // Get various stuff for autocomplete

        [HttpGet]
        public ActionResult GetLanguages()
        {
            var Item = from a in db.SpråkListe
                            select a.Språk;

            return Json(Item, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetProgrammingLanguages()
        {
            var Item = from a in db.ProgrammeringsspråkListe
                       select a.Programmeringsspråk;

            return Json(Item, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetFrameworks()
        {
            var Item = from a in db.RammeverkListe
                       select a.Rammeverk;

            return Json(Item, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetWebTechnologies()
        {
            var Item = from a in db.WebTeknologiListe
                       select a.WebTeknologi;

            return Json(Item, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetDatabaseSystems()
        {
            var Item = from a in db.DatabasesystemListe
                       select a.Databasesystem;

            return Json(Item, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetServerside()
        {
            var Item = from a in db.ServersideListe
                       select a.Serverside;

            return Json(Item, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetOperatingSystems()
        {
            var Item = from a in db.OperativsystemListe
                       select a.Operativsystem;

            return Json(Item, JsonRequestBehavior.AllowGet);
        }
    }
}