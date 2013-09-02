using System.Web.Optimization;
using Bootstrap.Extensions.StartupTasks;
using System.Web.Mvc;

namespace YangKai.BlogEngine.Web.Mvc.BootStrapper
{
    public class BundlesConfig : IStartupTask
    {
        public void Run()
        {
            var common = new[]
            {
                "~/Content/js/vendor/*.js",
                "~/Content/js/angular.js",
                "~/Content/js/angular-resource.js",
                "~/Content/plugin/syntaxhighlighter_3.0.83/scripts/shCore.js",
                "~/Content/plugin/syntaxhighlighter_3.0.83/scripts/shBrushCSharp.js",
                "~/Content/plugin/syntaxhighlighter_3.0.83/scripts/shBrushCss.js",
                "~/Content/plugin/syntaxhighlighter_3.0.83/scripts/shBrushJScript.js",
                "~/Content/plugin/syntaxhighlighter_3.0.83/scripts/shBrushXml.js",
                "~/Content/js/main.js",
                "~/Content/js/filters.js",
                "~/Content/js/directives/*.js",
                "~/Content/js/directives/custom/*.js",
                "~/Content/js/services/*.js",
            };

            BundleTable.Bundles.Add(new ScriptBundle("~/js").Include(common).Include(
                "~/Content/js/controllers/*.js",
                "~/Content/js/app.js"
                ));

            BundleTable.Bundles.Add(new ScriptBundle("~/admin/js").Include(common).Include(
                "~/Content/js/jquery-ui-{version}.js",
                "~/Content/js/jquery.fileupload.js",
                "~/Content/js/controllers/GlobalController.js",
                "~/Content/js/controllers/LoginController.js",
                "~/Content/js/controllers/admin/*.js",
                "~/Content/js/app.js"
                ));

            BundleTable.Bundles.Add(new StyleBundle("~/Content/style/css").Include(
                "~/Content/css/vendor/*.css",
                "~/Content/plugin/syntaxhighlighter_3.0.83/styles/shCoreDefault.css",
                "~/Content/css/style.css"
                ));

            //BundleTable.EnableOptimizations = true;
        }

        public void Reset()
        {
        }
    }
}