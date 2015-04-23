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
            return View();
        }

        [HttpGet]
        public ActionResult GetProsjekter()
        {
            var Prosjekter = from a in db.Prosjekt
                             orderby a.ProsjektId descending
                             select a;

            List<Prosjekt> ProsjektListe = new List<Prosjekt>();

            foreach (var Item in Prosjekter)
            {
                Prosjekt NyttProsjekt = new Prosjekt();
                NyttProsjekt.ProsjektId = Item.ProsjektId;
                NyttProsjekt.Kunde = Item.Kunde;
                NyttProsjekt.Navn = Item.Navn;
                NyttProsjekt.Beskrivelse = Item.Beskrivelse;
                NyttProsjekt.Fra = Item.Fra;
                NyttProsjekt.Til = Item.Til;

                ProsjektListe.Add(NyttProsjekt);
            }

            return Json(ProsjektListe, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public void LeggTilProsjekt(string Kunde, string Navn, string Beskrivelse)
        {
            Prosjekt NewItem = new Prosjekt();
            NewItem.Kunde = Kunde.Trim();
            NewItem.Navn = Navn.Trim();
            NewItem.Beskrivelse = Beskrivelse.Trim();
            NewItem.Fra = short.Parse(DateTime.Now.Year.ToString());
            NewItem.Til = short.Parse(DateTime.Now.Year.ToString()); ;
            db.Prosjekt.Add(NewItem);

            db.SaveChanges();
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