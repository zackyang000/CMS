using System.ComponentModel;
using System.Reflection;
using AtomLab.Domain;
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
            IUnityContainer container = AtomLab.Utility.IoC.UnityContainerHelper.Create("unity.config");
            InstanceLocator.SetLocator(new MyInstanceLocator(container));
        }

        public void Reset()
        {
        }        
    }
}
