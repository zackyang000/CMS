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
                "~/Content/js/jquery-{version}.js",
                "~/Content/js/jquery.lazyload.js",
                "~/Content/js/angular.js",
                "~/Content/js/angular-resource.js",
                "~/Content/js/bootstrap.js",
                "~/Content/js/messenger.js",
                "~/Content/js/messenger-theme-future.js",
                "~/Content/js/buttons.js",
                "~/Content/js/md5.js",
                "~/Content/js/uuid.js",
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
                "~/Content/css/html5reset.css",
                "~/Content/css/bootstrap.css",
                "~/Content/css/font-awesome.css",
                "~/Content/css/messenger.css",
                "~/Content/css/messenger-theme-future.css",
                "~/Content/css/buttons.css",
                "~/Content/css/ng-grid.css",
                "~/Content/css/jquery.fileupload-ui.css",
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