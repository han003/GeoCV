using GeoCV.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace GeoCV.Controllers
{
    [Authorize]
    public class WorkController : BaseController
    {
        public ActionResult Min()
        {
            return View(GetBrukerCv(GetAspNetBrukerID()));
        }

        public ActionResult Ny()
        {
            WorkModel ViewModel = new WorkModel();
            ViewModel.BrukerCv = GetBrukerCv(GetAspNetBrukerID());
            ViewModel.Stillinger = from a in db.ListeKatalog
                                   where a.Katalog.Equals("Stillinger")
                                   select a;

            return View(ViewModel);
        }

        [HttpPost]
        public ActionResult AddNewWork(string Arbeidsplass, string Stilling, string Beskrivelse, bool Nåværende, Int16 Fra, Int16 Til)
        {
            CVVersjon Cv = GetBrukerCv(GetAspNetBrukerID());

            // Sjekk om den nye stillingen er satt som nåværende
            if (Nåværende)
            {

                // Gå gjennom arbeidserfaring å sjekk om en annen stilling er satt som nåværende
                foreach (var Item in Cv.Arbeidserfaring)
                {
                    if (Item.Nåværende)
                    {
                        Item.Nåværende = false;
                        Item.Til = Int16.Parse(DateTime.Now.Year.ToString());
                    }
                }

                db.SaveChanges();
            }

            // Endre hvis fra dato er større enn til dato
            if (Fra > Til)
            {
                Int16 NyFra = Til;
                Til = Fra;
                Fra = NyFra;
            }

            // Legg til ny arbeidserfaring
            Arbeidserfaring NewItem = new Arbeidserfaring();
            NewItem.Arbeidsplass = Arbeidsplass;
            NewItem.Stilling = Stilling;
            NewItem.Beskrivelse = Beskrivelse;
            NewItem.Nåværende = Nåværende;
            NewItem.Fra = Fra;
            NewItem.Til = (Nåværende) ? Int16.Parse("0") : Til;

            Cv.Arbeidserfaring.Add(NewItem);

            db.SaveChanges();

            return Json(NewItem.ArbeidserfaringId, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public void DeleteElement(int Id)
        {
            var Arbeidserfaring = GetBrukerCv(GetAspNetBrukerID()).Arbeidserfaring;

            Arbeidserfaring ValgtArbeidserfaring = new Arbeidserfaring();

            foreach (var Item in Arbeidserfaring)
            {
                if (Item.ArbeidserfaringId == Id)
                {
                    ValgtArbeidserfaring = Item;
                }
            }

            db.Arbeidserfaring.Remove(ValgtArbeidserfaring);
            db.SaveChanges();
        }

        [HttpPost]
        public void ChangeElement(int Id, string NewValue, string Kolonne)
        {
            var Arbeidserfaring = GetBrukerCv(GetAspNetBrukerID()).Arbeidserfaring;

            foreach (var Item in Arbeidserfaring)
            {
                if (Item.ArbeidserfaringId == Id)
                {
                    switch (Kolonne)
                    {
                        case "Arbeidsplass":
                            Item.Arbeidsplass = NewValue;
                            break;

                        case "Stilling":
                            Item.Stilling = NewValue;
                            break;

                        case "Beskrivelse":
                            Item.Beskrivelse = NewValue;
                            break;

                        case "Fra":
                            Item.Fra = Int16.Parse(NewValue);
                            break;

                        case "Til":
                            Item.Til = Int16.Parse(NewValue);
                            break;
                    }
                }
            }

            db.SaveChanges();
        }
    }
}