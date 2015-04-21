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
    public class MyProjectsController : BaseController
    {
        public ActionResult Index()
        {
            var Prosjekter = from a in db.Prosjekt
                             select a;

            return View(Prosjekter);
        }
    }
}