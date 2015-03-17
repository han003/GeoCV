using System.Web;
using System.Web.Optimization;

namespace GeoCV
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {

            // Home
            bundles.Add(new ScriptBundle("~/Bundles/js/home")
                .Include("~/Scripts/jquery/jquery-{version}.js")
                .Include("~/Scripts/jquery/jquery.validate.js")
                .Include("~/Scripts/bootstrap/*.js")
                .Include("~/Scripts/home.js"));

            bundles.Add(new StyleBundle("~/Bundles/css/home")
                .Include("~/Content/bootstrap.css")
                .Include("~/Content/main.css")
                .Include("~/Content/home.css"));

            // Dashboard
            bundles.Add(new ScriptBundle("~/Bundles/js/dashboard")
                .Include("~/Scripts/jquery/jquery-{version}.js")
                .Include("~/Scripts/jquery/jquery.validate.js")
                .Include("~/Scripts/bootstrap/*.js")
                .Include("~/Scripts/dashboard.js"));

            bundles.Add(new StyleBundle("~/Bundles/css/dashboard")
                .Include("~/Content/bootstrap.css")
                .Include("~/Content/main.css")
                .Include("~/Content/dashboard.css"));

            // Search
            bundles.Add(new ScriptBundle("~/Bundles/js/search")
                .Include("~/Scripts/jquery/jquery-{version}.js")
                .Include("~/Scripts/jquery/jquery.validate.js")
                .Include("~/Scripts/bootstrap/*.js")
                .Include("~/Scripts/search.js"));

            bundles.Add(new StyleBundle("~/Bundles/css/search")
                .Include("~/Content/bootstrap.css")
                .Include("~/Content/main.css")
                .Include("~/Content/search.css"));

            // Customize
            bundles.Add(new ScriptBundle("~/Bundles/js/customize")
                .Include("~/Scripts/jquery/jquery-{version}.js")
                .Include("~/Scripts/jquery/jquery.validate.js")
                .Include("~/Scripts/bootstrap/*.js")
                .Include("~/Scripts/customize.js"));

            bundles.Add(new StyleBundle("~/Bundles/css/customize")
                .Include("~/Content/bootstrap.css")
                .Include("~/Content/main.css")
                .Include("~/Content/customize.css"));

            // Employees
            bundles.Add(new ScriptBundle("~/Bundles/js/employees")
                .Include("~/Scripts/jquery/jquery-{version}.js")
                .Include("~/Scripts/jquery/jquery.validate.js")
                .Include("~/Scripts/bootstrap/*.js")
                .Include("~/Scripts/employees.js"));

            bundles.Add(new StyleBundle("~/Bundles/css/employees")
                .Include("~/Content/bootstrap.css")
                .Include("~/Content/main.css")
                .Include("~/Content/employees.css"));

            // Admin Projects
            bundles.Add(new ScriptBundle("~/Bundles/js/projects")
                .Include("~/Scripts/jquery/jquery-{version}.js")
                .Include("~/Scripts/jquery/jquery.validate.js")
                .Include("~/Scripts/bootstrap/*.js")
                .Include("~/Scripts/projects.js"));

            bundles.Add(new StyleBundle("~/Bundles/css/projects")
                .Include("~/Content/bootstrap.css")
                .Include("~/Content/main.css")
                .Include("~/Content/projects.css"));

            // Register
            bundles.Add(new ScriptBundle("~/Bundles/js/register")
                .Include("~/Scripts/jquery/jquery-{version}.js")
                .Include("~/Scripts/jquery/jquery.validate.js")
                .Include("~/Scripts/bootstrap/*.js")
                .Include("~/Scripts/register.js"));

            bundles.Add(new StyleBundle("~/Bundles/css/register")
                .Include("~/Content/bootstrap.css")
                .Include("~/Content/main.css")
                .Include("~/Content/register.css"));

            // Login
            bundles.Add(new ScriptBundle("~/Bundles/js/login")
                .Include("~/Scripts/jquery/jquery-{version}.js")
                .Include("~/Scripts/jquery/jquery.validate.js")
                .Include("~/Scripts/bootstrap/*.js")
                .Include("~/Scripts/login.js"));

            bundles.Add(new StyleBundle("~/Bundles/css/login")
                .Include("~/Content/bootstrap.css")
                .Include("~/Content/main.css")
                .Include("~/Content/login.css"));

            // Manage
            bundles.Add(new ScriptBundle("~/Bundles/js/manage")
                .Include("~/Scripts/jquery/jquery-{version}.js")
                .Include("~/Scripts/jquery/jquery.validate.js")
                .Include("~/Scripts/bootstrap/*.js")
                .Include("~/Scripts/manage.js"));

            bundles.Add(new StyleBundle("~/Bundles/css/manage")
                .Include("~/Content/bootstrap.css")
                .Include("~/Content/main.css")
                .Include("~/Content/manage.css"));

            // Change Password
            bundles.Add(new ScriptBundle("~/Bundles/js/changepass")
                .Include("~/Scripts/jquery/jquery-{version}.js")
                .Include("~/Scripts/jquery/jquery.validate.js")
                .Include("~/Scripts/bootstrap/*.js")
                .Include("~/Scripts/changepass.js"));

            bundles.Add(new StyleBundle("~/Bundles/css/changepass")
                .Include("~/Content/bootstrap.css")
                .Include("~/Content/main.css")
                .Include("~/Content/changepass.css"));

            // Personal
            bundles.Add(new ScriptBundle("~/Bundles/js/personal")
                .Include("~/Scripts/jquery/jquery-{version}.js")
                .Include("~/Scripts/jquery/jquery.validate.js")
                .Include("~/Scripts/bootstrap/*.js")
                .Include("~/Scripts/bootstrap3-typeahead.js")
                .Include("~/Scripts/personal.js"));

            bundles.Add(new StyleBundle("~/Bundles/css/personal")
                .Include("~/Content/bootstrap.css")
                .Include("~/Content/main.css")
                .Include("~/Content/personal.css"));

            // Expertise
            bundles.Add(new ScriptBundle("~/Bundles/js/expertise")
                .Include("~/Scripts/jquery/jquery-{version}.js")
                .Include("~/Scripts/jquery/jquery.validate.js")
                .Include("~/Scripts/bootstrap/*.js")
                .Include("~/Scripts/bootstrap3-typeahead.js")
                .Include("~/Scripts/expertise.js"));

            bundles.Add(new StyleBundle("~/Bundles/css/expertise")
                .Include("~/Content/bootstrap.css")
                .Include("~/Content/main.css")
                .Include("~/Content/expertise.css"));

            // Education
            bundles.Add(new ScriptBundle("~/Bundles/js/education")
                .Include("~/Scripts/jquery/jquery-{version}.js")
                .Include("~/Scripts/jquery/jquery.validate.js")
                .Include("~/Scripts/bootstrap/*.js")
                .Include("~/Scripts/bootstrap3-typeahead.js")
                .Include("~/Scripts/education.js"));

            bundles.Add(new StyleBundle("~/Bundles/css/education")
                .Include("~/Content/bootstrap.css")
                .Include("~/Content/main.css")
                .Include("~/Content/education.css"));

            // Work
            bundles.Add(new ScriptBundle("~/Bundles/js/work")
                .Include("~/Scripts/jquery/jquery-{version}.js")
                .Include("~/Scripts/jquery/jquery.validate.js")
                .Include("~/Scripts/bootstrap/*.js")
                .Include("~/Scripts/bootstrap3-typeahead.js")
                .Include("~/Scripts/work.js"));

            bundles.Add(new StyleBundle("~/Bundles/css/work")
                .Include("~/Content/bootstrap.css")
                .Include("~/Content/main.css")
                .Include("~/Content/work.css"));

            // My Projects
            bundles.Add(new ScriptBundle("~/Bundles/js/myprojects")
                .Include("~/Scripts/jquery/jquery-{version}.js")
                .Include("~/Scripts/jquery/jquery.validate.js")
                .Include("~/Scripts/bootstrap/*.js")
                .Include("~/Scripts/bootstrap3-typeahead.js")
                .Include("~/Scripts/myprojects.js"));

            bundles.Add(new StyleBundle("~/Bundles/css/myprojects")
                .Include("~/Content/bootstrap.css")
                .Include("~/Content/main.css")
                .Include("~/Content/myprojects.css"));

            // New Project
            bundles.Add(new ScriptBundle("~/Bundles/js/newproject")
                .Include("~/Scripts/jquery/jquery-{version}.js")
                .Include("~/Scripts/jquery/jquery.validate.js")
                .Include("~/Scripts/bootstrap/*.js")
                .Include("~/Scripts/newproject.js"));

            bundles.Add(new StyleBundle("~/Bundles/css/newproject")
                .Include("~/Content/bootstrap.css")
                .Include("~/Content/main.css")
                .Include("~/Content/newproject.css"));

            // Edit Project
            bundles.Add(new ScriptBundle("~/Bundles/js/editproject")
                .Include("~/Scripts/jquery/jquery-{version}.js")
                .Include("~/Scripts/jquery/jquery.validate.js")
                .Include("~/Scripts/bootstrap/*.js")
                .Include("~/Scripts/editproject.js"));

            bundles.Add(new StyleBundle("~/Bundles/css/editproject")
                .Include("~/Content/bootstrap.css")
                .Include("~/Content/main.css")
                .Include("~/Content/editproject.css"));

            // Settings
            bundles.Add(new ScriptBundle("~/Bundles/js/settings")
                .Include("~/Scripts/jquery/jquery-{version}.js")
                .Include("~/Scripts/jquery/jquery.validate.js")
                .Include("~/Scripts/bootstrap/*.js")
                .Include("~/Scripts/settings.js"));

            bundles.Add(new StyleBundle("~/Bundles/css/settings")
                .Include("~/Content/bootstrap.css")
                .Include("~/Content/main.css")
                .Include("~/Content/settings.css"));

            // Admin Roles
            bundles.Add(new ScriptBundle("~/Bundles/js/adminroles")
                .Include("~/Scripts/jquery/jquery-{version}.js")
                .Include("~/Scripts/jquery/jquery.validate.js")
                .Include("~/Scripts/bootstrap/*.js")
                .Include("~/Scripts/bootstrap3-typeahead.js")
                .Include("~/Scripts/adminroles.js"));

            bundles.Add(new StyleBundle("~/Bundles/css/adminroles")
                .Include("~/Content/bootstrap.css")
                .Include("~/Content/main.css")
                .Include("~/Content/adminroles.css"));

            // Feedback
            bundles.Add(new ScriptBundle("~/Bundles/js/feedback")
                .Include("~/Scripts/jquery/jquery-{version}.js")
                .Include("~/Scripts/jquery/jquery.validate.js")
                .Include("~/Scripts/bootstrap/*.js")
                .Include("~/Scripts/bootstrap3-typeahead.js")
                .Include("~/Scripts/feedback.js"));

            bundles.Add(new StyleBundle("~/Bundles/css/feedback")
                .Include("~/Content/bootstrap.css")
                .Include("~/Content/main.css")
                .Include("~/Content/feedback.css"));

            // BundleTable.EnableOptimizations = true;
        }
    }
}
