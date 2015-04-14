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
    public class PersonalController : Controller
    {

        private cvEntities db = new cvEntities();

        // GET: Personal
        public ActionResult Index()
        {
            string UserId = (Session["ShadowUser"] != null) ? Session["ShadowUser"].ToString() : User.Identity.GetUserId();

            var Item = from a in db.CVVersjon
                       where a.AspNetUserId.Equals(UserId)
                       select a;

            return View(Item.FirstOrDefault());
        }

        [HttpGet]
        public ActionResult GetLanguages()
        {
            var Item = from a in db.ListeKatalog
                       where a.Katalog == "Språk"
                       orderby a.Element ascending
                       select a.Element;

            return Json(Item, JsonRequestBehavior.AllowGet);
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
        public ActionResult GetValgtStilling()
        {
            string UserId = (Session["ShadowUser"] != null) ? Session["ShadowUser"].ToString() : User.Identity.GetUserId();
            
            var ValgtStilling = from a in db.CVVersjon
                                where a.AspNetUserId.Equals(UserId)
                                select a.Person.Stilling;

            return Json(ValgtStilling, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetBursdag()
        {
            string UserId = (Session["ShadowUser"] != null) ? Session["ShadowUser"].ToString() : User.Identity.GetUserId();

            var Bursdag = from a in db.CVVersjon
                          where a.AspNetUserId.Equals(UserId)
                          select a.Person.Fødselsår;

            return Json(Bursdag.FirstOrDefault().ToString(), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public void UpdateBirthdate(DateTime Value)
        {
            string UserId = (Session["ShadowUser"] != null) ? Session["ShadowUser"].ToString() : User.Identity.GetUserId();

            var Item = from a in db.CVVersjon
                       where a.AspNetUserId.Equals(UserId)
                       select a;

            CVVersjon Cv = Item.FirstOrDefault();

            Cv.Person.Fødselsår = Value;

            db.SaveChanges();
        }


        [HttpGet]
        public ActionResult GetStartDato()
        {
            string UserId = (Session["ShadowUser"] != null) ? Session["ShadowUser"].ToString() : User.Identity.GetUserId();

            var StartDato = from a in db.CVVersjon
                            where a.AspNetUserId.Equals(UserId)
                            select a.Person.StartDato;

            return Json(StartDato.FirstOrDefault().ToString(), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public void UpdateStartDato(DateTime Value)
        {
            string UserId = (Session["ShadowUser"] != null) ? Session["ShadowUser"].ToString() : User.Identity.GetUserId();

            var Item = from a in db.CVVersjon
                       where a.AspNetUserId.Equals(UserId)
                       select a;

            CVVersjon Cv = Item.FirstOrDefault();

            Cv.Person.StartDato = Value;

            db.SaveChanges();
        }

        [HttpPost]
        public void InsertItem(string Insert, string Value)
        {
            ListeKatalog NewItem = new ListeKatalog();
            NewItem.Katalog = Insert;
            NewItem.Element = Value;
            db.ListeKatalog.Add(NewItem);

            db.SaveChanges();
        }

        [HttpPost]
        public void Update(string Update, string Value)
        {

            string UserId = (Session["ShadowUser"] != null) ? Session["ShadowUser"].ToString() : User.Identity.GetUserId();

            var Item = from a in db.CVVersjon
                       where a.AspNetUserId.Equals(UserId)
                       select a;

            CVVersjon Cv = Item.FirstOrDefault();

            switch (Update)
            {
                case "Fornavn":
                    Cv.Person.Fornavn = Value;
                    break;

                case "Mellomnavn":
                    Cv.Person.Mellomnavn = Value;
                    break;

                case "Etternavn":
                    Cv.Person.Etternavn = Value;
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
            }


            db.SaveChanges();
        }

    }
}