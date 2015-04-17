using GeoCV;
using GeoCV.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace GeoCV.Controllers
{

    [Authorize]
    public class EmployeesController : BaseController
    {
        // GET: Employees
        public ActionResult Index()
        {
            var CVer = from a in db.CVVersjon
                       select a;
            
            return View(CVer);
        }

        public ActionResult ChangeUser(int Id)
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
        public void Activate(int Id)
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
            UserMan.Update(user);
        }

        [HttpPost]
        public void Deactivate(int Id)
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

        [HttpGet]
        public ActionResult GetEmployees()
        {
            var Employees = from a in db.CVVersjon
                            select new
                            {
                                a.CVVersjonId,
                                a.Aktiv,
                                a.Person.Fornavn,
                                a.Person.Mellomnavn,
                                a.Person.Etternavn
                            };

            return Json(Employees, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Search(string Search)
        {
            var Employees = from a in db.Person
                            where a.Fornavn.Contains(Search) || a.Etternavn.Contains(Search)
                            select new
                            { 
                                a.PersonId,
                                a.Fornavn,
                                a.Etternavn,
                                a.CVVersjon.FirstOrDefault().CVVersjonId
                            };

            return Json(Employees, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> NewEmployee(String Fornavn, String Etternavn, String Epost, String Passord, String Rolle)
        {
            // Roles
            var RoleMan = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));
            var UserMan = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

                var user = new ApplicationUser { UserName = Epost, Email = Epost };
                var result = await UserMan.CreateAsync(user, Passord);

                if (result.Succeeded)
                {
                    // Create new CV
                    CVVersjon Cv = new CVVersjon();
                    Cv.AspNetUserId = user.Id;
                    Cv.Aktiv = true;

                    Person CvPerson = new Person();
                    CvPerson.Fornavn = Fornavn;
                    CvPerson.Etternavn = Etternavn;

                    Kompetanse CvKompetanse = new Kompetanse();

                    Innstillinger CvInnstillinger = new Innstillinger();
                    CvInnstillinger.Fornavn = true;
                    CvInnstillinger.Etternavn = true;
                    CvInnstillinger.Mellomnavn = true;
                    CvInnstillinger.Stilling = true;
                    CvInnstillinger.ÅrErfaring = true;
                    CvInnstillinger.Språk = true;
                    CvInnstillinger.Nasjonalitet = true;
                    CvInnstillinger.Fødselsår = true;
                    CvInnstillinger.Programmeringsspråk = true;
                    CvInnstillinger.Rammeverk = true;
                    CvInnstillinger.WebTeknologier = true;
                    CvInnstillinger.Databasesystemer = true;
                    CvInnstillinger.Serverside = true;
                    CvInnstillinger.Operativsystemer = true;
                    CvInnstillinger.Utdannelse = true;
                    CvInnstillinger.Arbeidserfaring = true;
                    CvInnstillinger.Prosjekter = true;

                    Cv.Person = CvPerson;
                    Cv.Kompetanse = CvKompetanse;
                    Cv.Innstillinger = CvInnstillinger;

                    db.CVVersjon.Add(Cv);
                    db.SaveChanges();

                    // Employee role
                    string EmployeeRole = Rolle;

                    // If role doesn't exist
                    if (!RoleMan.RoleExists(EmployeeRole))
                    {
                        var RoleResult = RoleMan.Create(new IdentityRole(EmployeeRole));
                        if (!RoleResult.Succeeded)
                        { 
                            // Error stuff
                        };
                    }

                    // Add user to role
                    UserMan.AddToRole(user.Id, EmployeeRole);
                }

                return null;
            }
        }
    }
