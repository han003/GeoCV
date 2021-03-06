﻿
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
                .Include("~/Scripts/jquery/jquery.validate.js")
                .Include("~/Scripts/jquery/jquery.validate.unobtrusive.js")
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
                .Include("~/Scripts/jquery/jquery.validate.js")
                .Include("~/Scripts/jquery/jquery.validate.unobtrusive.js")
                .Include("~/Scripts/theme/*.js")
                .Include("~/Scripts/dashboard.js"));

            bundles.Add(new StyleBundle("~/Bundles/css/dashboard")
                .Include("~/Content/theme/*.css")
                .Include("~/Content/main.css")
                .Include("~/Content/dashboard.css"));

            // Personal
            bundles.Add(new ScriptBundle("~/Bundles/js/personal")
                .Include("~/Scripts/jquery/jquery-{version}.js")
                .Include("~/Scripts/jquery/jquery.validate.js")
                .Include("~/Scripts/jquery/jquery.validate.unobtrusive.js")
                .Include("~/Scripts/jquery-ui/*.js")
                .Include("~/Scripts/theme/*.js")
                .Include("~/Scripts/personal.js"));


            bundles.Add(new StyleBundle("~/Bundles/css/personal")
                .Include("~/Content/theme/*.css")
                .Include("~/Content/jquery-ui/*.css")
                .Include("~/Content/main.css")
                .Include("~/Content/personal.css"));

            // Ansatte
            bundles.Add(new ScriptBundle("~/Bundles/js/AnsatteIndex")
                .Include("~/Scripts/jquery/jquery-{version}.js")
                .Include("~/Scripts/jquery/jquery.validate.js")
                .Include("~/Scripts/jquery/jquery.validate.unobtrusive.js")
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
                .Include("~/Scripts/jquery/jquery.validate.js")
                .Include("~/Scripts/jquery/jquery.validate.unobtrusive.js")
                .Include("~/Scripts/plugins/*.js")
                .Include("~/Scripts/theme/*.js")
                .Include("~/Scripts/Ansatte/LeggTil.js"));

            bundles.Add(new StyleBundle("~/Bundles/css/AnsatteLeggTil")
                .Include("~/Content/theme/*.css")
                .Include("~/Content/main.css")
                .Include("~/Content/Ansatte/LeggTil.css"));

            // Prosjekter
            bundles.Add(new ScriptBundle("~/Bundles/js/ProsjekterIndex")
                .Include("~/Scripts/jquery/jquery-{version}.js")
                .Include("~/Scripts/jquery/jquery.validate.js")
                .Include("~/Scripts/jquery/jquery.validate.unobtrusive.js")
                .Include("~/Scripts/plugins/*.js")
                .Include("~/Scripts/theme/*.js")
                .Include("~/Scripts/Prosjekter/Index.js"));

            bundles.Add(new StyleBundle("~/Bundles/css/ProsjekterIndex")
                .Include("~/Content/theme/*.css")
                .Include("~/Content/plugins/bootstrap-switch.css")
                .Include("~/Content/main.css")
                .Include("~/Content/Prosjekter/Index.css"));

            //////////////////////////////////////

            bundles.Add(new ScriptBundle("~/Bundles/js/ProsjekterLeggTil")
                .Include("~/Scripts/jquery/jquery-{version}.js")
                .Include("~/Scripts/jquery/jquery.validate.js")
                .Include("~/Scripts/jquery/jquery.validate.unobtrusive.js")
                .Include("~/Scripts/plugins/*.js")
                .Include("~/Scripts/theme/*.js")
                .Include("~/Scripts/Prosjekter/LeggTil.js"));

            bundles.Add(new StyleBundle("~/Bundles/css/ProsjekterLeggTil")
                .Include("~/Content/theme/*.css")
                .Include("~/Content/main.css")
                .Include("~/Content/Prosjekter/LeggTil.css"));

            // Register
            bundles.Add(new ScriptBundle("~/Bundles/js/register")
                .Include("~/Scripts/jquery/jquery-{version}.js")
                .Include("~/Scripts/jquery/jquery.validate.js")
                .Include("~/Scripts/jquery/jquery.validate.unobtrusive.js")
                .Include("~/Scripts/theme/*.js")
                .Include("~/Scripts/register.js"));

            bundles.Add(new StyleBundle("~/Bundles/css/register")
                .Include("~/Content/theme/*.css")
                .Include("~/Content/main.css")
                .Include("~/Content/register.css"));

            // Manage
            bundles.Add(new ScriptBundle("~/Bundles/js/manage")
                .Include("~/Scripts/jquery/jquery-{version}.js")
                .Include("~/Scripts/jquery/jquery.validate.js")
                .Include("~/Scripts/jquery/jquery.validate.unobtrusive.js")
                .Include("~/Scripts/theme/*.js")
                .Include("~/Scripts/manage.js"));

            bundles.Add(new StyleBundle("~/Bundles/css/manage")
                .Include("~/Content/theme/*.css")
                .Include("~/Content/main.css")
                .Include("~/Content/manage.css"));

            // Change Password
            bundles.Add(new ScriptBundle("~/Bundles/js/changepass")
                .Include("~/Scripts/jquery/jquery-{version}.js")
                .Include("~/Scripts/jquery/jquery.validate.js")
                .Include("~/Scripts/jquery/jquery.validate.unobtrusive.js")
                .Include("~/Scripts/theme/*.js")
                .Include("~/Scripts/changepass.js"));

            bundles.Add(new StyleBundle("~/Bundles/css/changepass")
                .Include("~/Content/theme/*.css")
                .Include("~/Content/main.css")
                .Include("~/Content/changepass.css"));

            // Expertise
            bundles.Add(new ScriptBundle("~/Bundles/js/expertise")
                .Include("~/Scripts/jquery/jquery-{version}.js")
                .Include("~/Scripts/jquery/jquery.validate.js")
                .Include("~/Scripts/jquery/jquery.validate.unobtrusive.js")
                .Include("~/Scripts/theme/*.js")
                .Include("~/Scripts/expertise.js"));

            bundles.Add(new StyleBundle("~/Bundles/css/expertise")
                .Include("~/Content/theme/*.css")
                .Include("~/Content/expertise.css")
                .Include("~/Content/main.css"));

            // Utdannelse
            bundles.Add(new ScriptBundle("~/Bundles/js/utdannelse")
                .Include("~/Scripts/jquery/jquery-{version}.js")
                .Include("~/Scripts/jquery/jquery.validate.js")
                .Include("~/Scripts/jquery/jquery.validate.unobtrusive.js")
                .Include("~/Scripts/plugins/*.js")
                .Include("~/Scripts/theme/*.js")
                .Include("~/Scripts/utdannelse.js"));

            bundles.Add(new StyleBundle("~/Bundles/css/utdannelse")
                .Include("~/Content/theme/*.css")
                .Include("~/Content/main.css")
            .Include("~/Content/utdannelse.css"));

            // Arbeidserfaring
            bundles.Add(new ScriptBundle("~/Bundles/js/arbeidserfaring")
                .Include("~/Scripts/jquery/jquery-{version}.js")
                .Include("~/Scripts/jquery/jquery.validate.js")
                .Include("~/Scripts/jquery/jquery.validate.unobtrusive.js")
                .Include("~/Scripts/plugins/*.js")
                .Include("~/Scripts/theme/*.js")
                .Include("~/Scripts/arbeidserfaring.js"));

            bundles.Add(new StyleBundle("~/Bundles/css/arbeidserfaring")
                .Include("~/Content/theme/*.css")
                .Include("~/Content/arbeidserfaring.css")
                .Include("~/Content/main.css"));

            // Mine Prosjekter
            bundles.Add(new ScriptBundle("~/Bundles/js/MineProsjekterIndex")
                .Include("~/Scripts/jquery/jquery-{version}.js")
                .Include("~/Scripts/jquery/jquery.validate.js")
                .Include("~/Scripts/jquery/jquery.validate.unobtrusive.js")
                .Include("~/Scripts/jquery-ui/*.js")
                .Include("~/Scripts/plugins/*.js")
                .Include("~/Scripts/theme/*.js")
                .Include("~/Scripts/MineProsjekter/Index.js"));

            bundles.Add(new StyleBundle("~/Bundles/css/MineProsjekterIndex")
                .Include("~/Content/theme/*.css")
                .Include("~/Content/jquery-ui/*.css")
                .Include("~/Content/plugins/*.css")
                .Include("~/Content/main.css")
                .Include("~/Content/MineProsjekter/Index.css"));

            //////////////////////

            bundles.Add(new ScriptBundle("~/Bundles/js/MineProsjekterLeggTil")
                .Include("~/Scripts/jquery/jquery-{version}.js")
                .Include("~/Scripts/jquery/jquery.validate.js")
                .Include("~/Scripts/jquery/jquery.validate.unobtrusive.js")
                .Include("~/Scripts/jquery-ui/*.js")
                .Include("~/Scripts/plugins/*.js")
                .Include("~/Scripts/theme/*.js")
                .Include("~/Scripts/MineProsjekter/LeggTil.js"));

            bundles.Add(new StyleBundle("~/Bundles/css/MineProsjekterLeggTil")
                .Include("~/Content/theme/*.css")
                .Include("~/Content/jquery-ui/*.css")
                .Include("~/Content/main.css")
                .Include("~/Content/MineProsjekter/LeggTil.css"));

            // Tekniske profiler
            bundles.Add(new ScriptBundle("~/Bundles/js/TekniskeProfilerIndex")
                .Include("~/Scripts/jquery/jquery-{version}.js")
                .Include("~/Scripts/jquery/jquery.validate.js")
                .Include("~/Scripts/jquery/jquery.validate.unobtrusive.js")
                .Include("~/Scripts/plugins/*.js")
                .Include("~/Scripts/theme/*.js")
                .Include("~/Scripts/TekniskeProfiler/Index.js"));

            bundles.Add(new StyleBundle("~/Bundles/css/TekniskeProfilerIndex")
                .Include("~/Content/theme/*.css")
                .Include("~/Content/main.css")
                .Include("~/Content/TekniskeProfiler/Index.css"));

            //////////////////////////////////////

            bundles.Add(new ScriptBundle("~/Bundles/js/TekniskeProfilerLeggTil")
                .Include("~/Scripts/jquery/jquery-{version}.js")
                .Include("~/Scripts/jquery/jquery.validate.js")
                .Include("~/Scripts/jquery/jquery.validate.unobtrusive.js")
                .Include("~/Scripts/plugins/*.js")
                .Include("~/Scripts/theme/*.js")
                .Include("~/Scripts/TekniskeProfiler/LeggTil.js"));

            bundles.Add(new StyleBundle("~/Bundles/css/TekniskeProfilerLeggTil")
                .Include("~/Content/theme/*.css")
                .Include("~/Content/main.css")
                .Include("~/Content/TekniskeProfiler/LeggTil.css"));

            // Settings
            bundles.Add(new ScriptBundle("~/Bundles/js/settings")
                .Include("~/Scripts/jquery/jquery-{version}.js")
                .Include("~/Scripts/jquery/jquery.validate.js")
                .Include("~/Scripts/jquery/jquery.validate.unobtrusive.js")
                .Include("~/Scripts/plugins/*.js")
                .Include("~/Scripts/theme/*.js")
                .Include("~/Scripts/settings.js"));

            bundles.Add(new StyleBundle("~/Bundles/css/settings")
                .Include("~/Content/theme/*.css")
                .Include("~/Content/plugins/*.css")
                .Include("~/Content/main.css")
                .Include("~/Content/settings.css"));

            // Database
            bundles.Add(new ScriptBundle("~/Bundles/js/DatabaseRediger")
                .Include("~/Scripts/jquery/jquery-{version}.js")
                .Include("~/Scripts/jquery/jquery.validate.js")
                .Include("~/Scripts/jquery/jquery.validate.unobtrusive.js")
                .Include("~/Scripts/plugins/*.js")
                .Include("~/Scripts/theme/*.js")
                .Include("~/Scripts/Database/Rediger.js"));

            bundles.Add(new StyleBundle("~/Bundles/css/DatabaseRediger")
                .Include("~/Content/theme/*.css")
                .Include("~/Content/main.css")
                .Include("~/Content/Database/Rediger.css"));

            /////////////////////////////////////////////

            bundles.Add(new ScriptBundle("~/Bundles/js/DatabaseLeggTil")
                .Include("~/Scripts/jquery/jquery-{version}.js")
                .Include("~/Scripts/jquery/jquery.validate.js")
                .Include("~/Scripts/jquery/jquery.validate.unobtrusive.js")
                .Include("~/Scripts/plugins/*.js")
                .Include("~/Scripts/theme/*.js")
                .Include("~/Scripts/Database/LeggTil.js"));

            bundles.Add(new StyleBundle("~/Bundles/css/DatabaseLeggTil")
                .Include("~/Content/theme/*.css")
                .Include("~/Content/main.css")
                .Include("~/Content/Database/LeggTil.css"));

            // BundleTable.EnableOptimizations = true;
        }
    }
}
