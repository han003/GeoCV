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
    [Authorize(Roles = "Admin")]
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
        public void AddRole(string RoleName)
        {
            // Roles
            var RoleMan = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));

            // If role doesn't exist
            if (!RoleMan.RoleExists(RoleName))
            {
                var RoleResult = RoleMan.Create(new IdentityRole(RoleName));
                if (!RoleResult.Succeeded)
                { 
                    // Error stuff
                };
            }
        }

        [HttpPost]
        public void DeleteRole(string RoleName)
        {
            var RoleMan = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));
            var Role = RoleMan.FindByName(RoleName);
            RoleMan.Delete(Role);
        }
    }
}