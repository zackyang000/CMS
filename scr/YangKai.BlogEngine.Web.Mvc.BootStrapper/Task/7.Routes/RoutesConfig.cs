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

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("favicon.ico");
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            RegisterDefaultRoute(routes);
        }

        private static void RegisterDefaultRoute(RouteCollection routes)
        {
            routes.MapRoute(
                "Default",
                "{controller}/{action}/{id}",
                new {controller = "Home", action = "Index", id = UrlParameter.Optional},
                CONTROLLERS_NAMESPACE);
            routes.MapRoute(
                "Default1",
                "{a}",
                new {controller = "Home", action = "Index"},
                CONTROLLERS_NAMESPACE);
            routes.MapRoute(
                "Default2",
                "{a}/{b}",
                new {controller = "Home", action = "Index"},
                CONTROLLERS_NAMESPACE);
            routes.MapRoute(
                "Default3",
                "{a}/{b}/{c}",
                new {controller = "Home", action = "Index"},
                CONTROLLERS_NAMESPACE);
            routes.MapRoute(
                "Default4",
                "{a}/{b}/{c}/{d}",
                new {controller = "Home", action = "Index"},
                CONTROLLERS_NAMESPACE);
            routes.MapRoute(
                "Default5",
                "{a}/{b}/{c}/{d}/{e}",
                new {controller = "Home", action = "Index"},
                CONTROLLERS_NAMESPACE);
            routes.MapRoute(
                "Default6",
                "{a}/{b}/{c}/{d}/{e}/{f}",
                new {controller = "Home", action = "Index"},
                CONTROLLERS_NAMESPACE);
        }
    }
}