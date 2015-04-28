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
        // GET: Expertise
        public ActionResult Index()
        {
            // Bruker CV
            CVVersjon BrukerCv = GetUserCV();

            // Model for å sendte til View
            ExpertiseModel ViewModel = new ExpertiseModel();
            
            // Legg til Katalog
            var Katalog = from a in db.ListeKatalog
                          where a.Katalog != "Nasjonaliteter" || a.Katalog != "Stillinger" || a.Katalog != "Språk"
                          select a;
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

            try
            {
                List<string> BrukerWebTeknologierListe = BrukerCv.Kompetanse.WebTeknologier.Split(';').ToList();
                ViewModel.BrukerWebTeknologier = from a in Katalog
                                                 where BrukerWebTeknologierListe.Contains(a.ListeKatalogId.ToString())
                                                 select a;
            }
            catch (Exception)
            {
            }

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

            try
            {
                List<string> BrukerServersideListe = BrukerCv.Kompetanse.Serverside.Split(';').ToList();
                ViewModel.BrukerServerside = from a in Katalog
                                             where BrukerServersideListe.Contains(a.ListeKatalogId.ToString())
                                             select a;
            }
            catch (Exception)
            {
            }

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


        [HttpGet]
        public ActionResult GetElements(string Katalog)
        {
            // Hent ID til bruker
            string UserId = GetAspNetUserID();

            IQueryable Bruker = Enumerable.Empty<IQueryable>().AsQueryable();

            switch (Katalog)
            {
                case "Programmeringsspråk":
                    Bruker = GetProgrammeringsspråk(UserId);
                    break;
                case "Rammeverk":
                    Bruker = GetRammeverk(UserId);
                    break;
                case "WebTeknologier":
                    Bruker = GetWebteknologier(UserId);
                    break;
                case "Databasesystemer":
                    Bruker = GetDatabasesystemer(UserId);
                    break;
                case "Serverside":
                    Bruker = GetServerside(UserId);
                    break;
                case "Operativsystemer":
                    Bruker = GetOperativsystemer(UserId);
                    break;
                case "Annet":
                    Bruker = GetAnnet(UserId);
                    break;
            }

            var Element = from a in db.ListeKatalog
                          where a.Katalog == Katalog
                          orderby a.Element ascending
                          select new
                          {
                              a.ListeKatalogId,
                              a.Element
                          };

            List<IQueryable> Kombinert = new List<IQueryable>();
            Kombinert.Add(Bruker);
            Kombinert.Add(Element);

            return Json(Kombinert, JsonRequestBehavior.AllowGet);
        }

        private IQueryable GetProgrammeringsspråk(string UserId)
        {
            var Bruker = from a in db.CVVersjon
                         where a.AspNetUserId.Equals(UserId)
                         select a.Kompetanse.Programmeringsspråk;

            return Bruker;
        }

        private IQueryable GetRammeverk(string UserId)
        {
            var Bruker = from a in db.CVVersjon
                         where a.AspNetUserId.Equals(UserId)
                         select a.Kompetanse.Rammeverk;

            return Bruker;
        }

        private IQueryable GetWebteknologier(string UserId)
        {
            var Bruker = from a in db.CVVersjon
                         where a.AspNetUserId.Equals(UserId)
                         select a.Kompetanse.WebTeknologier;

            return Bruker;
        }

        private IQueryable GetDatabasesystemer(string UserId)
        {
            var Bruker = from a in db.CVVersjon
                         where a.AspNetUserId.Equals(UserId)
                         select a.Kompetanse.Databasesystemer;

            return Bruker;
        }

        private IQueryable GetServerside(string UserId)
        {
            var Bruker = from a in db.CVVersjon
                         where a.AspNetUserId.Equals(UserId)
                         select a.Kompetanse.Serverside;

            return Bruker;
        }

        private IQueryable GetOperativsystemer(string UserId)
        {
            var Bruker = from a in db.CVVersjon
                         where a.AspNetUserId.Equals(UserId)
                         select a.Kompetanse.Operativsystemer;

            return Bruker;
        }

        private IQueryable GetAnnet(string UserId)
        {
            var Bruker = from a in db.CVVersjon
                         where a.AspNetUserId.Equals(UserId)
                         select a.Kompetanse.Annet;

            return Bruker;
        }

        [HttpPost]
        public void Update(string Update, string Value)
        {
            CVVersjon Cv = GetUserCV();

            switch (Update)
            {
                case "Programmeringsspråk":
                    Cv.Kompetanse.Programmeringsspråk = Value;
                    break;

                case "Rammeverk":
                    Cv.Kompetanse.Rammeverk = Value;
                    break;

                case "WebTeknologier":
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