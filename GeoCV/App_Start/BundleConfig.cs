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

            // Cv
            bundles.Add(new ScriptBundle("~/Bundles/js/cv")
                .Include("~/Scripts/jquery/jquery-{version}.js")
                .Include("~/Scripts/jquery/jquery.validate.js")
                .Include("~/Scripts/bootstrap/*.js")
                .Include("~/Scripts/bootstrap3-typeahead.js")
                .Include("~/Scripts/cv/*.js"));

            bundles.Add(new StyleBundle("~/Bundles/css/cv")
                .Include("~/Content/bootstrap.css")
                .Include("~/Content/animate.css")
                .Include("~/Content/main.css")
                .Include("~/Content/cv.css"));

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

            BundleTable.EnableOptimizations = true;
        }
    }
}
