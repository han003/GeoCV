﻿using GeoCV.Models;
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
        public ActionResult Rediger()
        {
            var DatabaseElementer = from a in db.ListeKatalog
                                    orderby a.Element ascending
                                    select a;

            return View(DatabaseElementer);
        }

        public ActionResult Ny()
        {
            var DatabaseElementer = from a in db.ListeKatalog
                                    orderby a.Element ascending
                                    select a;

            return View(DatabaseElementer);
        }

        [HttpPost]
        public void SlettElement(int Id)
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
        public ActionResult AddElement(string NyttElement, string Katalog)
        {
            var Data = from a in db.ListeKatalog
                       where a.Element.Equals(NyttElement) && a.Katalog.Equals(Katalog)
                       select a;

            int NyId = 0;

            if (Data.Count() == 0)
            {
                ListeKatalog NyOppføring = new ListeKatalog();
                NyOppføring.Katalog = Katalog;
                NyOppføring.Element = NyttElement.Trim();
                db.ListeKatalog.Add(NyOppføring);

                db.SaveChanges();

                NyId = NyOppføring.ListeKatalogId;
            }

            return Json(NyId, JsonRequestBehavior.AllowGet);
        }
    }
}