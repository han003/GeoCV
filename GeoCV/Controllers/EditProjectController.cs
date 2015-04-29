using GeoCV.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GeoCV.Controllers
{

    [Authorize(Roles = "Admin")]
    public class EditProjectController : BaseController
    {
        public ActionResult Index(int Id)
        {
            EditProjectModel ViewModel = new EditProjectModel();
            ViewModel.Prosjekt = GetProsjekt(Id);
            ViewModel.TekniskeProfiler = GetTekniskeProfiler(Id);
            ViewModel.KatalogElementer = GetKatalogElementer();

            return View(ViewModel);
        }

        private Prosjekt GetProsjekt(int Id)
        {
            var Data = from a in db.Prosjekt
                       where a.ProsjektId.Equals(Id)
                       select a;

            return Data.FirstOrDefault();
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

        private IEnumerable<TekniskProfil> GetTekniskeProfiler(int Id)
        {
            Prosjekt NåværendeProsjekt = GetProsjekt(Id);

            IEnumerable<TekniskProfil> TekniskProfiler = NåværendeProsjekt.TekniskProfil;
            return TekniskProfiler;
        }

        [HttpPost]
        public void UpdateProjectInfo(int Id, string Update, string Value)
        {
            Prosjekt Pro = GetProsjekt(Id);

            switch (Update)
            {
                case "Navn":
                    Pro.Navn = Value;
                    break;

                case "Kunde":
                    Pro.Kunde = Value;
                    break;

                case "Beskrivelse":
                    Pro.Beskrivelse = Value;
                    break;
            }


            db.SaveChanges();
        }

        [HttpPost]
        public ActionResult LeggTilProfil(int Id, string Navn)
        {
            Prosjekt Pro = GetProsjekt(Id);

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
            var Profil = from a in db.TekniskProfil
                         where a.TekniskProfilId.Equals(ProfilId)
                         select a;

            TekniskProfil OppdaterProfil = Profil.FirstOrDefault();
            OppdaterProfil.Elementer = Verdi;

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