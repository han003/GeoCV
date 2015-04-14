using GeoCV.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GeoCV.Controllers
{
    public class DatabaseController : Controller
    {

        private cvEntities db = new cvEntities();

        // GET: Database
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetEditData()
        {
            var Data = from a in db.ListeKatalog
                            select new
                            {
                                a.ListeKatalogId,
                                a.Katalog,
                                a.Element
                            };

            return Json(Data, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult FilterElements(string Filter)
        {
            var Data = from a in db.ListeKatalog
                       where a.Element.Contains(Filter)
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

            Row.Element = NewValue;

            db.SaveChanges();
        }

        [HttpPost]
        public void AddElement(string NyttElement, string Katalog)
        {
            ListeKatalog NewItem = new ListeKatalog();
            NewItem.Katalog = Katalog;
            NewItem.Element = NyttElement;
            db.ListeKatalog.Add(NewItem);

            db.SaveChanges();
        }
    }
}