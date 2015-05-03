
using System.Web;
using System.Web.Optimization;

namespace GeoCV
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {

            // Home
            bundles.Add(new ScriptBundle("~/Bundles/js/home")
                .Include("~/Scripts/jquery/jquery-{version}.js")
                .Include("~/Scripts/theme/*.js")
                .Include("~/Scripts/home.js"));

            bundles.Add(new StyleBundle("~/Bundles/css/home")
                .Include("~/Content/theme/*.css")
                .Include("~/Content/plugins/*.css")
                .Include("~/Content/main.css")
                .Include("~/Content/home.css"));

            // Login
            bundles.Add(new StyleBundle("~/Bundles/css/login")
                .Include("~/Content/main.css")
                .Include("~/Content/login.css"));

            // Dashboard
            bundles.Add(new ScriptBundle("~/Bundles/js/dashboard")
                .Include("~/Scripts/jquery/jquery-{version}.js")
                .Include("~/Scripts/theme/*.js")
                .Include("~/Scripts/dashboard.js"));

            bundles.Add(new StyleBundle("~/Bundles/css/dashboard")
                .Include("~/Content/theme/*.css")
                .Include("~/Content/main.css")
                .Include("~/Content/dashboard.css"));

            // Ansatte
            bundles.Add(new ScriptBundle("~/Bundles/js/AnsatteIndex")
                .Include("~/Scripts/jquery/jquery-{version}.js")
                .Include("~/Scripts/plugins/*.js")
                .Include("~/Scripts/theme/*.js")
                .Include("~/Scripts/Ansatte/Index.js"));

            bundles.Add(new StyleBundle("~/Bundles/css/AnsatteIndex")
                .Include("~/Content/theme/*.css")
                .Include("~/Content/main.css")
                .Include("~/Content/Ansatte/Index.css"));

            /////////////////////

            bundles.Add(new ScriptBundle("~/Bundles/js/AnsatteLeggTil")
                .Include("~/Scripts/jquery/jquery-{version}.js")
                .Include("~/Scripts/plugins/*.js")
                .Include("~/Scripts/theme/*.js")
                .Include("~/Scripts/Ansatte/LeggTil.js"));

            bundles.Add(new StyleBundle("~/Bundles/css/AnsatteLeggTil")
                .Include("~/Content/theme/*.css")
                .Include("~/Content/main.css")
                .Include("~/Content/Ansatte/LeggTil.css"));

            // Admin Projects
            bundles.Add(new ScriptBundle("~/Bundles/js/projects")
                .Include("~/Scripts/jquery/jquery-{version}.js")
                .Include("~/Scripts/plugins/*.js")
                .Include("~/Scripts/theme/*.js")
                .Include("~/Scripts/projects.js"));

            bundles.Add(new StyleBundle("~/Bundles/css/projects")
                .Include("~/Content/theme/*.css")
                .Include("~/Content/main.css")
                .Include("~/Content/projects.css"));

            // Register
            bundles.Add(new ScriptBundle("~/Bundles/js/register")
                .Include("~/Scripts/jquery/jquery-{version}.js")
                .Include("~/Scripts/theme/*.js")
                .Include("~/Scripts/register.js"));

            bundles.Add(new StyleBundle("~/Bundles/css/register")
                .Include("~/Content/theme/*.css")
                .Include("~/Content/main.css")
                .Include("~/Content/register.css"));

            // Manage
            bundles.Add(new ScriptBundle("~/Bundles/js/manage")
                .Include("~/Scripts/jquery/jquery-{version}.js")
                .Include("~/Scripts/theme/*.js")
                .Include("~/Scripts/manage.js"));

            bundles.Add(new StyleBundle("~/Bundles/css/manage")
                .Include("~/Content/theme/*.css")
                .Include("~/Content/main.css")
                .Include("~/Content/manage.css"));

            // Change Password
            bundles.Add(new ScriptBundle("~/Bundles/js/changepass")
                .Include("~/Scripts/jquery/jquery-{version}.js")
                .Include("~/Scripts/theme/*.js")
                .Include("~/Scripts/changepass.js"));

            bundles.Add(new StyleBundle("~/Bundles/css/changepass")
                .Include("~/Content/theme/*.css")
                .Include("~/Content/main.css")
                .Include("~/Content/changepass.css"));

            // Personal
            bundles.Add(new ScriptBundle("~/Bundles/js/personal")
                .Include("~/Scripts/jquery/jquery-{version}.js")
                .Include("~/Scripts/jquery-ui/*.js")
                .Include("~/Scripts/theme/*.js")
                .Include("~/Scripts/personal.js"));
            

            bundles.Add(new StyleBundle("~/Bundles/css/personal")
                .Include("~/Content/theme/*.css")
                .Include("~/Content/jquery-ui/*.css")
                .Include("~/Content/main.css")
                .Include("~/Content/personal.css"));

            // Expertise
            bundles.Add(new ScriptBundle("~/Bundles/js/expertise")
                .Include("~/Scripts/jquery/jquery-{version}.js")
                .Include("~/Scripts/theme/*.js")
                .Include("~/Scripts/expertise.js"));

            bundles.Add(new StyleBundle("~/Bundles/css/expertise")
                .Include("~/Content/theme/*.css")
                .Include("~/Content/expertise.css")
                .Include("~/Content/main.css"));

            // Education
            bundles.Add(new ScriptBundle("~/Bundles/js/education")
                .Include("~/Scripts/jquery/jquery-{version}.js")
                .Include("~/Scripts/plugins/*.js")
                .Include("~/Scripts/theme/*.js")
                .Include("~/Scripts/education.js"));

            bundles.Add(new StyleBundle("~/Bundles/css/education")
                .Include("~/Content/theme/*.css")
                .Include("~/Content/education.css")
                .Include("~/Content/main.css"));

            // Work
            bundles.Add(new ScriptBundle("~/Bundles/js/work")
                .Include("~/Scripts/jquery/jquery-{version}.js")
                .Include("~/Scripts/plugins/*.js")
                .Include("~/Scripts/theme/*.js")
                .Include("~/Scripts/work.js"));

            bundles.Add(new StyleBundle("~/Bundles/css/work")
                .Include("~/Content/theme/*.css")
                .Include("~/Content/work.css")
                .Include("~/Content/main.css"));

            // Mine Prosjekter
            bundles.Add(new ScriptBundle("~/Bundles/js/MineProsjekterIndex")
                .Include("~/Scripts/jquery/jquery-{version}.js")
                .Include("~/Scripts/jquery-ui/*.js")
                .Include("~/Scripts/plugins/*.js")
                .Include("~/Scripts/theme/*.js")
                .Include("~/Scripts/MineProsjekter/Index.js"));

            bundles.Add(new StyleBundle("~/Bundles/css/MineProsjekterIndex")
                .Include("~/Content/theme/*.css")
                .Include("~/Content/jquery-ui/*.css")
                .Include("~/Content/main.css")
                .Include("~/Content/MineProsjekter/Index.css"));

            //////////////////////

            bundles.Add(new ScriptBundle("~/Bundles/js/MineProsjekterLeggTil")
                .Include("~/Scripts/jquery/jquery-{version}.js")
                .Include("~/Scripts/jquery-ui/*.js")
                .Include("~/Scripts/plugins/*.js")
                .Include("~/Scripts/theme/*.js")
                .Include("~/Scripts/MineProsjekter/LeggTil.js"));

            bundles.Add(new StyleBundle("~/Bundles/css/MineProsjekterLeggTil")
                .Include("~/Content/theme/*.css")
                .Include("~/Content/jquery-ui/*.css")
                .Include("~/Content/main.css")
                .Include("~/Content/MineProsjekter/LeggTil.css"));

            // Teknisk profil
            bundles.Add(new ScriptBundle("~/Bundles/js/TekniskProfilIndex")
                .Include("~/Scripts/jquery/jquery-{version}.js")
                .Include("~/Scripts/plugins/*.js")
                .Include("~/Scripts/theme/*.js")
                .Include("~/Scripts/TekniskProfil/Index.js"));

            bundles.Add(new StyleBundle("~/Bundles/css/TekniskProfilIndex")
                .Include("~/Content/theme/*.css")
                .Include("~/Content/main.css")
                .Include("~/Content/TekniskProfil/Index.css"));

            //////////////////////////////////////

            bundles.Add(new ScriptBundle("~/Bundles/js/TekniskProfilLeggTil")
                .Include("~/Scripts/jquery/jquery-{version}.js")
                .Include("~/Scripts/plugins/*.js")
                .Include("~/Scripts/theme/*.js")
                .Include("~/Scripts/TekniskProfil/LeggTil.js"));

            bundles.Add(new StyleBundle("~/Bundles/css/TekniskProfilLeggTil")
                .Include("~/Content/theme/*.css")
                .Include("~/Content/main.css")
                .Include("~/Content/TekniskProfil/LeggTil.css"));

            // Settings
            bundles.Add(new ScriptBundle("~/Bundles/js/settings")
                .Include("~/Scripts/jquery/jquery-{version}.js")
                .Include("~/Scripts/plugins/*.js")
                .Include("~/Scripts/theme/*.js")
                .Include("~/Scripts/settings.js"));

            bundles.Add(new StyleBundle("~/Bundles/css/settings")
                .Include("~/Content/theme/*.css")
                .Include("~/Content/plugins/*.css")
                .Include("~/Content/main.css")
                .Include("~/Content/settings.css"));

            // Database
            bundles.Add(new ScriptBundle("~/Bundles/js/database")
                .Include("~/Scripts/jquery/jquery-{version}.js")
                .Include("~/Scripts/plugins/*.js")
                .Include("~/Scripts/theme/*.js")
                .Include("~/Scripts/database.js"));

            bundles.Add(new StyleBundle("~/Bundles/css/database")
                .Include("~/Content/theme/*.css")
                .Include("~/Content/main.css")
                .Include("~/Content/database.css"));

            // BundleTable.EnableOptimizations = true;
        }
    }
}
