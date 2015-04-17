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
    public class MyProjectsController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetEksisterendeProsjekter()
        {
            var Item = from a in db.Prosjekt
                       orderby a.Navn ascending
                       select a.Navn;

            return Json(Item, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public void AddNewProject(string Prosjekt, string Rolle)
        {
            CVVersjon Cv = GetUserCV();

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
            NyMedlem.Person = Cv.Person;
            NyMedlem.Prosjekt = ProsjektDb;

            // Legg til medlem
            Cv.Person.Medlem.Add(NyMedlem);

            // Lagre endringer
            db.SaveChanges();
        }
    }
}