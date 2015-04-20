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
            NewItem.Kunde = Kunde;
            NewItem.Navn = Navn;
            NewItem.Beskrivelse = Beskrivelse;
            NewItem.Fra = short.Parse(DateTime.Now.Year.ToString());
            NewItem.Til = short.Parse(DateTime.Now.Year.ToString()); ;
            db.Prosjekt.Add(NewItem);

            db.SaveChanges();
        }
    }
}