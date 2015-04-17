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
        // GET: Work
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public void AddNewWork(string Arbeidsplass, string Stilling, string Beskrivelse, Int16 Fra, Int16 Til)
        {
            CVVersjon Cv = GetUserCV();

            Arbeidserfaring NewItem = new Arbeidserfaring();
            NewItem.Arbeidsplass = Arbeidsplass;
            NewItem.Stilling = Stilling;
            NewItem.Beskrivelse = Beskrivelse;
            NewItem.Fra = Fra;
            NewItem.Til = Til;

            Cv.Arbeidserfaring.Add(NewItem);

            db.SaveChanges();
        }

        [HttpGet]
        public ActionResult GetArbeidserfaring()
        {
            var Arbeidserfaring = GetUserCV().Arbeidserfaring;

            List<Arbeidserfaring> ArbeidserfaringList = new List<Arbeidserfaring>();

            foreach (var Item in Arbeidserfaring)
            {
                Arbeidserfaring NyArbeidserfaring = new Arbeidserfaring();
                NyArbeidserfaring.ArbeidserfaringId = Item.ArbeidserfaringId;
                NyArbeidserfaring.Arbeidsplass = Item.Arbeidsplass;
                NyArbeidserfaring.Stilling = Item.Stilling;
                NyArbeidserfaring.Beskrivelse = Item.Beskrivelse;
                NyArbeidserfaring.Fra = Item.Fra;
                NyArbeidserfaring.Til = Item.Til;

                ArbeidserfaringList.Add(NyArbeidserfaring);
            }

            return Json(ArbeidserfaringList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public void DeleteElement(int Id)
        {
            var Arbeidserfaring = GetUserCV().Arbeidserfaring;

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
            var Arbeidserfaring = GetUserCV().Arbeidserfaring;

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