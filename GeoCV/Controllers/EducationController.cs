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
    public class EducationController : BaseController
    {
        // GET: Education
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetUtdannelse()
        {
            var Utdannelse = GetUserCV().Utdannelse;

            List<Utdannelse> UtdannelseList = new List<Utdannelse>();

            foreach (var Item in Utdannelse)
            {
                Utdannelse NyUtdannelse = new Utdannelse();
                NyUtdannelse.UtdannelseId = Item.UtdannelseId;
                NyUtdannelse.Studiested = Item.Studiested;
                NyUtdannelse.Beskrivelse = Item.Beskrivelse;
                NyUtdannelse.Fra = Item.Fra;
                NyUtdannelse.Til = Item.Til;

                UtdannelseList.Add(NyUtdannelse);
            }

            List<Utdannelse> SortedList = UtdannelseList.OrderByDescending(u => u.Fra).ToList();

            return Json(SortedList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public void AddNewEducation(string Skole, string Beskrivelse, Int16 Fra, Int16 Til)
        {
            CVVersjon Cv = GetUserCV();

            Utdannelse NewItem = new Utdannelse();
            NewItem.Studiested = Skole;
            NewItem.Beskrivelse = Beskrivelse;
            NewItem.Fra = Fra;
            NewItem.Til = Til;

            Cv.Utdannelse.Add(NewItem);

            db.SaveChanges();
        }

        [HttpPost]
        public void DeleteElement(int Id)
        {
            var Utdannelse = GetUserCV().Utdannelse;

            Utdannelse ValgtUtdannelse = new Utdannelse();

            foreach (var Item in Utdannelse)
            {
                if (Item.UtdannelseId == Id)
                {
                    ValgtUtdannelse = Item;
                }
            }

            db.Utdannelse.Remove(ValgtUtdannelse);
            db.SaveChanges();
        }

        [HttpPost]
        public void ChangeElement(int Id, string NewValue, string Kolonne)
        {
            var Utdannelse = GetUserCV().Utdannelse;

            foreach (var Item in Utdannelse)
            {
                if (Item.UtdannelseId == Id)
                {
                    switch (Kolonne)
                    {
                        case "Studiested":
                            Item.Studiested = NewValue;
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


        