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
        public ActionResult Rediger()
        {
            var DatabaseElementer = from a in db.ListeKatalog
                                    orderby a.Element ascending
                                    select a;

            return View(DatabaseElementer);
        }

        public ActionResult LeggTil()
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
        public void EndreElement(int Id, string NyVerdi)
        {
            db.ListeKatalog.Where(x => x.ListeKatalogId.Equals(Id)).FirstOrDefault().Element = NyVerdi.Trim();
            db.SaveChanges();
        }

        [HttpPost]
        public ActionResult LeggTilElement(string NyttElement, string Katalog)
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
            else
            {
                return new HttpStatusCodeResult(400, NyttElement + " finnes allerede i databasen");
            }

            return Json(NyId, JsonRequestBehavior.AllowGet);
        }
    }
}