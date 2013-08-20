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
            config.EnableQuerySupport();

            var modelBuilder = new ODataConventionModelBuilder();
            var user = modelBuilder.EntitySet<User>("User");
            user.EntityType.Ignore(p => p.Password);

            modelBuilder.EntitySet<Log>("Log");
            modelBuilder.EntitySet<Channel>("Channel");
            modelBuilder.EntitySet<Group>("Group");
            modelBuilder.EntitySet<Category>("Category");
            modelBuilder.EntitySet<Comment>("Comment");
            modelBuilder.EntitySet<Board>("Board");
            modelBuilder.EntitySet<Post>("Article");
            modelBuilder.EntitySet<Tag>("Tag");
            modelBuilder.EntitySet<Thumbnail>("Thumbnail");
            modelBuilder.EntitySet<Source>("Source");
            modelBuilder.EntitySet<QrCode>("QrCode");

            var signin = modelBuilder.Entity<User>().Action("Signin");
            signin.Parameter<string>("Username");
            signin.Parameter<string>("Password");
            signin.Parameter<bool>("IsRemember");

            var signout = modelBuilder.Entity<User>().Action("Signout");

            var model = modelBuilder.GetEdmModel();

            config.Routes.MapODataRoute(
                routeName: "OData",
                routePrefix: "odata",
                model: model);

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new {id = RouteParameter.Optional}
                );
        }
    }
}