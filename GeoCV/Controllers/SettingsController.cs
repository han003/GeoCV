using GeoCV.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace GeoCV.Controllers
{
    [Authorize]
    public class SettingsController : BaseController
    {
        public ActionResult Index()
        {
            return View(GetBrukerCv(GetAspNetBrukerID()));
        }

        [HttpPost]
        public void Update(string Update, Boolean Value)
        {
            CVVersjon Cv = GetBrukerCv(GetAspNetBrukerID());

            switch (Update)
            {
                case "Fornavn":
                    Cv.Innstillinger.Fornavn = Value;
                    break;

                case "Mellomnavn":
                    Cv.Innstillinger.Mellomnavn = Value;
                    break;

                case "Etternavn":
                    Cv.Innstillinger.Etternavn = Value;
                    break;
                case "Stilling":
                    Cv.Innstillinger.Stilling = Value;
                    break;

                case "Fødselsår":
                    Cv.Innstillinger.Fødselsår = Value;
                    break;

                case "Nasjonalitet":
                    Cv.Innstillinger.Nasjonalitet = Value;
                    break;

                case "År Erfaring":
                    Cv.Innstillinger.ÅrErfaring = Value;
                    break;

                case "Språk":
                    Cv.Innstillinger.Språk = Value;
                    break;

                case "Programmeringsspråk":
                    Cv.Innstillinger.Programmeringsspråk = Value;
                    break;

                case "Rammeverk":
                    Cv.Innstillinger.Rammeverk = Value;
                    break;

                case "Web Teknologier":
                    Cv.Innstillinger.WebTeknologier = Value;
                    break;

                case "Databasesystemer":
                    Cv.Innstillinger.Databasesystemer = Value;
                    break;

                case "Serverside":
                    Cv.Innstillinger.Serverside = Value;
                    break;

                case "Operativsystemer":
                    Cv.Innstillinger.Operativsystemer = Value;
                    break;

                case "Annet":
                    Cv.Innstillinger.Annet = Value;
                    break;

                case "Utdannelse":
                    Cv.Innstillinger.Utdannelse = Value;
                    break;

                case "Arbeidserfaring":
                    Cv.Innstillinger.Arbeidserfaring = Value;
                    break;

                case "Prosjekter":
                    Cv.Innstillinger.Prosjekter = Value;
                    break;
            }


            db.SaveChanges();
        }
    }
}