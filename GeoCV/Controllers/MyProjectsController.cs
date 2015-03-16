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
    public class MyProjectsController : Controller
    {
        private cvEntities db = new cvEntities();

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

        [HttpPost]
        public void AddNewProject(string Prosjekt, string Rolle)
        {
            // Hent ID til bruker som er innlogget
            string UserId = User.Identity.GetUserId();

            // Hent CVVersjon til innlogget bruker
            var CvQuery = from a in db.CVVersjon
                       where a.AspNetUserId.Equals(UserId)
                       select a;

            CVVersjon CvDb = CvQuery.FirstOrDefault();

            // Hent valgt prosjekt
            var ProsjektQuery = from a in db.Prosjekt
                           where a.Navn.Equals(Prosjekt)
                           select a;

            Prosjekt ProsjektDb = ProsjektQuery.FirstOrDefault();

            // Opprett bruker som ett nytt medlem
            Medlem NyMedlem = new Medlem();
            NyMedlem.Rolle = Rolle;
            NyMedlem.Start = 1991;
            NyMedlem.Slutt = 1991;
            NyMedlem.Person = CvDb.Person;
            NyMedlem.Prosjekt = ProsjektDb;

            // Legg til medlem
            CvDb.Person.Medlem.Add(NyMedlem);

            // Lagre endringer
            db.SaveChanges();
        }
    }
}