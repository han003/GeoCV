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
    public class UtdannelseController : BaseController
    {
        public ActionResult Index()
        {
            return View(GetBrukerCv(GetAspNetBrukerID()));
        }

        public ActionResult Ny()
        {
            return View();
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
            db.Utdannelse.Remove(GetBrukerCv(GetAspNetBrukerID()).Utdannelse.Where(x => x.UtdannelseId.Equals(Id)).FirstOrDefault());
            db.SaveChanges();
        }

        [HttpPost]
        public void ChangeElement(int Id, string NewValue, string Kolonne)
        {
            var Utdannelse = GetBrukerCv(GetAspNetBrukerID()).Utdannelse.Where(x => x.UtdannelseId.Equals(Id)).FirstOrDefault();

            switch (Kolonne)
            {
                case "Studiested":
                    Utdannelse.Studiested = NewValue;
                    break;

                case "Beskrivelse":
                    Utdannelse.Beskrivelse = NewValue;
                    break;

                case "Fra":
                    Utdannelse.Fra = Int16.Parse(NewValue);
                    break;

                case "Til":
                    Utdannelse.Til = Int16.Parse(NewValue);
                    break;
            }

            db.SaveChanges();
        }
    }
}


        