using GeoCV.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GeoCV.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProsjekterController : BaseController
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
            NewItem.Avsluttet = false;
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
        }

        [HttpPost]
        public void EndreProsjektInfo(int Id, string NyVerdi, string Tekstfelt)
        {
            var ProsjektData = from a in db.Prosjekt
                               where a.ProsjektId.Equals(Id)
                               select a;

            var Prosjekt = ProsjektData.FirstOrDefault();

            switch (Tekstfelt)
            {
                case "navn":
                    Prosjekt.Navn = NyVerdi;
                    break;

                case "kunde":
                    Prosjekt.Kunde = NyVerdi;
                    break;

                case "beskrivelse":
                    Prosjekt.Beskrivelse = NyVerdi;
                    break;
            }

            db.SaveChanges();
        }

        [HttpPost]
        public void EndrePorsjektStatus(int Id, bool Status)
        {
            db.Prosjekt.Where(x => x.ProsjektId.Equals(Id)).FirstOrDefault().Avsluttet = Status;
            db.SaveChanges();
        }

    }
}