using GeoCV.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GeoCV.Controllers
{
    [Authorize]
    public class NewProjectController : Controller
    {

        private cvEntities db = new cvEntities();

        // GET: NewProject
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddNewProject(string Kunde, string Navn, string Beskrivelse)
        {
            Prosjekt NewItem = new Prosjekt();
            NewItem.Kunde = Kunde;
            NewItem.Navn = Navn;
            NewItem.Beskrivelse = Beskrivelse;
            NewItem.Fra = short.Parse(DateTime.Now.Year.ToString());
            NewItem.Til = 9999;
            db.Prosjekt.Add(NewItem);

            db.SaveChanges();

            return Json(NewItem.ProsjektId, JsonRequestBehavior.AllowGet);
        }
    }
}