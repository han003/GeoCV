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
    public class MyProjectsController : BaseController
    {
        public ActionResult Index()
        {
            CVVersjon BrukerCv = GetUserCV();

            var Prosjekter = from a in db.Prosjekt
                             select a;

            var Stillinger = from a in db.ListeKatalog
                             where a.Katalog.Equals("Stillinger")
                             select a;

            var Data = from a in db.Medlem
                       where a.Person.PersonId.Equals(BrukerCv.Person.PersonId)
                       select new MyProjectCustomObject
                       {
                           ProsjektId = a.Prosjekt.ProsjektId,
                           ProsjektNavn = a.Prosjekt.Navn,
                           ProsjektKunde = a.Prosjekt.Kunde,

                           ProsjektTekniskProfil = a.Prosjekt.TekniskProfil,

                           MedlemId = a.MedlemId,
                           MedlemRolle = a.Rolle,
                           MedlemStart = a.Start,
                           MedlemSlutt = a.Slutt
                       };

            //store data of both queries in your ViewModel class here:
            var MyViewModel = new MyProjectViewModel();
            MyViewModel.Prosjekt = Prosjekter;
            MyViewModel.Stillinger = Stillinger;
            MyViewModel.Data = Data;

            //return ViewModel to View.
            return View(MyViewModel);
        }

        [HttpPost]
        public void LeggTilProsjekt(int ProsjektId)
        {
            // Bruker data
            var Bruker = GetUserCV();

            // Prosjekt data
            var Data = from a in db.Prosjekt
                           where a.ProsjektId.Equals(ProsjektId)
                           select a;

            var Prosjekt = Data.FirstOrDefault();

            // Legg til brukeren som et medlem i valgt prosjekt
            Medlem NyttMedlem = new Medlem();
            NyttMedlem.Person = Bruker.Person;
            NyttMedlem.Prosjekt = Prosjekt;
            NyttMedlem.Rolle = 0;
            NyttMedlem.Start = DateTime.Now;
            NyttMedlem.Slutt = DateTime.Now;

            Prosjekt.Medlem.Add(NyttMedlem);

            db.SaveChanges();
        }

        [HttpGet]
        public ActionResult GetMineProsjekter()
        {
            CVVersjon BrukerCv = GetUserCV();

            var Data = from a in db.Medlem
                       where a.Person.PersonId.Equals(BrukerCv.Person.PersonId)
                       select new MyProjectCustomObject
                       {
                           ProsjektId = a.Prosjekt.ProsjektId,
                           ProsjektNavn = a.Prosjekt.Navn,
                           ProsjektKunde = a.Prosjekt.Kunde,

                           MedlemId = a.MedlemId,
                           MedlemRolle = a.Rolle,
                           MedlemStart = a.Start,
                           MedlemSlutt = a.Slutt
                       };

            return Json(Data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public void EndreStilling(int ProsjektId, int NyStilling)
        {
            // Bruker data
            var Bruker = GetUserCV();

            // Prosjekt data
            var Data = from a in db.Medlem
                       where a.Person_PersonId.Equals(Bruker.Person.PersonId) && a.ProsjektProsjektId.Equals(ProsjektId)
                       select a;

            var ProMedlem = Data.FirstOrDefault();
            ProMedlem.Rolle = NyStilling;

            db.SaveChanges();
        }

        [HttpPost]
        public void EndreDato(int ProsjektId, string NyDato, string Type)
        {
            // Bruker data
            var Bruker = GetUserCV();

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
    }
}