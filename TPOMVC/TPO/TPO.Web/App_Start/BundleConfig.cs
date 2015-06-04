using System.Web;
using System.Web.Optimization;

namespace TPO.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js"));
            
            bundles.Add(new ScriptBundle("~/bundles/sidr").Include(
                      "~/Scripts/jquery.sidr.js"));

    
            bundles.Add(new ScriptBundle("~/bundles/common").Include(
                "~/Scripts/common.js"));
            bundles.Add(new ScriptBundle("~/bundles/editors").Include("~/Scripts/datagrid-editors.js"));

    
    

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/simple-sidebar.css",
                      "~/Content/rawmaterials.css",
                      "~/Content/jquery-ui-1.10.4.css",
                      "~/Content/sidr_dark.css",
                      "~/Content/tab-system.css",
                      "~/Content/RedHold-tab-system.css"));

        }
    }
}
