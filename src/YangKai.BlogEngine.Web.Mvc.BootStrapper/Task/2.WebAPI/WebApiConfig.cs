using System.Net.Http.Formatting;
using System.Web.Http;
using Bootstrap.Extensions.StartupTasks;
using Newtonsoft.Json;

namespace YangKai.BlogEngine.Web.Mvc.BootStrapper
{
    public class WebApiConfig : IStartupTask
    {
        public void Run()
        {
            RegisterRoutes(GlobalConfiguration.Configuration);
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

            config.Formatters.JsonFormatter.AddQueryStringMapping("$format", "json", "application/json");
            config.Formatters.XmlFormatter.AddQueryStringMapping("$format", "xml", "application/xml");

            config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

            //使EF支持$expand
            config.Filters.Add(new EFExpandActionFilter());

            config.EnableSystemDiagnosticsTracing();
        }
    }
}