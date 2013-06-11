using System.ComponentModel;
using System.Configuration;
using System.Reflection;
using AtomLab.Domain;
using AtomLab.Utility.IoC;
using Bootstrap.Extensions.StartupTasks;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using YangKai.BlogEngine.ICommandServices;

namespace YangKai.BlogEngine.Web.Mvc.BootStrapper
{
    public class IocConfig : IStartupTask
    {
        public void Run()
        {
            var path = ConfigurationManager.AppSettings["ContainerConfigPath"];
            IUnityContainer container = UnityContainerHelper.Create(path);
            InstanceLocator.SetLocator(new MyInstanceLocator(container));
        }

        public void Reset()
        {
        }        
    }
}
