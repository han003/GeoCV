using GeoCV.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GeoCV.Controllers
{

    [Authorize(Roles = "Admin")]
    public class EditProjectController : Controller
    {
        private cvEntities db = new cvEntities();

        public ActionResult Index(int Id)
        {
            var Item = from a in db.Prosjekt
                       where a.ProsjektId.Equals(Id)
                       select a;

            return View(Item.FirstOrDefault());
        }

        [HttpPost]
        public void Update(int Id, string Update, string Value)
        {
            var Item = from a in db.Prosjekt
                       where a.ProsjektId.Equals(Id)
                       select a;

            Prosjekt Pro = Item.FirstOrDefault();

            switch (Update)
            {
                case "Navn":
                    Pro.Navn = Value;
                    break;

                case "Kunde":
                    Pro.Kunde = Value;
                    break;

                case "Beskrivelse":
                    Pro.Beskrivelse = Value;
                    break;
            }


            db.SaveChanges();
        }

    }
}