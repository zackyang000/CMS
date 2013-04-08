using System.Configuration;
using System.Linq;
using System.Web.Routing;
using AtomLab.Utility.RouteHelper;
using Bootstrap.Extensions.StartupTasks;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using YangKai.BlogEngine.ServiceProxy;

namespace YangKai.BlogEngine.Web.Mvc.BootStrapper
{
    public class RoutesConfig : IStartupTask
    {
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

            RegisterGroupRoute(routes);
            RegisterChannelRoute(routes);
            RegisterArticleRoute(routes);
        }

        private static void RegisterGroupRoute(RouteCollection routes)
        {
            var groups = QueryFactory.Post.GetGroupsByNotDeletion();
            foreach (var item in groups)
            {
                routes.MapLowerCaseUrlRoute(
                    item.Url,
                    item.Url + "/{action}/{id}",
                    new
                        {
                            Controller = "Article",
                            Action = "Index",
                            GroupUrl = item.Url,
                            id = UrlParameter.Optional,
                        },
                    new[] {"YangKai.BlogEngine.Web.Mvc.Controllers"}
                    );
            }
        }

        private static void RegisterChannelRoute(RouteCollection routes)
        {
            var channels = QueryFactory.Post.FindAllByNotDeletion();

            foreach (var entity in channels)
            {
                routes.MapLowerCaseUrlRoute(
                    entity.Url,
                    entity.Url + "/{id}",
                    new
                        {
                            controller = "Article",
                            action = "Index",
                            channelUrl = entity.Url,
                            id = UrlParameter.Optional
                        },
                    new[] {"YangKai.BlogEngine.Web.Mvc.Controllers"});

                routes.MapLowerCaseUrlRoute(
                    entity.Url + "-calendar",
                    entity.Url + "-calendar",
                    new
                        {
                            controller = "Article",
                            action = "calendar",
                            channelUrl = entity.Url
                        }
                    , new[] {"YangKai.BlogEngine.Web.Mvc.Controllers"}
                    );
            }

            var defaultChannel = channels.FirstOrDefault(p => p.IsDefault);
            if (defaultChannel == null) throw new ConfigurationErrorsException("Channels must have a 'DefaultChannel'");
            routes.MapLowerCaseUrlRoute(
                "index",
                string.Empty,
                new {controller = "Article", action = "Index", channelUrl = defaultChannel.Url}
                , new[] {"YangKai.BlogEngine.Web.Mvc.Controllers"}
                );
        }

        private static void RegisterArticleRoute(RouteCollection routes)
        {
            var groups = QueryFactory.Post.GetGroupsByNotDeletion();
            /********************************************************
             * 以下每2段为1组,目的是为了控制访问&生成所需要的URL.
             * 第1段foreach是为了保证让访问如videos-demo时,无论第一
             * 个"-"之前为什么内容,都能跳转到Article的Controller第
             * 2段是为了使用ActionLink时能生成为如videos-demo一样的
             * url.总之:第1段是为了访问,第2段是为了生成.并且两句之间顺
             * 序不能变更.
             ********************************************************/
            foreach (var item in groups)
            {
                routes.MapLowerCaseUrlRoute(item.Url + "1", item.Url + "-{id}",
                                            new
                                                {
                                                    controller = "Article",
                                                    action = "Detail",
                                                    id = UrlParameter.Optional,
                                                    groupUrl = item.Url,
                                                    channelUrl = item.Channel.Url
                                                });
            }
            routes.MapLowerCaseUrlRoute(
                "Article-Detail",
                "{controller}-{id}",
                new {controller = "Article", action = "Detail", id = UrlParameter.Optional},
                new[] {"YangKai.BlogEngine.Web.Mvc.Controllers"}
                );


            //由于使用Default URL所以不需要本行
            //routes.MapLowerCaseUrlRoute(
            //        "2",
            //        "{controller}/{action}/{id}",
            //        new { controller = "Article", action = "Detail", id = UrlParameter.Optional }
            //);

            routes.MapLowerCaseUrlRoute(
                "Default",
                "{controller}/{action}/{id}",
                new {controller = "Article", action = "Index", id = UrlParameter.Optional},
                new[] {"YangKai.BlogEngine.Web.Mvc.Controllers"}
                );
        }
    }
}