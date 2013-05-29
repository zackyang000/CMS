using System.Web.Http;
using Bootstrap.Extensions.StartupTasks;

namespace YangKai.BlogEngine.Web.Mvc.BootStrapper
{
    public class WebApiConfig : IStartupTask
    {
        public void Run()
        {
            RegisterRoutes(GlobalConfiguration.Configuration);

            //TODO 暂时屏蔽WEB API的返回XML数据的方式.
            GlobalConfiguration.Configuration.Formatters.XmlFormatter.SupportedMediaTypes.Clear();
        }

        public void Reset()
        {
        }

        public static void RegisterRoutes(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                  name: "DefaultApi",
                  routeTemplate: "api/{controller}/{id}",
                  defaults: new { id = RouteParameter.Optional }
              );

            config.EnableSystemDiagnosticsTracing();
        }
    }
}