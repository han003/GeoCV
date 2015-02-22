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
                .Include("~/Scripts/jquery/*.js")
                .Include("~/Scripts/bootstrap/*.js")
                .Include("~/Scripts/home.js"));

            bundles.Add(new StyleBundle("~/Bundles/css/home")
                .Include("~/Content/bootstrap.css", new CssRewriteUrlTransform())
                .Include("~/Content/main.css", new CssRewriteUrlTransform())
                .Include("~/Content/home.css", new CssRewriteUrlTransform()));

            // Dashboard
            bundles.Add(new ScriptBundle("~/Bundles/js/dashboard")
                .Include("~/Scripts/jquery/*.js")
                .Include("~/Scripts/bootstrap/*.js")
                .Include("~/Scripts/dashboard.js"));

            bundles.Add(new StyleBundle("~/Bundles/css/dashboard")
                .Include("~/Content/bootstrap.css", new CssRewriteUrlTransform())
                .Include("~/Content/main.css", new CssRewriteUrlTransform())
                .Include("~/Content/dashboard.css", new CssRewriteUrlTransform()));

            // Cv
            bundles.Add(new ScriptBundle("~/Bundles/js/cv")
                .Include("~/Scripts/jquery/*.js")
                .Include("~/Scripts/bootstrap/*.js")
                .Include("~/Scripts/bootstrap3-typeahead.js")
                .Include("~/Scripts/cv/*.js"));

            bundles.Add(new StyleBundle("~/Bundles/css/cv")
                .Include("~/Content/bootstrap.css", new CssRewriteUrlTransform())
                .Include("~/Content/animate.css", new CssRewriteUrlTransform())
                .Include("~/Content/main.css", new CssRewriteUrlTransform())
                .Include("~/Content/cv.css", new CssRewriteUrlTransform()));

            // Search
            bundles.Add(new ScriptBundle("~/Bundles/js/search")
                .Include("~/Scripts/jquery/*.js")
                .Include("~/Scripts/bootstrap/*.js")
                .Include("~/Scripts/search.js"));

            bundles.Add(new StyleBundle("~/Bundles/css/search")
                .Include("~/Content/bootstrap.css", new CssRewriteUrlTransform())
                .Include("~/Content/main.css", new CssRewriteUrlTransform())
                .Include("~/Content/search.css", new CssRewriteUrlTransform()));

            // Customize
            bundles.Add(new ScriptBundle("~/Bundles/js/customize")
                .Include("~/Scripts/jquery/*.js")
                .Include("~/Scripts/bootstrap/*.js")
                .Include("~/Scripts/customize.js"));

            bundles.Add(new StyleBundle("~/Bundles/css/customize")
                .Include("~/Content/bootstrap.css", new CssRewriteUrlTransform())
                .Include("~/Content/main.css", new CssRewriteUrlTransform())
                .Include("~/Content/customize.css", new CssRewriteUrlTransform()));

            // Employees
            bundles.Add(new ScriptBundle("~/Bundles/js/employees")
                .Include("~/Scripts/jquery/*.js")
                .Include("~/Scripts/bootstrap/*.js")
                .Include("~/Scripts/employees.js"));

            bundles.Add(new StyleBundle("~/Bundles/css/employees")
                .Include("~/Content/bootstrap.css", new CssRewriteUrlTransform())
                .Include("~/Content/main.css", new CssRewriteUrlTransform())
                .Include("~/Content/employees.css", new CssRewriteUrlTransform()));

            // Register
            bundles.Add(new ScriptBundle("~/Bundles/js/register")
                .Include("~/Scripts/jquery/*.js")
                .Include("~/Scripts/bootstrap/*.js")
                .Include("~/Scripts/register.js"));

            bundles.Add(new StyleBundle("~/Bundles/css/register")
                .Include("~/Content/bootstrap.css", new CssRewriteUrlTransform())
                .Include("~/Content/main.css", new CssRewriteUrlTransform())
                .Include("~/Content/register.css", new CssRewriteUrlTransform()));

            // Login
            bundles.Add(new ScriptBundle("~/Bundles/js/login")
                .Include("~/Scripts/jquery/*.js")
                .Include("~/Scripts/bootstrap/*.js")
                .Include("~/Scripts/login.js"));

            bundles.Add(new StyleBundle("~/Bundles/css/login")
                .Include("~/Content/bootstrap.css", new CssRewriteUrlTransform())
                .Include("~/Content/main.css", new CssRewriteUrlTransform())
                .Include("~/Content/login.css", new CssRewriteUrlTransform()));

            // Manage
            bundles.Add(new ScriptBundle("~/Bundles/js/manage")
                .Include("~/Scripts/jquery/*.js")
                .Include("~/Scripts/bootstrap/*.js")
                .Include("~/Scripts/manage.js"));

            bundles.Add(new StyleBundle("~/Bundles/css/manage")
                .Include("~/Content/bootstrap.css", new CssRewriteUrlTransform())
                .Include("~/Content/main.css", new CssRewriteUrlTransform())
                .Include("~/Content/manage.css", new CssRewriteUrlTransform()));

            // Change Password
            bundles.Add(new ScriptBundle("~/Bundles/js/changepass")
                .Include("~/Scripts/jquery/*.js")
                .Include("~/Scripts/bootstrap/*.js")
                .Include("~/Scripts/changepass.js"));

            bundles.Add(new StyleBundle("~/Bundles/css/changepass")
                .Include("~/Content/bootstrap.css", new CssRewriteUrlTransform())
                .Include("~/Content/main.css", new CssRewriteUrlTransform())
                .Include("~/Content/changepass.css", new CssRewriteUrlTransform()));

            // BundleTable.EnableOptimizations = true;
        }
    }
}
