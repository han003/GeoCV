using GeoCV.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GeoCV.Controllers
{

    [Authorize(Roles = "Admin")]
    public class TekniskProfilController : BaseController
    {
        public ActionResult Index()
        {
            TekniskProfilModel ViewModel = new TekniskProfilModel();
            ViewModel.Prosjekter = GetProsjekter();
            ViewModel.TekniskeProfiler = GetTekniskeProfiler();
            ViewModel.KatalogElementer = GetKatalogElementer();

            return View(ViewModel);
        }

        public ActionResult LeggTil()
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
        public ActionResult LeggTilProfil(int Id, string Navn)
        {
            var Data = from a in db.Prosjekt
                           where a.ProsjektId.Equals(Id)
                           select a;

            Prosjekt Pro = Data.FirstOrDefault();

            TekniskProfil NyTekniskProfil = new TekniskProfil();
            NyTekniskProfil.Navn = Navn;
            NyTekniskProfil.Elementer = "";

            Pro.TekniskProfil.Add(NyTekniskProfil);

            db.SaveChanges();

            return Json(NyTekniskProfil.TekniskProfilId, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public void OppdaterProfil(int ProfilId, string Verdi)
        {
            db.TekniskProfil.Where(x => x.TekniskProfilId.Equals(ProfilId)).FirstOrDefault().Elementer = Verdi;
            db.SaveChanges();
        }

        [HttpPost]
        public void EndreProfilNavn(int ProfilId, string Navn)
        {
            var Profil = from a in db.TekniskProfil
                         where a.TekniskProfilId.Equals(ProfilId)
                         select a;

            TekniskProfil OppdaterProfil = Profil.FirstOrDefault();
            OppdaterProfil.Navn = Navn;

            db.SaveChanges();
        }

        [HttpPost]
        public void SlettProfil(int Id)
        {
            var Data = from a in db.TekniskProfil
                         where a.TekniskProfilId.Equals(Id)
                         select a;

            TekniskProfil Profil = Data.FirstOrDefault();

            db.TekniskProfil.Remove(Profil);

            db.SaveChanges();
        }
    }
}