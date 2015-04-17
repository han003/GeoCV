using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using GeoCV.Models;

namespace GeoCV.Controllers
{
    [Authorize]
    public class FeedbackController : BaseController
    {
        // GET: Feedback
        public ActionResult Index()
        {
            var Item = from a in db.Feedback
                       select a;

            return View(Item);
        }

        [HttpPost]
        public void SendFeedback(string Feedback)
        {
            var Bruker = GetUserCV().Person;

            Feedback NewFeedback = new Feedback();
            NewFeedback.Beskjed = Feedback;
            NewFeedback.Person = Bruker.Fornavn + " " + Bruker.Mellomnavn + " " + Bruker.Etternavn;
            NewFeedback.Dato = DateTime.Now.ToUniversalTime().AddHours(1);

            db.Feedback.Add(NewFeedback);

            db.SaveChanges();
        }
    }
}