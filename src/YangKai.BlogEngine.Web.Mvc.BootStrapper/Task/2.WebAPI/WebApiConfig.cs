using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.OData.Builder;
using Bootstrap.Extensions.StartupTasks;
using Microsoft.Data.Edm;
using Newtonsoft.Json;
using YangKai.BlogEngine.Domain;

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
            var modelBuilder = new ODataConventionModelBuilder();
            modelBuilder.EntitySet<User>("User");
            modelBuilder.EntitySet<Log>("Log");
            modelBuilder.EntitySet<Channel>("Channel");
            modelBuilder.EntitySet<Group>("Group");
            modelBuilder.EntitySet<Category>("Category");
            modelBuilder.EntitySet<Post>("Post");
            modelBuilder.EntitySet<Comment>("Comment");
            modelBuilder.EntitySet<Board>("Board");
            modelBuilder.EntitySet<Post>("Post");
            modelBuilder.EntitySet<Tag>("Tag");
            modelBuilder.EntitySet<Thumbnail>("Thumbnail");
            modelBuilder.EntitySet<Source>("Source");
            modelBuilder.EntitySet<QrCode>("QrCode");

            var model = modelBuilder.GetEdmModel();

            config.Routes.MapODataRoute(routeName: "OData", routePrefix: "odata", model: model);

            config.EnableQuerySupport();

            //config.Formatters.JsonFormatter.AddQueryStringMapping("$format", "json", "application/json");
            //config.Formatters.XmlFormatter.AddQueryStringMapping("$format", "xml", "application/xml");

            //config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

            //使EF支持$expand
            //config.Filters.Add(new EFExpandActionFilter());

            //config.EnableSystemDiagnosticsTracing();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new {id = RouteParameter.Optional}
                );
        }
    }
}