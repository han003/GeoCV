using GeoCV.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;

namespace GeoCV.Controllers
{

    [Authorize]
    public class EducationController : Controller
    {
        private cvEntities db = new cvEntities();

        // GET: Education
        public ActionResult Index()
        {
            string UserId = (Session["ShadowUser"] != null) ? Session["ShadowUser"].ToString() : User.Identity.GetUserId();

            var Item = from a in db.CVVersjon
                       where a.AspNetUserId.Equals(UserId)
                       select a;

            return View(Item.FirstOrDefault());
        }

        [HttpGet]
        public ActionResult GetUtdannelse()
        {
            string UserId = (Session["ShadowUser"] != null) ? Session["ShadowUser"].ToString() : User.Identity.GetUserId();

            var Data = from a in db.CVVersjon
                       where a.AspNetUserId.Equals(UserId)
                       select a;

            var Utdannelse = Data.FirstOrDefault().Utdannelse;

            List<Utdannelse> UtdannelseList = new List<Utdannelse>();

            foreach (var Item in Utdannelse)
            {
                Utdannelse NyUtdannelse = new Utdannelse();
                NyUtdannelse.UtdannelseId = Item.UtdannelseId;
                NyUtdannelse.Studiested = Item.Studiested;
                NyUtdannelse.Beskrivelse = Item.Beskrivelse;
                NyUtdannelse.Fra = Item.Fra;
                NyUtdannelse.Til = Item.Til;

                UtdannelseList.Add(NyUtdannelse);
            }

            return Json(UtdannelseList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public void AddNewEducation(string Skole, string Beskrivelse, Int16 Fra, Int16 Til)
        {
            string UserId = (Session["ShadowUser"] != null) ? Session["ShadowUser"].ToString() : User.Identity.GetUserId();

            var Item = from a in db.CVVersjon
                       where a.AspNetUserId.Equals(UserId)
                       select a;

            CVVersjon Cv = Item.FirstOrDefault();

            Utdannelse NewItem = new Utdannelse();
            NewItem.Studiested = Skole;
            NewItem.Beskrivelse = Beskrivelse;
            NewItem.Fra = Fra;
            NewItem.Til = Til;

            Cv.Utdannelse.Add(NewItem);

            db.SaveChanges();
        }

        [HttpPost]
        public void DeleteElement(int Id)
        {
            string UserId = (Session["ShadowUser"] != null) ? Session["ShadowUser"].ToString() : User.Identity.GetUserId();

            var Data = from a in db.CVVersjon
                       where a.AspNetUserId.Equals(UserId)
                       select a;

            var Utdannelse = Data.FirstOrDefault().Utdannelse;

            Utdannelse ValgtUtdannelse = new Utdannelse();

            foreach (var Item in Utdannelse)
            {
                if (Item.UtdannelseId == Id)
                {
                    ValgtUtdannelse = Item;
                }
            }

            db.Utdannelse.Remove(ValgtUtdannelse);
            db.SaveChanges();
        }

        [HttpPost]
        public void ChangeElement(int Id, string NewValue, string Kolonne)
        {
            string UserId = (Session["ShadowUser"] != null) ? Session["ShadowUser"].ToString() : User.Identity.GetUserId();

            var Data = from a in db.CVVersjon
                       where a.AspNetUserId.Equals(UserId)
                       select a;

            var Utdannelse = Data.FirstOrDefault().Utdannelse;

            foreach (var Item in Utdannelse)
            {
                if (Item.UtdannelseId == Id)
                {
                    switch (Kolonne)
                    {
                        case "Studiested":
                            Item.Studiested = NewValue;
                            break;

                        case "Beskrivelse":
                            Item.Beskrivelse = NewValue;
                            break;

                        case "Fra":
                            Item.Fra = Int16.Parse(NewValue);
                            break;

                        case "Til":
                            Item.Til = Int16.Parse(NewValue);
                            break;
                    }
                }
            }

            db.SaveChanges();
        }

    }
}