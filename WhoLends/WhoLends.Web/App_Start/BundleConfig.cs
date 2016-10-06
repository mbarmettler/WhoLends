using System.Web;
using System.Web.Optimization;

namespace WhoLends
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            //Scripts

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquerygrid").Include(
                        "~/Scripts/jquery-ui-{version}.js",
                        "~/Scripts/jquery-ui-1.10.0.js",
                        "~/Scripts/jquery.jqGrid.js",
                        "~/Scripts/i18nD/grid.locale-de.js"
                        ));            

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap-datepicker.js",
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            //styles

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap-datepicker.css",
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/jquery.jqGrid/ui.jqgrid.css"));            
        }
    }
}
