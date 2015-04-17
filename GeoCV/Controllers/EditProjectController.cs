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
        public ActionResult GetElements()
        {
            // Hent alt relatert til prosjekt fra databasen
            var Items = from a in db.ListeKatalog
                        where a.Katalog != "Stillinger" && 
                              a.Katalog != "Nasjonaliteter" && 
                              a.Katalog != "Språk"
                        orderby a.Element ascending
                        select a.Element;

            // Send listen som et JSON elemnt til View
            return Json(Items, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public void AddTechProfile(int Id, string Navn, string Elementer)
        {
            Prosjekt Pro = GetProsjekt(Id);

            TekniskProfil NyTekniskProfil = new TekniskProfil();
            NyTekniskProfil.Navn = Navn;
            NyTekniskProfil.Elementer = Elementer;

            Pro.TekniskProfil.Add(NyTekniskProfil);

            db.SaveChanges();
        }

    }
}