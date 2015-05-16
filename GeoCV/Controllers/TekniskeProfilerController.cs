using GeoCV.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace GeoCV.Controllers
{

    [Authorize(Roles = "Admin")]
    public class TekniskeProfilerController : BaseController
    {
        public ActionResult Index(int? ProsjektId)
        {
            TekniskProfilModel ViewModel = new TekniskProfilModel();
            ViewModel.Prosjekter = GetProsjekter();
            ViewModel.TekniskeProfiler = GetTekniskeProfiler();
            ViewModel.KatalogElementer = GetKatalogElementer();

            return View(ViewModel);
        }

        public ActionResult LeggTilSlett(int? ProsjektId)
        {
            var ViewData = from a in db.Prosjekt
                           select a;

            return View(ViewData);
        }

        private IEnumerable<Prosjekt> GetProsjekter()
        {
            var Prosjekter = from a in db.Prosjekt
                             select a;

            return Prosjekter;
        }

        private IEnumerable<ListeKatalog> GetKatalogElementer()
        {
            // Hent alt relatert til prosjekt fra databasen
            var Items = from a in db.ListeKatalog
                        where a.Katalog != "Stillinger" &&
                              a.Katalog != "Nasjonaliteter" &&
                              a.Katalog != "Språk"
                        orderby a.Element ascending
                        select a;

            return Items;
        }

        private IEnumerable<TekniskProfil> GetTekniskeProfiler()
        {
            IEnumerable<TekniskProfil> TekniskProfiler = from a in db.TekniskProfil
                                                         select a;

            return TekniskProfiler;
        }

        [HttpPost]
        public ActionResult LeggTilProfil(LeggTilTekniskProfilModel Model)
        {
            var Data = from a in db.Prosjekt
                       where a.ProsjektId.Equals(Model.ProsjektId)
                       select a;

            Prosjekt Pro = Data.FirstOrDefault();

            TekniskProfil NyTekniskProfil = new TekniskProfil();
            NyTekniskProfil.Navn = Model.ProfilNavn;
            NyTekniskProfil.Elementer = "";

            Pro.TekniskProfil.Add(NyTekniskProfil);

            db.SaveChanges();

            return RedirectToAction("Index", "TekniskeProfiler");
        }

        [HttpPost]
        public void OppdaterProfil(int ProfilId, string Verdi)
        {
            db.TekniskProfil.Where(x => x.TekniskProfilId.Equals(ProfilId)).FirstOrDefault().Elementer = Verdi;
            db.SaveChanges();
        }

        [HttpPost]
        public ActionResult SlettProfil(SlettTekniskProfilModel Model)
        {
            db.TekniskProfil.Remove(db.TekniskProfil.Where(x => x.TekniskProfilId.Equals(Model.Id)).FirstOrDefault());
            db.SaveChanges();

            return RedirectToAction("Index", "TekniskeProfiler");
        }
    }
}