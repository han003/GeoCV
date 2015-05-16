using GeoCV.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace GeoCV.Controllers
{
    [Authorize]
    public class ArbeidserfaringController : BaseController
    {
        public ActionResult Index()
        {
            return View(GetBrukerCv(GetAspNetBrukerID()));
        }

        [HttpPost]
        public ActionResult LeggTilArbeidserfaring(ArbeidserfaringModel Model)
        {
            CVVersjon Cv = GetBrukerCv(GetAspNetBrukerID());

            // Sjekk om den nye stillingen er satt som nåværende
            if (Model.NåværendeStilling)
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
            }

            // Endre hvis fra dato er større enn til dato
            if (Model.Fra > Model.Til)
            {
                Int16 NyFra = Int16.Parse(Model.Til.ToString());
                Model.Til = Model.Fra;
                Model.Fra = NyFra;
            }

            // Legg til ny arbeidserfaring
            Arbeidserfaring NewItem = new Arbeidserfaring();
            NewItem.Arbeidsplass = Model.Arbeidsplass;
            NewItem.Stilling = Model.Stilling;
            NewItem.Beskrivelse = Model.Beskrivelse;
            NewItem.Nåværende = Model.NåværendeStilling;
            NewItem.Fra = Int16.Parse(Model.Fra.ToString());
            NewItem.Til = (Model.NåværendeStilling) ? Int16.Parse("0") : Int16.Parse(Model.Til.ToString());

            Cv.Arbeidserfaring.Add(NewItem);

            db.SaveChanges();

            return RedirectToAction("Index", "Arbeidserfaring");
        }

        [HttpPost]
        public ActionResult SlettArbeidserfaring(int Id)
        {
            var Arbeidserfaring = GetBrukerCv(GetAspNetBrukerID()).Arbeidserfaring.Where(x => x.ArbeidserfaringId == Id).FirstOrDefault();
            db.Arbeidserfaring.Remove(Arbeidserfaring);
            db.SaveChanges();

            return RedirectToAction("Index", "Arbeidserfaring");
        }

        [HttpPost]
        public ActionResult RedigerArbeidserfaring(ArbeidserfaringModel Model)
        {
            CVVersjon Cv = GetBrukerCv(GetAspNetBrukerID());
            var Arbeidserfaring = GetBrukerCv(GetAspNetBrukerID()).Arbeidserfaring.Where(x => x.ArbeidserfaringId == Model.Id).FirstOrDefault();

            // Sjekk om den redigerte stillingen er satt som nåværende
            if (Model.NåværendeStilling)
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
            }

            Arbeidserfaring.Arbeidsplass = Model.Arbeidsplass;
            Arbeidserfaring.Stilling = Model.Stilling;
            Arbeidserfaring.Beskrivelse = Model.Beskrivelse;
            Arbeidserfaring.Nåværende = Model.NåværendeStilling;
            Arbeidserfaring.Fra = Int16.Parse(Model.Fra.ToString());
            Arbeidserfaring.Til = Int16.Parse(Model.Til.ToString());

            db.SaveChanges();

            return RedirectToAction("Index", "Arbeidserfaring");
        }
    }
}