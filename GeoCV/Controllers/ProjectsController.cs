using GeoCV.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GeoCV.Controllers
{
    [Authorize]
    public class ProjectsController : Controller
    {
        private cvEntities db = new cvEntities();

        // GET: Projects
        public ActionResult Index()
        {
            var Prosjekter = from a in db.Prosjekt
                             orderby a.ProsjektId descending
                             select a;

            return View(Prosjekter);
        }

        
    }
}