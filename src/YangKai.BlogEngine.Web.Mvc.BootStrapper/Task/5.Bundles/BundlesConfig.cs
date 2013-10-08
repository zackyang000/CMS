using System.Web.Optimization;
using Bootstrap.Extensions.StartupTasks;

namespace YangKai.BlogEngine.Web.Mvc.BootStrapper
{
    public class BundlesConfig : IStartupTask
    {
        public void Run()
        {
            BundleJs();
            BundleCss();

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

            var messengerFiles = new BundleFileSetOrdering("messenger");
            messengerFiles.Files.Add("messenger.js");
            messengerFiles.Files.Add("messenger-*.js");
            BundleTable.Bundles.FileSetOrderList.Add(messengerFiles);


            var bundle = new ScriptBundle("~/js")
                .IncludeDirectory("~/Content/js/vendor", "*.js", true)
                .Include("~/Content/js/main.js")
                .IncludeDirectory("~/Content/js/directives", "*.js", true)
                .IncludeDirectory("~/Content/js/services", "*.js", true)
                .IncludeDirectory("~/Content/js/filters", "*.js", true)
                .IncludeDirectory("~/Content/js/controllers", "*.js", true)
                .Include("~/Content/js/app.js")
                .Include(plugin);

            BundleTable.Bundles.Add(bundle);
        }

        private void BundleCss(params string[] plugin)
        {
            var messengerFiles = new BundleFileSetOrdering("messenger");
            messengerFiles.Files.Add("messenger.css");
            messengerFiles.Files.Add("messenger-*.css");
            BundleTable.Bundles.FileSetOrderList.Add(messengerFiles);

            var bundle = new StyleBundle("~/Content/style/css")
                .IncludeDirectory("~/Content/css/vendor", "*.css", true)
                .Include(plugin)
                .Include("~/Content/css/style.css");

            BundleTable.Bundles.Add(bundle);
        }
    }
}