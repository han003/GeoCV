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
            // Opprett model
            PersonalModel ViewModel = new PersonalModel();

            // Hent CV
            CVVersjon BrukerCv = GetBrukerCv(GetAspNetBrukerID());
            ViewModel.BrukerCv = BrukerCv;
            
            var Nasjonaliteter = from a in db.ListeKatalog
                                 where a.Katalog == "Nasjonaliteter"
                                 orderby a.Element ascending
                                 select a;
            ViewModel.Nasjonaliteter = Nasjonaliteter;

            var Stillinger = from a in db.ListeKatalog
                             where a.Katalog == "Stillinger"
                             orderby a.Element ascending
                             select a;
            ViewModel.Stillinger = Stillinger;

            var Språk = from a in db.ListeKatalog
                        where a.Katalog == "Språk"
                        orderby a.Element ascending
                        select a;
            ViewModel.Språk = Språk;

            // Prøv om det er noen verdier i stringen til språk
            try
            {
                List<string> BrukerSpråkListe = BrukerCv.Person.Språk.Split(';').ToList();
                var BrukerSpråk = from a in db.ListeKatalog
                                  where BrukerSpråkListe.Contains(a.ListeKatalogId.ToString())
                                  select a;
                ViewModel.BrukerSpråk = BrukerSpråk;
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