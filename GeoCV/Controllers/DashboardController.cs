using GeoCV.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Net;

namespace GeoCV.Controllers
{

    [Authorize]
    public class DashboardController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }



        public ActionResult EndSession()
        {
            Session.Abandon();

            return RedirectToAction("Index", "Dashboard");
        }

    }
}