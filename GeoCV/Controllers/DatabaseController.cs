using GeoCV.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GeoCV.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DatabaseController : BaseController
    {
        // GET: Database
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetDatabase(string Filter)
        {
            var Data = from a in db.ListeKatalog
                       where a.Element.Contains(Filter)
                       orderby a.Element ascending
                       select new
                       {
                           a.ListeKatalogId,
                           a.Katalog,
                           a.Element
                       };

            return Json(Data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public void DeleteElement(int Id)
        {
            var Item = from a in db.ListeKatalog
                          where a.ListeKatalogId.Equals(Id)
                          select a;

            var Element = Item.FirstOrDefault();

            db.ListeKatalog.Remove(Element);

            db.SaveChanges();
        }

        [HttpPost]
        public void ChangeElement(int Id, string NewValue)
        {
            var Item = from a in db.ListeKatalog
                       where a.ListeKatalogId.Equals(Id)
                       select a;

            var Row = Item.FirstOrDefault();

            Row.Element = NewValue.Trim();

            db.SaveChanges();
        }

        [HttpPost]
        public void AddElement(string NyttElement, string Katalog)
        {
            var Data = from a in db.ListeKatalog
                       where a.Element.Equals(NyttElement) && a.Katalog.Equals(Katalog)
                       select a;

            if (Data.Count() == 0)
            {
                ListeKatalog NewItem = new ListeKatalog();
                NewItem.Katalog = Katalog;
                NewItem.Element = NyttElement.Trim();
                db.ListeKatalog.Add(NewItem);

                db.SaveChanges();
            }
        }
    }
}