using GeoCV.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GeoCV.Controllers
{
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
    }
}