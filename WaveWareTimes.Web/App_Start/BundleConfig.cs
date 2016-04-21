using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Optimization;

namespace WaveWareTimes.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                "~/Scripts/jquery.unobtrusive*",
                "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/knockout").Include(
                "~/Scripts/knockout-{version}.js",
                "~/Scripts/knockout.validation.js"));

            //bundles.Add(new ScriptBundle("~/bundles/app").Include(
            //    "~/Scripts/sammy-{version}.js",
            //    "~/Scripts/app/common.js",
            //    "~/Scripts/app/app.datamodel.js",
            //    "~/Scripts/app/app.viewmodel.js",
            //    "~/Scripts/app/home.viewmodel.js",
            //    "~/Scripts/app/_run.js"));
            bundles.Add(new ScriptBundle("~/bundles/kendo").Include(
                    "~/Scripts/kendo/kendo.all.min.js",                    
                    "~/Scripts/kendo/knockout/knockout-kendo.min.js",
                    "~/Scripts/kendo/kendo.timezones.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/app").Include(
                "~/Scripts/app/common.js",
                "~/Scripts/app/ajaxPrefilters.js",
                "~/Scripts/app/app.datamodel.js",
                "~/Scripts/app/app.viewmodel.js",
                "~/Scripts/app/home.viewmodel.js",
                "~/Scripts/app/login.viewmodel.js",
                "~/Scripts/app/register.viewmodel.js",
                "~/Scripts/app/registerExternal.viewmodel.js",
                "~/Scripts/app/manage.viewmodel.js",
                "~/Scripts/app/userInfo.viewmodel.js",
                "~/Scripts/app/workTimeRecord.viewmodel.js",
                "~/Scripts/app/_run.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Scripts/bootstrap.js",
                "~/Scripts/respond.js"));




            bundles.Add(new StyleBundle("~/Content/kendo/bootstrap/css").Include(                
                 "~/Content/kendo/kendo.common.min.css",
                 "~/Content/kendo/kendo.common-bootstrap.min.css",
                 "~/Content/kendo/kendo.bootstrap.min.css",
                 "~/Content/kendo/kendo.bootstrap.mobile.min.css"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                 "~/Content/bootstrap.css",
                 "~/Content/bootstrap.theme.css",
                 "~/Content/Site.css"));
        }
    }
}
