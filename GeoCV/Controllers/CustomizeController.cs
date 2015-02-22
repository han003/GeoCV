using GeoCV.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CV.Controllers
{

    [Authorize]
    public class CustomizeController : Controller
    {
        private cvEntities db = new cvEntities();

        [Route("Customize/{id:int}")]
        [HttpGet]
        public ActionResult Index(int Id)
        {
            CVVersjon Cv = new CVVersjon();
            Cv = db.CVVersjon.Find(Id);

            return View(Cv);
        }
    }
}