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
            CVVersjon BrukerCv = GetUserCV();

            var Nasjonaliteter = from a in db.ListeKatalog
                                 where a.Katalog == "Nasjonaliteter"
                                 orderby a.Element ascending
                                 select a;

            var Stillinger = from a in db.ListeKatalog
                             where a.Katalog == "Stillinger"
                             orderby a.Element ascending
                             select a;

            var Språk = from a in db.ListeKatalog
                        where a.Katalog == "Språk"
                        orderby a.Element ascending
                        select a;

            List<string> BrukerSpråkListe = BrukerCv.Person.Språk.Split(';').ToList();

            var BrukerSpråk = from a in db.ListeKatalog
                              where BrukerSpråkListe.Contains(a.ListeKatalogId.ToString())
                              select a;


            PersonalModel ViewModel = new PersonalModel();
            ViewModel.BrukerCv = BrukerCv;
            ViewModel.Nasjonaliteter = Nasjonaliteter;
            ViewModel.Stillinger = Stillinger;
            ViewModel.Språk = Språk;
            ViewModel.BrukerSpråk = BrukerSpråk;


            return View(ViewModel);
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
                    Cv.Person.Stilling = Int32.Parse(Value);
                    break;

                case "Nasjonalitet":
                    Cv.Person.Nasjonalitet = Int32.Parse(Value);
                    break;

                case "ÅrErfaring":
                    Cv.Person.ÅrErfaring = (Value.Trim().Equals("")) ? Int16.Parse("0") : Int16.Parse(Value);
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