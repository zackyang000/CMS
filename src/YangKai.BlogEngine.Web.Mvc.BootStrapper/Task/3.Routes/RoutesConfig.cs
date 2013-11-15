using System.Web.Routing;
using Bootstrap.Extensions.StartupTasks;
using System.Web.Mvc;

namespace YangKai.BlogEngine.Web.Mvc.BootStrapper
{
    public class RoutesConfig : IStartupTask
    {
        private static readonly string[] CONTROLLERS_NAMESPACE = new[] {"YangKai.BlogEngine.Web.Mvc.Controllers"};

        public void Run()
        {
            RegisterRoutes(RouteTable.Routes);
        }

        public void Reset()
        {
        }

        private void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("favicon.ico");
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            RegisterDefaultRoute(routes);
        }

        private void RegisterDefaultRoute(RouteCollection routes)
        {
            routes.MapRoute(
                name: "Home",
                url: "Home/{action}/{id}",
                defaults: new {controller = "Home", action = "Index", id = UrlParameter.Optional}
                );

            routes.MapRoute(
                name: "Admin-Login",
                url: "Admin/Login",
                defaults: new { controller = "Admin", action = "Login"}
                );

            routes.MapRoute(
                name: "Admin-GetUser",
                url: "Admin/GetUser",
                defaults: new { controller = "Admin", action = "GetUser"}
                );

            routes.MapRoute(
                name: "FileUpload",
                url: "FileUpload/{action}/{id}",
                defaults: new { controller = "FileUpload", action = "Index", id = UrlParameter.Optional }
                );

            routes.MapRoute(
                name: "Admin",
                url: "Admin/{*all}",
                defaults: new {controller = "Admin", action = "Index"}
                );

            routes.MapRoute(
                name: "Default",
                url: "{*all}",
                defaults: new {controller = "Home", action = "Index"}
                );
        }
    }
}