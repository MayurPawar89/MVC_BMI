using System.Web;
using System.Web.Optimization;

namespace MVC_BMEApplication
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));
            bundles.Add(new ScriptBundle("~/bundles/moment").Include(
            "~/Scripts/moment*"));

            bundles.Add(new ScriptBundle("~/bundles/AppJs").Include(
                      "~/Scripts/bootstrap.min.js",
                      "~/Scripts/jquery.slimscroll.js",
                      "~/Scripts/jquery.dataTables.min.js",
                      "~/Scripts/dataTables.bootstrap.min.js",
                      "~/Scripts/dataTables.buttons.min.js",
                      "~/Scripts/buttons.bootstrap.min.js",
                      "~/Scripts/jszip.min.js",
                      //"~/Scripts/pdfmake.min.js",
                      "~/Scripts/vfs_fonts.js",
                      "~/Scripts/buttons.html5.min.js",
                      "~/Scripts/bootstrap-datetimepicker.js",
                      "~/Scripts/bootstrap-datetimepicker-init.js",
                      "~/Scripts/app.js",
                      "~/Scripts/wow.min.js",
                      "~/Scripts/tilt.jquery.min.js"));

            bundles.Add(new StyleBundle("~/Content/AppCss").Include(
                      "~/Content/font-awesome.min.css",
                      "~/Content/bootstrap.min.css",
                      //"~/Content/bootstrap-datetimepicker.min.css",
                      "~/Content/dataTables.bootstrap.min.css",
                      "~/Content/buttons.dataTables.min.css",
                      "~/Content/animate.css",
                      "~/Content/custom.css",
                      "~/Content/util.css",
                      "~/Content/extra_pages.css"));

        }
    }
}
