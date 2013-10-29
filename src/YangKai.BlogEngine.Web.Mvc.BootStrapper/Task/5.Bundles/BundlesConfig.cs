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
                "~/Content/plugin/syntaxhighlighter_3.0.83/scripts/shBrushJava.js",
                "~/Content/plugin/syntaxhighlighter_3.0.83/scripts/shBrushSql.js",
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

            var messengerFiles = new BundleFileSetOrdering("Messenger");
            messengerFiles.Files.Add("messenger.js");
            messengerFiles.Files.Add("messenger-*.js");
            BundleTable.Bundles.FileSetOrderList.Add(messengerFiles);

            //js
            var bundle = new ScriptBundle("~/js")
                .Include("~/Content/js/init.js")
                .IncludeDirectory("~/Content/js/vendor", "*.js", true)
                .Include("~/Content/js/common.js")
                .IncludeDirectory("~/Content/js/directives", "*.js", true)
                .IncludeDirectory("~/Content/js/services", "*.js", true)
                .IncludeDirectory("~/Content/js/filters", "*.js", true)
                .IncludeDirectory("~/Content/i18n", "*.js", true)
                .IncludeDirectory("~/Content/app", "*.js", true)
                .Include(plugin);
            BundleTable.Bundles.Add(bundle);

            //admin-js
            var aceBundle = new ScriptBundle("~/admin-js")
                .Include("~/Content/js/init.js")
                .IncludeDirectory("~/Content/js/vendor", "*.js", true)
                .Include("~/Content/js/common.js")
                .IncludeDirectory("~/Content/js/directives", "*.js", true)
                .IncludeDirectory("~/Content/js/services", "*.js", true)
                .IncludeDirectory("~/Content/js/filters", "*.js", true)
                .IncludeDirectory("~/Content/i18n", "*.js", true)
                .IncludeDirectory("~/Content/app", "*.js", true)
                .IncludeDirectory("~/Content/plugin/ace_1.2", "*.js", true)
                .Include(plugin);
            BundleTable.Bundles.Add(aceBundle);
        }

        private void BundleCss(params string[] plugin)
        {
            var messengerFiles = new BundleFileSetOrdering("messenger");
            messengerFiles.Files.Add("messenger.css");
            messengerFiles.Files.Add("messenger-*.css");
            BundleTable.Bundles.FileSetOrderList.Add(messengerFiles);

            //css
            var bundle = new StyleBundle("~/Content/style/css")
                .IncludeDirectory("~/Content/css/vendor", "*.css", true)
                .Include(plugin)
                .IncludeDirectory("~/Content/app", "*.css", true)
                .Include("~/Content/css/style.css");
            BundleTable.Bundles.Add(bundle);

            //admin-css
            var aceBundle = new StyleBundle("~/Content/style/admin-css")
                .IncludeDirectory("~/Content/css/vendor", "*.css", true)
                .Include(plugin)
                .IncludeDirectory("~/Content/plugin/ace_1.2", "*.css", true)
                .IncludeDirectory("~/Content/app", "*.css", true);
            BundleTable.Bundles.Add(aceBundle);
        }
    }
}