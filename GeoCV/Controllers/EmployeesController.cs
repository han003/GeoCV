using GeoCV.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CV.Controllers
{

    [Authorize]
    public class EmployeesController : Controller
    {

        private cvEntities1 db = new cvEntities1();

        // GET: Employees
        public ActionResult Index()
        {
            var CVer = from a in db.CVVersjon
                       select a;

            return View(CVer);
        }

        [HttpGet]
        public ActionResult Search(string Search)
        {
            db.Configuration.ProxyCreationEnabled = false;

            var Employees = from a in db.Person
                            where a.Fornavn.Contains(Search) || a.Etternavn.Contains(Search)
                            select new
                            { 
                                a.PersonId,
                                a.Fornavn,
                                a.Etternavn,
                                a.CVVersjon.First().CVVersjonId
                            };

            return Json(Employees, JsonRequestBehavior.AllowGet);
        }
    }
}