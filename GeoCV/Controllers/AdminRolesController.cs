using GeoCV.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GeoCV.Controllers
{
    public class AdminRolesController : Controller
    {
        // GET: AdminRoles
        public ActionResult Index()
        {
            var RoleMan = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));
            var Roles = RoleMan.Roles;
            return View(Roles);
        }

        [HttpPost]
        public void AddNewRole(string RoleName)
        {
            var RoleMan = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));
            IdentityRole Role = new IdentityRole();
            Role.Name = RoleName;
            RoleMan.Create(Role);
        }

        [HttpPost]
        public void DeleteRole(string RoleName)
        {
            var RoleMan = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));
            IdentityRole Role = new IdentityRole();
            Role.Name = RoleName;
            RoleMan.Delete(Role);
        }
    }
}