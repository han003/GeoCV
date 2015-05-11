using GeoCV.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace GeoCV.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AnsatteController : BaseController
    {
        public ActionResult Index()
        {
            var Ansatte = from a in db.CVVersjon
                          orderby a.Person.Fornavn ascending
                          select a;

            return View(Ansatte);
        }

        public ActionResult LeggTil()
        {
            return View();
        }


        public ActionResult EndreBruker(int Id)
        {
            // Finn ansatt med riktig ID
            var Query = from a in db.CVVersjon
                        where a.CVVersjonId.Equals(Id)
                        select a;

            // Velg ansatt
            var NewUser = Query.FirstOrDefault();

            // Opprett en session som valgt ansatt
            Session["ShadowUser"] = NewUser.AspNetUserId;
            Session["ShadowUserName"] = NewUser.Person.Fornavn + " " + NewUser.Person.Etternavn;

            return RedirectToAction("Index", "Dashboard");
        }

        [HttpPost]
        public void Aktiver(int Id)
        {
            // Finn CVen som har brukerens ID
            var Item = from a in db.CVVersjon
                       where a.CVVersjonId.Equals(Id)
                       select a;

            CVVersjon Cv = Item.FirstOrDefault();

            Cv.Aktiv = true;

            db.SaveChanges();

            var UserMan = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            var user = UserMan.FindById(Cv.AspNetUserId);
            user.LockoutEnabled = false;
            user.LockoutEndDateUtc = null;
            UserMan.Update(user);
        }

        [HttpPost]
        public void Deaktiver(int Id)
        {
            // Finn CVen som har brukerens Id
            var Item = from a in db.CVVersjon
                       where a.CVVersjonId.Equals(Id)
                       select a;

            CVVersjon Cv = Item.FirstOrDefault();

            Cv.Aktiv = false;

            db.SaveChanges();

            var UserMan = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            var user = UserMan.FindById(Cv.AspNetUserId);
            user.LockoutEnabled = true;
            user.LockoutEndDateUtc = DateTime.Now.AddYears(100);
            UserMan.Update(user);
        }

        [HttpPost]
        public void SlettBruker(int Id)
        {
            // Finn riktig CV
            var Item = from a in db.CVVersjon
                       where a.CVVersjonId.Equals(Id)
                       select a;

            CVVersjon Cv = Item.FirstOrDefault();

            // Slett alt
            ICollection<Arbeidserfaring> Arbeid = Cv.Arbeidserfaring;
            db.Arbeidserfaring.RemoveRange(Arbeid);

            ICollection<Utdannelse> Skole = Cv.Utdannelse;
            db.Utdannelse.RemoveRange(Skole);

            db.Innstillinger.Remove(Cv.Innstillinger);

            db.Kompetanse.Remove(Cv.Kompetanse);

            db.Person.Remove(Cv.Person);

            db.CVVersjon.Remove(Cv);

            // Lagre
            db.SaveChanges();

            // Slett bruker fra AspNet databasen
            var UserMan = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            var user = UserMan.FindById(Cv.AspNetUserId);
            UserMan.Delete(user);
        }
    }
}
