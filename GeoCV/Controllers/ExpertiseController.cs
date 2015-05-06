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
    public class ExpertiseController : BaseController
    {
        private IOrderedQueryable<ListeKatalog> GetKatalog()
        {
            var Katalog = from a in db.ListeKatalog
                          where a.Katalog != "Nasjonaliteter" || a.Katalog != "Stillinger" || a.Katalog != "Språk"
                          orderby a.Element descending
                          select a;

            return Katalog;
        }

        public ActionResult Programmeringsspraak()
        {
            // Bruker CV
            CVVersjon BrukerCv = GetBrukerCv(GetAspNetBrukerID());

            // Model for å sendte til View
            ExpertiseModel ViewModel = new ExpertiseModel();

            var Katalog = GetKatalog();
            ViewModel.Katalog = Katalog;

            try
            {
                List<string> BrukerProgrammeringsspråkListe = BrukerCv.Kompetanse.Programmeringsspråk.Split(';').ToList();
                ViewModel.BrukerProgrammeringsspråk = from a in Katalog
                                                      where BrukerProgrammeringsspråkListe.Contains(a.ListeKatalogId.ToString())
                                                      select a;
            }
            catch (Exception)
            {
            }

            return View(ViewModel);
        }

        public ActionResult Rammeverk()
        {
            // Bruker CV
            CVVersjon BrukerCv = GetBrukerCv(GetAspNetBrukerID());

            // Model for å sendte til View
            ExpertiseModel ViewModel = new ExpertiseModel();

            var Katalog = GetKatalog();
            ViewModel.Katalog = Katalog;

            try
            {
                List<string> BrukerRammeverkListe = BrukerCv.Kompetanse.Rammeverk.Split(';').ToList();
                ViewModel.BrukerRammeverk = from a in Katalog
                                            where BrukerRammeverkListe.Contains(a.ListeKatalogId.ToString())
                                            select a;
            }
            catch (Exception)
            {
            }

            return View(ViewModel);
        }

        public ActionResult Webteknologier()
        {
            // Bruker CV
            CVVersjon BrukerCv = GetBrukerCv(GetAspNetBrukerID());

            // Model for å sendte til View
            ExpertiseModel ViewModel = new ExpertiseModel();

            var Katalog = GetKatalog();
            ViewModel.Katalog = Katalog;

            try
            {
                List<string> BrukerWebteknologierListe = BrukerCv.Kompetanse.WebTeknologier.Split(';').ToList();
                ViewModel.BrukerWebteknologier = from a in Katalog
                                                 where BrukerWebteknologierListe.Contains(a.ListeKatalogId.ToString())
                                                 select a;
            }
            catch (Exception)
            {
            }

            return View(ViewModel);
        }

        public ActionResult Databasesystemer()
        {
            // Bruker CV
            CVVersjon BrukerCv = GetBrukerCv(GetAspNetBrukerID());

            // Model for å sendte til View
            ExpertiseModel ViewModel = new ExpertiseModel();

            var Katalog = GetKatalog();
            ViewModel.Katalog = Katalog;

            try
            {
                List<string> BrukerDatabasesystemerListe = BrukerCv.Kompetanse.Databasesystemer.Split(';').ToList();
                ViewModel.BrukerDatabasesystemer = from a in Katalog
                                                   where BrukerDatabasesystemerListe.Contains(a.ListeKatalogId.ToString())
                                                   select a;
            }
            catch (Exception)
            {
            }

            return View(ViewModel);
        }

        public ActionResult Serverside()
        {
            // Bruker CV
            CVVersjon BrukerCv = GetBrukerCv(GetAspNetBrukerID());

            // Model for å sendte til View
            ExpertiseModel ViewModel = new ExpertiseModel();

            var Katalog = GetKatalog();
            ViewModel.Katalog = Katalog;

            try
            {
                List<string> ServersideListe = BrukerCv.Kompetanse.Serverside.Split(';').ToList();
                ViewModel.BrukerServerside = from a in Katalog
                                             where ServersideListe.Contains(a.ListeKatalogId.ToString())
                                             select a;
            }
            catch (Exception)
            {
            }

            return View(ViewModel);
        }

        public ActionResult Operativsystemer()
        {
            // Bruker CV
            CVVersjon BrukerCv = GetBrukerCv(GetAspNetBrukerID());

            // Model for å sendte til View
            ExpertiseModel ViewModel = new ExpertiseModel();

            var Katalog = GetKatalog();
            ViewModel.Katalog = Katalog;

            try
            {
                List<string> BrukerOperativsystemerListe = BrukerCv.Kompetanse.Operativsystemer.Split(';').ToList();
                ViewModel.BrukerOperativsystemer = from a in Katalog
                                                   where BrukerOperativsystemerListe.Contains(a.ListeKatalogId.ToString())
                                                   select a;
            }
            catch (Exception)
            {
            }

            return View(ViewModel);
        }

        public ActionResult Annet()
        {
            // Bruker CV
            CVVersjon BrukerCv = GetBrukerCv(GetAspNetBrukerID());

            // Model for å sendte til View
            ExpertiseModel ViewModel = new ExpertiseModel();

            var Katalog = GetKatalog();
            ViewModel.Katalog = Katalog;

            try
            {
                List<string> BrukerAnnetListe = BrukerCv.Kompetanse.Annet.Split(';').ToList();
                ViewModel.BrukerAnnet = from a in Katalog
                                        where BrukerAnnetListe.Contains(a.ListeKatalogId.ToString())
                                        select a;
            }
            catch (Exception)
            {
            }

            return View(ViewModel);
        }

        [HttpPost]
        public void Update(string Update, string Value)
        {
            CVVersjon Cv = GetBrukerCv(GetAspNetBrukerID());

            switch (Update)
            {
                case "Programmeringsspråk":
                    Cv.Kompetanse.Programmeringsspråk = Value;
                    break;

                case "Rammeverk":
                    Cv.Kompetanse.Rammeverk = Value;
                    break;

                case "Webteknologier":
                    Cv.Kompetanse.WebTeknologier = Value;
                    break;

                case "Databasesystemer":
                    Cv.Kompetanse.Databasesystemer = Value;
                    break;

                case "Serverside":
                    Cv.Kompetanse.Serverside = Value;
                    break;

                case "Operativsystemer":
                    Cv.Kompetanse.Operativsystemer = Value;
                    break;

                case "Annet":
                    Cv.Kompetanse.Annet = Value;
                    break;
            }

            db.SaveChanges();
        }

    }
}