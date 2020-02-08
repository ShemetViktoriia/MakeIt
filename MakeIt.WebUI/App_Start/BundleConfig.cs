using System.Web;
using System.Web.Optimization;

namespace MakeIt.WebUI
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.UseCdn = true;
            bundles.Add(new StyleBundle("~/Content/commonStyles").Include(
                    "~/Content/css/bootstrap.min.css",
                    "~/Content/css/sb-admin-2.css",
                    "~/Content/css/plugins/metisMenu/metisMenu.min.css"));

            //bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
            //            "~/Scripts/jquery.js"));

            //bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
            //            "~/Scripts/jquery-ui-{version}.js"));

            //bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
            //            "~/Scripts/jquery.validate.min.js",
            //            "~/Scripts/jquery.validate.unobtrusive.min.js"));

            //bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
            //          "~/Scripts/bootstrap.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/metisMenu").Include(
                      "~/Scripts/plugins/metisMenu/metisMenu.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/sb-admin-2").Include(
                     "~/Scripts/sb-admin-2.js"));         
        }
    }
}
