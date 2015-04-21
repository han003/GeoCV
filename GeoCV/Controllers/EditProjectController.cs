﻿using GeoCV.Models;
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
        private Prosjekt GetProsjekt(int Id)
        {
            var Item = from a in db.Prosjekt
                       where a.ProsjektId.Equals(Id)
                       select a;

            return Item.FirstOrDefault();
        }

        public ActionResult Index(int Id)
        {
            return View(GetProsjekt(Id));
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

        [HttpGet]
        public ActionResult GetKatalogElementer()
        {
            // Hent alt relatert til prosjekt fra databasen
            var Items = from a in db.ListeKatalog
                        where a.Katalog != "Stillinger" &&
                              a.Katalog != "Nasjonaliteter" &&
                              a.Katalog != "Språk"
                        orderby a.Element ascending
                        select a;

            // Send listen som et JSON elemnt til View
            return Json(Items, JsonRequestBehavior.AllowGet);
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


        [HttpGet]
        public ActionResult GetAlleTeknologier(int Id)
        {
            Prosjekt NåværendeProsjekt = GetProsjekt(Id);

            List<TekniskProfil> ProfilListe = new List<TekniskProfil>();

            foreach (var Profil in NåværendeProsjekt.TekniskProfil)
            {

                TekniskProfil NyProfil = new TekniskProfil();
                NyProfil.TekniskProfilId = Profil.TekniskProfilId;
                NyProfil.Navn = Profil.Navn;
                NyProfil.Elementer = Profil.Elementer;
                ProfilListe.Add(NyProfil);

            }

            // Send listen som et JSON elemnt til View
            return Json(ProfilListe, JsonRequestBehavior.AllowGet);
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

        [HttpGet]
        public ActionResult HentElementer(string Elementer)
        {
            string[] ElementIDer = Elementer.Split(';');
            List<int> ElementIDerInt = new List<int>();

            foreach (var ID in ElementIDer)
            {
                ElementIDerInt.Add(Int32.Parse(ID));
            }

            var Katalog = from a in db.ListeKatalog
                          select a;

            List<ListeKatalog> ProfilElementer = new List<ListeKatalog>();

            foreach (var ElementID in ElementIDerInt)
            {
                foreach (var Item in Katalog)
                {
                    if (Item.ListeKatalogId.Equals(ElementID))
                    {
                        ListeKatalog Ny = new ListeKatalog();
                        Ny.ListeKatalogId = Item.ListeKatalogId;
                        Ny.Element = Item.Element;
                        Ny.Katalog = Item.Katalog;
                        ProfilElementer.Add(Ny);
                    }
                }
            }

            return Json(ProfilElementer, JsonRequestBehavior.AllowGet);
        }
    }
}