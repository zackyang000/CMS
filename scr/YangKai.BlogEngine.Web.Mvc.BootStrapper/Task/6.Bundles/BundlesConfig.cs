using System.Web.Optimization;
using Bootstrap.Extensions.StartupTasks;
using System.Web.Mvc;

namespace YangKai.BlogEngine.Web.Mvc.BootStrapper
{
    public class BundlesConfig : IStartupTask
    {
        public void Run()
        {
//            var bundles = BundleTable.Bundles;
//
//            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
//                    "~/Scripts/jquery-{version}.js"));
//
//            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
//                        "~/Scripts/jquery-ui-{version}.js"));
//
//            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
//                        "~/Scripts/jquery.unobtrusive*",
//                        "~/Scripts/jquery.validate*"));
//
//            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
//                        "~/Scripts/modernizr-*"));
//
//            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/site.css"));
//
//            bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
//                        "~/Content/themes/base/jquery.ui.core.css",
//                        "~/Content/themes/base/jquery.ui.resizable.css",
//                        "~/Content/themes/base/jquery.ui.selectable.css",
//                        "~/Content/themes/base/jquery.ui.accordion.css",
//                        "~/Content/themes/base/jquery.ui.autocomplete.css",
//                        "~/Content/themes/base/jquery.ui.button.css",
//                        "~/Content/themes/base/jquery.ui.dialog.css",
//                        "~/Content/themes/base/jquery.ui.slider.css",
//                        "~/Content/themes/base/jquery.ui.tabs.css",
//                        "~/Content/themes/base/jquery.ui.datepicker.css",
//                        "~/Content/themes/base/jquery.ui.progressbar.css",
//                        "~/Content/themes/base/jquery.ui.theme.css"));
        }

        public void Reset()
        {
        }
    }
}