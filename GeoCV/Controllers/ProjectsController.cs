using GeoCV.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GeoCV.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProjectsController : BaseController
    {
        public ActionResult Index()
        {
            var Prosjekter = from a in db.Prosjekt
                             orderby a.ProsjektId descending
                             select a;

            return View(Prosjekter);
        }

        public ActionResult LeggTil()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LeggTilProsjekt(string Kunde, string Navn, string Beskrivelse)
        {
            Prosjekt NewItem = new Prosjekt();
            NewItem.Kunde = Kunde.Trim();
            NewItem.Navn = Navn.Trim();
            NewItem.Beskrivelse = Beskrivelse.Trim();
            NewItem.Fra = short.Parse(DateTime.Now.Year.ToString());
            NewItem.Til = short.Parse(DateTime.Now.Year.ToString()); ;
            db.Prosjekt.Add(NewItem);

            db.SaveChanges();

            return Json(NewItem.ProsjektId, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public void SlettProsjekt(int Id)
        {
            var Item = from a in db.Prosjekt
                       where a.ProsjektId.Equals(Id)
                       select a;

            Prosjekt ValgtProsjekt = Item.FirstOrDefault();
            ICollection<TekniskProfil> Profiler = ValgtProsjekt.TekniskProfil;
            ICollection<Medlem> Medlemmer = ValgtProsjekt.Medlem;

            db.Medlem.RemoveRange(Medlemmer);
            db.TekniskProfil.RemoveRange(Profiler);

            db.Prosjekt.Remove(ValgtProsjekt);
            db.SaveChanges();

            RedirectToAction("Index", "Projects");
        }
    }
}