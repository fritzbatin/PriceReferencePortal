using System.Web;
using System.Web.Optimization;

namespace PriceReferencePortal
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            //bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
            //            "~/Scripts/jquery-{version}.js"));

            //bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
            //            "~/Scripts/jquery.validate*"));

            //// Use the development version of Modernizr to develop with and learn from. Then, when you're
            //// ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            //bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
            //            "~/Scripts/modernizr-*"));

            //bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
            //          "~/Scripts/bootstrap.js"));

            //bundles.Add(new StyleBundle("~/Content/css").Include(
            //          "~/Content/bootstrap.css",
            //          "~/Content/site.css"));
            //}


            //Create bundel for jQueryUI  
            //js  
            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                      "~/Scripts/jquery-ui-{version}.js"));
            //css  
            bundles.Add(new StyleBundle("~/Content/cssjqryUi").Include(
                   "~/Content/jquery-ui.css"));

            bundles.Add(new ScriptBundle("~/Content/vendor/jquery").Include(
                    "~/Content/vendor/jquery.js",
                     "~/Content/vendor/jquery.min.js",
                      "~/Content/vendor/jquery.slim.js",
                      "~/Content/vendor/jquery.slim.min.js"));

            bundles.Add(new ScriptBundle("~/Content/vendor/jquery-easing").Include(
                        "~/Content/vendor/jquery-easing/jquery.easing.compatibility.js",
                         "~/Content/vendor/jquery-easing/jquery.easing.js",
                         "~/Content/vendor/jquery-easing/jquery.easing.min.js"));

            ////Use the development version of Modernizr to develop with and learn from.Then, when you're
            //// ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                    "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/Content/vendor/bootstrap/js").Include(
            "~/Content/vendor/bootstrap/js/bootstrap.bundle.js",
            "~/Content/vendor/bootstrap/js/bootstrap.bundle.min.js",
           "~/Content/vendor/bootstrap/js/bootstrap.js",
           "~/Content/vendor/bootstrap/js/bootstrap.min.js"));

            bundles.Add(new ScriptBundle("~/Content/js").Include(
                  "~/Content/js/sb-admin-2.js",
                   "~/Content/js/sb-admin-2.min.js"));

        bundles.Add(new StyleBundle("~/Content/css").Include(
                  "~/Content/css/sb-admin-2.css",
                  "~/Content/css/sb-admin-2.min.css"));
        }

}
}
