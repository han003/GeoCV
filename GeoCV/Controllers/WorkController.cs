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
    public class WorkController : Controller
    {
        private cvEntities db = new cvEntities();

        // GET: Work
        public ActionResult Index()
        {
            string UserId = User.Identity.GetUserId();

            var Item = from a in db.CVVersjon
                       where a.AspNetUserId.Equals(UserId)
                       select a;

            return View(Item.FirstOrDefault());
        }

        [HttpPost]
        public void AddNewWork(string Arbeidsplass, string Stilling, string Beskrivelse, Int16 Fra, Int16 Til)
        {
            string UserId = User.Identity.GetUserId();

            var Item = from a in db.CVVersjon
                       where a.AspNetUserId.Equals(UserId)
                       select a;

            CVVersjon Cv = Item.FirstOrDefault();

            Arbeidserfaring NewItem = new Arbeidserfaring();
            NewItem.Arbeidsplass = Arbeidsplass;
            NewItem.Stilling = Stilling;
            NewItem.Beskrivelse = Beskrivelse;
            NewItem.Fra = Fra;
            NewItem.Til = Til;

            Cv.Arbeidserfaring.Add(NewItem);

            db.SaveChanges();
        }
    }
}