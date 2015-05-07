using GeoCV.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;

namespace GeoCV.Controllers
{
    [Authorize]
    public class MineProsjekterController : BaseController
    {
        public ActionResult Index()
        {
            CVVersjon BrukerCv = GetBrukerCv(GetAspNetBrukerID());

            var Prosjekter = from a in db.Prosjekt
                             select a;

            var katalog = from a in db.ListeKatalog
                          where a.Katalog != "Språk" || a.Katalog != "Nasjonaliteter"
                          select a;

            var Stillinger = katalog.Where(x => x.Katalog.Equals("Stillinger"));

            var BrukerProsjekter = from a in db.Medlem
                                   where a.Person.PersonId.Equals(BrukerCv.Person.PersonId)
                                   select a;

            //store data of both queries in your ViewModel class here:
            var ViewModel = new MineProsjekterIndexModel();
            ViewModel.Katalog = katalog;
            ViewModel.Prosjekt = Prosjekter;
            ViewModel.Stillinger = Stillinger;
            ViewModel.BrukerProsjekter = BrukerProsjekter;

            //return ViewModel to View.
            return View(ViewModel);
        }

        public ActionResult LeggTil()
        {
            CVVersjon BrukerCv = GetBrukerCv(GetAspNetBrukerID());

            MineProsjekterLeggTilModel ViewModel = new MineProsjekterLeggTilModel();

            ViewModel.AlleProsjekter = from a in db.Prosjekt
                                       select a;

            ViewModel.BrukerProsjekter = from a in db.Medlem
                                         where a.Person_PersonId.Equals(BrukerCv.Person.PersonId)
                                         select a;

            return View(ViewModel);
        }

        [HttpPost]
        public void LeggTilProsjekt(int ProsjektId)
        {
            // Bruker data
            var Bruker = GetBrukerCv(GetAspNetBrukerID());

            // Sjekk for å se om det er registrert fra før i databasen
            var Medlemsjekk = from a in db.Medlem
                              where a.ProsjektProsjektId.Equals(ProsjektId) && a.Person_PersonId.Equals(Bruker.Person.PersonId)
                              select a;

            if (Medlemsjekk.Count() == 0)
            {
                // Prosjekt data
                var Data = from a in db.Prosjekt
                           where a.ProsjektId.Equals(ProsjektId)
                           select a;

                var Prosjekt = Data.FirstOrDefault();

                // Legg til brukeren som et medlem i valgt prosjekt
                Medlem NyttMedlem = new Medlem();
                NyttMedlem.Person = Bruker.Person;
                NyttMedlem.Prosjekt = Prosjekt;
                NyttMedlem.Rolle = null;
                NyttMedlem.TekniskProfil = null;
                NyttMedlem.Start = DateTime.Now;
                NyttMedlem.Slutt = DateTime.Now;

                Prosjekt.Medlem.Add(NyttMedlem);

                db.SaveChanges();
            }
        }

        [HttpPost]
        public void EndreStilling(int ProsjektId, int NyStilling)
        {
            // Bruker data
            var Bruker = GetBrukerCv(GetAspNetBrukerID());

            // Prosjekt data
            var Data = from a in db.Medlem
                       where a.Person_PersonId.Equals(Bruker.Person.PersonId) && a.ProsjektProsjektId.Equals(ProsjektId)
                       select a;

            Data.FirstOrDefault().Rolle = NyStilling;
            db.SaveChanges();
        }

        [HttpPost]
        public void EndreTekniskProfil(int ProsjektId, int NyTekniskProfil)
        {
            // Bruker data
            var Bruker = GetBrukerCv(GetAspNetBrukerID());

            // Prosjekt data
            var Data = from a in db.Medlem
                       where a.Person_PersonId.Equals(Bruker.Person.PersonId) && a.ProsjektProsjektId.Equals(ProsjektId)
                       select a;

            Data.FirstOrDefault().TekniskProfil = NyTekniskProfil;
            db.SaveChanges();
        }

        [HttpPost]
        public void EndreDato(int ProsjektId, string NyDato, string Type)
        {
            // Bruker data
            var Bruker = GetBrukerCv(GetAspNetBrukerID());

            // Prosjekt data
            var Data = from a in db.Medlem
                       where a.Person_PersonId.Equals(Bruker.Person.PersonId) && a.ProsjektProsjektId.Equals(ProsjektId)
                       select a;

            var ProMedlem = Data.FirstOrDefault();
            if (Type.Equals("fra"))
            {
                ProMedlem.Start = DateTime.Parse(NyDato);
            }
            else
            {
                ProMedlem.Slutt = DateTime.Parse(NyDato);
            }

            db.SaveChanges();
        }

        [HttpPost]
        public void FjernProsjekt(int ProsjektId)
        {
            // Bruker data
            var Bruker = GetBrukerCv(GetAspNetBrukerID());

            Medlem BrukerData = db.Medlem.Where(x => x.Person_PersonId.Equals(Bruker.Person.PersonId) && x.ProsjektProsjektId.Equals(ProsjektId)).FirstOrDefault();

            db.Medlem.Remove(BrukerData);
            db.SaveChanges();
        }
    }
}