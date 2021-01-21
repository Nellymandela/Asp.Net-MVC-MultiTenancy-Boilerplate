using System.Web;
using System.Web.Optimization;

namespace App.Web
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

            //BundleTable.EnableOptimizations = true;
            bundles.Add(new StyleBundle("~/assets/metronic/css").Include(
                        "~/assets/plugins/global/plugins.bundle.css",
                        "~/assets/plugins/custom/prismjs/prismjs.bundle.css",
                        "~/assets/css/style.bundle.css",
                        "~/assets/css/themes/layout/header/base/light.css",
                        "~/assets/css/themes/layout/header/menu/light.css",
                        "~/assets/css/themes/layout/brand/light.css",
                        "~/assets/css/themes/layout/aside/light.css"));

            bundles.Add(new ScriptBundle("~/assets/metronic/js").Include(
                        "~/assets/plugins/global/plugins.bundle.js",
                        "~/assets/plugins/custom/prismjs/prismjs.bundle.js",
                        "~/assets/js/scripts.bundle.js"));

            bundles.Add(new StyleBundle("~/assets/ladda/css").Include(
                        "~/Content/ui-assets/global_assets/css/ladda/ladda.min.css",
                        "~/Content/ui-assets/global_assets/css/ladda/ladda-themeless.min.css"));

            bundles.Add(new ScriptBundle("~/assets/ladda/js").Include(
                        "~/Content/ui-assets/global_assets/js/plugins/ladda/ladda.jquery.min.js",
                        "~/Content/ui-assets/global_assets/js/plugins/ladda/spin.min.js",
                        "~/Content/ui-assets/global_assets/js/plugins/ladda/ladda.min.js"));

            bundles.Add(new StyleBundle("~/assets/datatables/css").Include(
                        "~/assets/plugins/custom/datatables/datatables.bundle.css"));

            bundles.Add(new ScriptBundle("~/assets/datatables/js").Include(
                        "~/assets/plugins/custom/datatables/datatables.bundle.js",
                        "~/assets/js/pages/crud/datatables/extensions/responsive.js"));

            bundles.Add(new StyleBundle("~/assets/jstree/css").Include(
                        "~/assets/plugins/custom/jstree/jstree.bundle.css"));

            bundles.Add(new ScriptBundle("~/assets/jstree/js").Include(
                        "~/assets/plugins/custom/jstree/jstree.bundle.js",
                        "~/assets/js/pages/features/miscellaneous/treeview.js"));
        }
    }
}
