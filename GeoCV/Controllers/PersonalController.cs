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
    public class PersonalController : BaseController
    {

        // GET: Personal
        public ActionResult Index()
        {
            return View(GetUserCV());
        }

        [HttpGet]
        public ActionResult GetLanguages()
        {
            string UserId = GetAspNetUserID();

            var BrukerSpråk = from a in db.CVVersjon
                              where a.AspNetUserId.Equals(UserId)
                              select a.Person.Språk;

            var Språk = from a in db.ListeKatalog
                        where a.Katalog == "Språk"
                        orderby a.Element ascending
                        select new
                        {
                            a.ListeKatalogId,
                            a.Element
                        };

            List<IQueryable> Kombinert = new List<IQueryable>();
            Kombinert.Add(BrukerSpråk);
            Kombinert.Add(Språk);

            return Json(Kombinert, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetStillinger()
        {
            var Stillinger = from a in db.ListeKatalog
                             where a.Katalog == "Stillinger"
                             select new
                             {
                                 a.ListeKatalogId,
                                 a.Element
                             };

            return Json(Stillinger, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetNasjonaliteter()
        {
            var Nasjonaliteter = from a in db.ListeKatalog
                                 where a.Katalog == "Nasjonaliteter"
                                 select new
                                 {
                                     a.ListeKatalogId,
                                     a.Element
                                 };

            return Json(Nasjonaliteter, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetValgtStilling()
        {
            var ValgtStilling = GetUserCV().Person.Stilling;

            return Json(ValgtStilling, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetValgtNasjonalitet()
        {
            var ValgtNasjonalitet = GetUserCV().Person.Nasjonalitet;

            return Json(ValgtNasjonalitet, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetBursdag()
        {
            var Bursdag = GetUserCV().Person.Fødselsår;

            return Json(Bursdag.ToString(), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetStartDato()
        {
            var StartDato = GetUserCV().Person.StartDato;

            return Json(StartDato.ToString(), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public void Update(string Update, string Value)
        {
            CVVersjon Cv = GetUserCV();

            switch (Update)
            {
                case "Fornavn":
                    Cv.Person.Fornavn = Value;
                    Session["ShadowUserName"] = Value + " " + Cv.Person.Etternavn;
                    break;

                case "Mellomnavn":
                    Cv.Person.Mellomnavn = Value;
                    break;

                case "Etternavn":
                    Cv.Person.Etternavn = Value;
                    Session["ShadowUserName"] = Cv.Person.Fornavn + " " + Value;
                    break;
                case "Stilling":
                    Cv.Person.Stilling = Value;
                    break;

                case "Nasjonalitet":
                    Cv.Person.Nasjonalitet = Value;
                    break;

                case "ÅrErfaring":
                    Cv.Person.ÅrErfaring = Int16.Parse(Value);
                    break;

                case "Språk":
                    Cv.Person.Språk = Value;
                    break;

                case "Fødselsår":
                    Cv.Person.Fødselsår = DateTime.Parse(Value);
                    break;

                case "StartDato":
                    Cv.Person.StartDato = DateTime.Parse(Value);
                    break;
            }


            db.SaveChanges();
        }

    }
}