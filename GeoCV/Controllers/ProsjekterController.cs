using GeoCV.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace GeoCV.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProsjekterController : BaseController
    {
        public ActionResult Index()
        {
            ProsjekterViewModel ViewModel = new ProsjekterViewModel();

            ViewModel.Prosjekter = from a in db.Prosjekt
                                   orderby a.ProsjektId descending
                                   select a;

            ViewModel.Katalog = from a in db.ListeKatalog
                                select a;

            return View(ViewModel);
        }

        [HttpPost]
        public ActionResult LeggTilProsjekt(ProsjekterModel Model)
        {
            Prosjekt NyttProsjekt = new Prosjekt();
            NyttProsjekt.Kunde = Model.Kunde.Trim();
            NyttProsjekt.Navn = Model.Prosjektnavn.Trim();
            NyttProsjekt.Beskrivelse = Model.Beskrivelse.Trim();
            NyttProsjekt.Fra = short.Parse(DateTime.Now.Year.ToString());
            NyttProsjekt.Til = short.Parse(DateTime.Now.Year.ToString());
            NyttProsjekt.Avsluttet = false;
            db.Prosjekt.Add(NyttProsjekt);

            db.SaveChanges();

            return RedirectToAction("Index", "Prosjekter");
        }

        [HttpPost]
        public ActionResult SlettProsjekt(SlettProsjektModel Model)
        {
            var Item = from a in db.Prosjekt
                       where a.ProsjektId.Equals(Model.Id)
                       select a;

            Prosjekt ValgtProsjekt = Item.FirstOrDefault();
            ICollection<TekniskProfil> Profiler = ValgtProsjekt.TekniskProfil;
            ICollection<Medlem> Medlemmer = ValgtProsjekt.Medlem;

            db.Medlem.RemoveRange(Medlemmer);
            db.TekniskProfil.RemoveRange(Profiler);

            db.Prosjekt.Remove(ValgtProsjekt);
            db.SaveChanges();

            return RedirectToAction("Index", "Prosjekter");
        }

        [HttpPost]
        public ActionResult EndreProsjektInfo(ProsjekterModel Model)
        {
            var ProsjektData = from a in db.Prosjekt
                               where a.ProsjektId.Equals(Model.Id)
                               select a;

            var ValgtProsjekt = ProsjektData.FirstOrDefault();

            ValgtProsjekt.Beskrivelse = Model.Beskrivelse;
            ValgtProsjekt.Kunde = Model.Kunde;
            ValgtProsjekt.Navn = Model.Prosjektnavn;

            db.SaveChanges();

            return RedirectToAction("Index", "Prosjekter");
        }

        public ActionResult EndrePorsjektStatus(int Id, bool Status)
        {
            db.Prosjekt.Where(x => x.ProsjektId.Equals(Id)).FirstOrDefault().Avsluttet = Status;
            db.SaveChanges();

            return RedirectToAction("Index", "Prosjekter");
        }

    }
}