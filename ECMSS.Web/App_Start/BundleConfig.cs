using ECMSS.Utilities;
using System.Web.Optimization;
using StyleBundle = ECMSS.Web.Extensions.CustomBundles.StyleBundle;

namespace ECMSS.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/css/style-bundles")
               .Include("~/assets/libs/bootstrap-3.3.7-dist/css/bootstrap.min.css", new CssRewriteUrlTransform())
               .Include("~/assets/libs/DataTables/DataTables-1.10.23/css/jquery.dataTables.min.css", new CssRewriteUrlTransform())
               .Include("~/assets/libs/DataTables/DataTables-1.10.23/css/dataTables.bootstrap.min.css", new CssRewriteUrlTransform())
               .Include("~/assets/libs/jquery-ui-1.12.1/jquery-ui.min.css", new CssRewriteUrlTransform())
               .Include("~/assets/libs/fontawesome-free-5.15.1/css/all.min.css", new CssRewriteUrlTransform())
               .Include("~/assets/css/style.css", new CssRewriteUrlTransform()));

            bundles.Add(new ScriptBundle("~/assets/script-bundles").Include(
                "~/assets/libs/jquery-3.5.1.min.js",
                "~/assets/libs/sweetalert.min.js",
                "~/assets/libs/axios-0.21.1/dist/axios.min.js",
                "~/assets/libs/vanilla-router-v.1.2.7/dist/vanilla-router.min.js",
                "~/assets/libs/DataTables/DataTables-1.10.23/js/jquery.dataTables.min.js",
                "~/assets/libs/jquery-ui-1.12.1/jquery-ui.min.js",
                "~/assets/libs/bootstrap-3.3.7-dist/js/bootstrap.min.js",
                "~/assets/libs/moment.min.js",
                "~/assets/js/config.js",
                "~/assets/js/main.js",
                "~/assets/js/initData.js",
                "~/assets/routes/app.js"));

            BundleTable.EnableOptimizations = bool.Parse(ConfigHelper.Read("EnableBundles"));
        }
    }
}