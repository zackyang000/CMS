using System.Web.Optimization;
using Bootstrap.Extensions.StartupTasks;
using System.Web.Mvc;

namespace YangKai.BlogEngine.Web.Mvc.BootStrapper
{
    public class BundlesConfig : IStartupTask
    {
        public void Run()
        {
            BundleTable.Bundles.Add(new ScriptBundle("~/js").Include(
                "~/Content/js/jquery-{version}.js",
                "~/Content/js/jquery.lazyload.js",
                "~/Content/js/knockout-{version}.js",
                "~/Content/js/bootstrap.js",
                "~/Content/js/koExternalTemplateEngine_all.js",
                "~/Content/js/ko.pager.js",
                "~/Content/js/sammy.js",
                "~/Content/js/messenger.js",
                "~/Content/js/messenger-theme-future.js",
                "~/Content/plugin/syntaxhighlighter_3.0.83/scripts/shCore.js",
                "~/Content/plugin/syntaxhighlighter_3.0.83/scripts/shBrushCSharp.js",
                "~/Content/plugin/syntaxhighlighter_3.0.83/scripts/shBrushCss.js",
                "~/Content/plugin/syntaxhighlighter_3.0.83/scripts/shBrushJScript.js",
                "~/Content/plugin/syntaxhighlighter_3.0.83/scripts/shBrushXml.js",
                "~/Content/js/main.js",
                "~/Content/ko/viewmodel/*.js"
                                        ));

            BundleTable.Bundles.Add(new StyleBundle("~/css").Include(
                "~/Content/plugin/syntaxhighlighter_3.0.83/styles/shCoreDefault.css",
                "~/Content/css/normalize.css",
                "~/Content/css/html5reset.css",
                "~/Content/css/bootstrap.css",
                "~/Content/css/messenger.css",
                "~/Content/css/messenger-theme-future.css",
                "~/Content/css/style.css"
                                        ));

            //BundleTable.EnableOptimizations = true;
        }

        public void Reset()
        {
        }
    }
}