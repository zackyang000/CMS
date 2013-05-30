using System.Configuration;
using System.Linq;
using System.Web.Routing;
using Bootstrap.Extensions.StartupTasks;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using YangKai.BlogEngine.ServiceProxy;

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
            
            RegisterGroupIndexViewRoute(routes);
            RegisterGroupDetailViewRoute(routes);
            RegisterChannelIndexViewRoute(routes);
            RegisterChannelCalendarViewRoute(routes);
            RegisterDefaultRoute(routes);
        }

        //{group.Url}/{id}
        private static void RegisterGroupIndexViewRoute(RouteCollection routes)
        {
            var groups = QueryFactory.Instance.Post.GetGroupsByNotDeletion();
            foreach (var item in groups)
            {
                routes.MapRoute(
                    item.Url,
                    item.Url + "/{id}",
                    new
                        {
                            Controller = "Article",
                            Action = "Index",
                            groupUrl = item.Url,
                            id = UrlParameter.Optional,
                        },
                    CONTROLLERS_NAMESPACE);
            }
        }

        //{group.Url}-{id}
        private static void RegisterGroupDetailViewRoute(RouteCollection routes)
        {
            var groups = QueryFactory.Instance.Post.GetGroupsByNotDeletion();

            foreach (var item in groups)
            {
                routes.MapRoute(item.Url + "1", item.Url + "-{id}",
                                            new
                                                {
                                                    controller = "Article",
                                                    action = "Detail",
                                                    id = UrlParameter.Optional,
                                                    groupUrl = item.Url,
                                                    channelUrl = item.Channel.Url
                                                });
            }
        }

        //{channel.Url}/{id}
        private static void RegisterChannelIndexViewRoute(RouteCollection routes)
        {
            var channels = QueryFactory.Instance.Post.FindAllByNotDeletion();

            foreach (var entity in channels)
            {
                routes.MapRoute(
                    entity.Url,
                    entity.Url + "/{id}",
                    new
                        {
                            Controller = "Article",
                            Action = "Index",
                            channelUrl = entity.Url,
                            id = UrlParameter.Optional
                        },
                    CONTROLLERS_NAMESPACE);
            }
        }

        //{channel.Url}-calendar
        private static void RegisterChannelCalendarViewRoute(RouteCollection routes)
        {
            var channels = QueryFactory.Instance.Post.FindAllByNotDeletion();

            foreach (var entity in channels)
            {
                routes.MapRoute(
                    entity.Url + "-calendar",
                    entity.Url + "-calendar",
                    new
                        {
                            Controller = "Article",
                            Action = "Calendar",
                            channelUrl = entity.Url,
                            groupUrl = string.Empty,
                        },
                    CONTROLLERS_NAMESPACE);
            }
        }

        //{controller}/{action}/{id}
        private static void RegisterDefaultRoute(RouteCollection routes)
        {
            routes.MapRoute(
                "Default",
                "{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                CONTROLLERS_NAMESPACE);
        }
    }
}