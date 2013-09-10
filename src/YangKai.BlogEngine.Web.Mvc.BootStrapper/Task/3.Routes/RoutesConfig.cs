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
                "Default",
                "{controller}/{action}/{id}",
                new {controller = "Home", action = "Index", id = UrlParameter.Optional},
                CONTROLLERS_NAMESPACE);
        }
    }
}