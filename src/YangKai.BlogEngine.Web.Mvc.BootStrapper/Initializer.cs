using System.Web;
using AtomLab.Core;
using AtomLab.Utility;
using Bootstrap.Extensions.StartupTasks;
using YangKai.BlogEngine.Web.Mvc.BootStrapper;

[assembly: PreApplicationStartMethod(typeof(Initializer), "Initialize")]
namespace YangKai.BlogEngine.Web.Mvc.BootStrapper
{
    public static class Initializer
    {
        public static void Initialize()
        {
            Auth.GetName = () => EncryptionCookieHelper.Load("__Username__");

            Bootstrap.Bootstrapper.With.StartupTasks()
                .UsingThisExecutionOrder(s => s
                    .Then<IoCConfig>()
                    .DelayStartBy(1).MilliSeconds
                    .First<DataInitializeConfig>()
                    .DelayStartBy(1).MilliSeconds
                    .Then<WebApiConfig>()
                    .Then<RoutesConfig>()
                    .Then<FiltersConfig>()
                    .Then<BundlesConfig>()
                ).Start();
        }
    }
}