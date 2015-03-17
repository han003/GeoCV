using GeoCV.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GeoCV.Controllers
{

    [Authorize(Roles = "Admin")]
    public class EditProjectController : Controller
    {
        private cvEntities db = new cvEntities();

        public ActionResult Index(int Id)
        {
            var Item = from a in db.Prosjekt
                       where a.ProsjektId.Equals(Id)
                       select a;

            return View(Item.FirstOrDefault());
        }

        [HttpPost]
        public void UpdateProjectInfo(int Id, string Update, string Value)
        {
            var Item = from a in db.Prosjekt
                       where a.ProsjektId.Equals(Id)
                       select a;

            Prosjekt Pro = Item.FirstOrDefault();

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
            // Opprett liste
            var ElementListe = new List<String>();

            // Velg alle programmeringsspråk i databasen
            var Programmeringsspråk = from a in db.ProgrammeringsspråkListe
                                      select a.Programmeringsspråk;

            // Legg til i listen
            ElementListe.AddRange(Programmeringsspråk.ToList());

            // Velg alle rammeverk i databasen
            var Rammeverk = from a in db.RammeverkListe
                            select a.Rammeverk;

            // Legg til i listen
            ElementListe.AddRange(Rammeverk.ToList());

            // Velg alle webteknologier i databasen
            var Webteknologier = from a in db.WebTeknologiListe
                                 select a.WebTeknologi;

            // Legg til i listen
            ElementListe.AddRange(Webteknologier.ToList());

            // Velg alle databasesystemer i databasen
            var Databasesystemer = from a in db.DatabasesystemListe
                                   select a.Databasesystem;

            // Legg til i listen
            ElementListe.AddRange(Databasesystemer.ToList());

            // Velg alle serverside i databasen
            var Serverside = from a in db.ServersideListe
                                   select a.Serverside;

            // Legg til i listen
            ElementListe.AddRange(Serverside.ToList());

            // Velg alle operativsystemer i databasen
            var Operativsystemer = from a in db.OperativsystemListe
                                   select a.Operativsystem;

            // Legg til i listen
            ElementListe.AddRange(Operativsystemer.ToList());

            // Send listen som et JSON elemnt til View
            return Json(ElementListe, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public void AddTechProfile(int Id, string Navn, string Elementer)
        {
            var Item = from a in db.Prosjekt
                       where a.ProsjektId.Equals(Id)
                       select a;

            Prosjekt Pro = Item.FirstOrDefault();

            TekniskProfil NyTekniskProfil = new TekniskProfil();
            NyTekniskProfil.Navn = Navn;
            NyTekniskProfil.Elementer = Elementer;

            Pro.TekniskProfil.Add(NyTekniskProfil);

            db.SaveChanges();
        }

    }
}