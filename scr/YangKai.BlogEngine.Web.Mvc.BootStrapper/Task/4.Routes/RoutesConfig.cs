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

            ConstRoute(routes);
            var mainClasses =QueryFactory.Post.GetGroupsByNotDeletion();
            /********************************************************
             * 以下每2段为1组,目的是为了控制访问&生成所需要的URL.   *
             * 第1段foreach是为了保证让访问如videos-demo时,无论第   *
             * 一个"-"之前为什么内容,都能跳转到Article的Controller  *
             * 第2段是为了使用ActionLink时能生成为如videos-demo一   *
             * 样的url.总之:第1段是为了访问,第2段是为了生成并且两   *
             * 句之间顺序不能变更(原因不明)                         *
             ********************************************************/
            foreach (var item in mainClasses)
            {
                LowerCaseUrlRouteMapHelper.MapLowerCaseUrlRoute((RouteCollection) routes, item.Url + "1",
                    item.Url + "-{id}",
                    new {controller = "Article", action = "Detail", id = UrlParameter.Optional, MainClassUrl = item.Url}
                    );
            }
            routes.MapLowerCaseUrlRoute(
                "Article-Detail",
                "{controller}-{id}",
                new {controller = "Article", action = "Detail", id = UrlParameter.Optional},
                new[] { "YangKai.BlogEngine.Web.Mvc.Controllers" }
                );

            /*******************************************
             * foreach中,MapLowerCaseUrlRoute的name即item.Url是给  *
             * MvcPager提供生成分页Link使用,若修改需   *
             * 与Html.Pager中的routeName一同修改       *
             *******************************************/
            foreach (var item in mainClasses)
            {
                routes.MapLowerCaseUrlRoute(
                    item.Url,
                    item.Url + "/{action}/{id}",
                    new {controller = "Article", action = "Index", id = UrlParameter.Optional, MainClassUrl = item.Url},
                    new[] { "YangKai.BlogEngine.Web.Mvc.Controllers" }
                    );
            }
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
                new[] { "YangKai.BlogEngine.Web.Mvc.Controllers" }
                );
        }

        private static void ConstRoute(RouteCollection routes)
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
                        }
                        , new[] { "YangKai.BlogEngine.Web.Mvc.Controllers" }
                    );
            }
            foreach (var entity in channels)
            {
                routes.MapLowerCaseUrlRoute(
                    entity.Url + "-calendar",
                    entity.Url + "-calendar",
                    new
                        {
                            controller = "Article",
                            action = "calendar",
                            channelUrl = entity.Url
                        }
                        , new[] { "YangKai.BlogEngine.Web.Mvc.Controllers" }
                    );
            }
            var defaultChannel = channels.Where(p => p.IsDefault).FirstOrDefault();
            if (defaultChannel != null)
            {
                routes.MapLowerCaseUrlRoute(
                    "index",
                    "",
                    new {controller = "Article", action = "Index", channelUrl = defaultChannel.Url}
                    , new[] { "YangKai.BlogEngine.Web.Mvc.Controllers" }
                    );
            }
        }
    }
}