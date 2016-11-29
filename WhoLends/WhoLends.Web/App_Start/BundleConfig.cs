using System.Web.Optimization;

namespace WhoLends
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            //Scripts

            #region JQuery
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery-{version}.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));
            #endregion

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/moment.js",
                      "~/Scripts/moment.min.js",
                      "~/Scripts/moment-with-locales.js",
                      "~/Scripts/moment-with-locales.min.js",
                      "~/Scripts/locate/de.js",
                      "~/Scripts/bootstrap-datetimepicker.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                    "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/mvcGrid").Include(
                    "~/Scripts/gridmvc.js",
                    "~/Scripts/gridmvc.min.js",
                    "~/Content/Gridmvc.css"
                    ));

            //styles

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/bootstrap-datetimepicker.css",
                      "~/Content/site.css",
                      "~/Content/Gridmvc.css",
                      "~/Content/jquery.jqGrid/ui.jqgrid.css",
                      "~/Content/font-awesome.css"));            
        }
    }
}
