using System.ComponentModel;
using AtomLab.Domain;
using Bootstrap.Extensions.StartupTasks;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;

namespace YangKai.BlogEngine.Web.Mvc.BootStrapper
{
    public class IocConfig : IStartupTask
    {
        public void Run()
        {
            IUnityContainer container = AtomLab.Utility.IoC.UnityContainerHelper.Create("unity.config");

            //TODO:应当将以下代码转移至配置文件unity.config中，但转移后拦截失败，原因不明。
            //参考http://www.cnblogs.com/kyo-yo/archive/2010/12/08/learning-entlib-tenth-decoupling-your-system-using-the-unity-part3-unity-and-piab.html

            container.AddNewExtension<Interception>();

            container.Configure<Interception>()
                .SetDefaultInterceptorFor<ICommandServices.ICommandService>(new TransparentProxyInterceptor())
                .AddPolicy("新增文章-更新RSS")
                .AddMatchingRule<MemberNameMatchingRule>
                (new InjectionConstructor("CreatePost"))
                .AddCallHandler<Modules.PostModule.Services.CallHandlers.PostRssCallHandler>
                ("PostRssCallHandler",
                 new ContainerControlledLifetimeManager());

            container.Configure<Interception>()
                .SetDefaultInterceptorFor<ICommandServices.ICommandService>(new TransparentProxyInterceptor())
                .AddPolicy("新增评论-更新RSS")
                .AddMatchingRule<MemberNameMatchingRule>
                (new InjectionConstructor("CreateComment"))
                .AddCallHandler<Modules.PostModule.Services.CallHandlers.CommentRssCallHandler>
                ("CommentRssCallHandler",
                 new ContainerControlledLifetimeManager());

            ControllerBuilder.Current.SetControllerFactory(new UnityControllerFactory(container));
            InstanceLocator.SetLocator(new MyInstanceLocator(container));
        }

        public void Reset()
        {
        }        
    }
}
