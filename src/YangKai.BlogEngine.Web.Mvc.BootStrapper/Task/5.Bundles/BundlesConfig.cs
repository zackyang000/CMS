using System.Web.Optimization;
using Bootstrap.Extensions.StartupTasks;

namespace YangKai.BlogEngine.Web.Mvc.BootStrapper
{
    public class BundlesConfig : IStartupTask
    {
        public void Run()
        {
            BundleJs(
                "~/Content/plugin/syntaxhighlighter_3.0.83/scripts/shCore.js",
                "~/Content/plugin/syntaxhighlighter_3.0.83/scripts/shBrushCSharp.js",
                "~/Content/plugin/syntaxhighlighter_3.0.83/scripts/shBrushCss.js",
                "~/Content/plugin/syntaxhighlighter_3.0.83/scripts/shBrushJScript.js",
                "~/Content/plugin/syntaxhighlighter_3.0.83/scripts/shBrushXml.js"
                );

            BundleCss(
                "~/Content/plugin/syntaxhighlighter_3.0.83/styles/shCoreDefault.css"
                );

            //BundleTable.EnableOptimizations = true;
        }

        public void Reset()
        {
        }

        private void BundleJs(params string[] plugin)
        {
            var angularFiles = new BundleFileSetOrdering("Angular");
            angularFiles.Files.Add("angular.js");
            angularFiles.Files.Add("angular-{version}.js");
            angularFiles.Files.Add("angular-*.js");
            angularFiles.Files.Add("angular-*-{version}.js");
            BundleTable.Bundles.FileSetOrderList.Add(angularFiles);

            var bundle = new ScriptBundle("~/js")
                .IncludeDirectory("~/Content/js/vendor", "*.js", true)
                .Include("~/Content/js/main.js",
                    "~/Content/js/directives/*.js",
                    "~/Content/js/directives/custom/*.js",
                    "~/Content/js/services/*.js")
                .IncludeDirectory("~/Content/js/filters", "*.js", true)
                .IncludeDirectory("~/Content/js/controllers", "*.js", true)
                .Include("~/Content/js/app.js")
                .Include(plugin);

            BundleTable.Bundles.Add(bundle);
        }

        private void BundleCss(params string[] plugin)
        {
            var bundle = new StyleBundle("~/Content/style/css")
                .IncludeDirectory("~/Content/css/vendor/", "*.css",true)
                .Include(plugin)
                .Include("~/Content/css/style.css");

            BundleTable.Bundles.Add(bundle);
        }
    }
}