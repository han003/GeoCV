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
            return View(GetBrukerCv(GetAspNetBrukerID()));
        }

        [HttpPost]
        public ActionResult AddNewEducation(string Skole, string Beskrivelse, Int16 Fra, Int16 Til)
        {
            CVVersjon Cv = GetBrukerCv(GetAspNetBrukerID());

            if (Fra > Til)
            {
                Int16 NyFra = Til;
                Til = Fra;
                Fra = NyFra;
            }

            Utdannelse NewItem = new Utdannelse();
            NewItem.Studiested = Skole;
            NewItem.Beskrivelse = Beskrivelse;
            NewItem.Fra = Fra;
            NewItem.Til = Til;

            Cv.Utdannelse.Add(NewItem);

            db.SaveChanges();

            return Json(NewItem.UtdannelseId, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public void DeleteElement(int Id)
        {
            var Utdannelse = GetBrukerCv(GetAspNetBrukerID()).Utdannelse;

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
            var Utdannelse = GetBrukerCv(GetAspNetBrukerID()).Utdannelse;

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


        