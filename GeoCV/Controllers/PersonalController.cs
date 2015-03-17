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
            string UserId = User.Identity.GetUserId();

            var Item = from a in db.CVVersjon
                       where a.AspNetUserId.Equals(UserId)
                       select a;

            return View(Item.FirstOrDefault());
        }

        [HttpGet]
        public ActionResult GetLanguages()
        {
            var Item = from a in db.SpråkListe
                       orderby a.Språk ascending
                       select a.Språk;

            return Json(Item, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public void InsertItem(string Insert, string Value)
        {
            if (Insert == "Språk")
            {
                SpråkListe NewItem = new SpråkListe();
                NewItem.Språk = Value;
                db.SpråkListe.Add(NewItem);
            }

            db.SaveChanges();
        }


        [HttpPost]
        public void Update(string Update, string Value)
        {

            string UserId = User.Identity.GetUserId();

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

                case "Fødselsår":
                    Cv.Person.Fødselsår = Int16.Parse(Value);
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