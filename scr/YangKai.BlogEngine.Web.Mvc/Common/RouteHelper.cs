using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace YangKai.BlogEngine.Web.Mvc.Common
{
    /// <summary>
    /// 生成ArticleController中Index和Detail的超链接.
    /// 因为这2个Action的路由中,传值必须要使用groupUrl和channelUrl其中之一.为了便于使用,编写该Helper.
    /// 用于代替Url.Action(actionName, routeValues)
    /// 其效果同于:
    ///     Url.Action("Index", new RouteValueDictionary
    ///         {
    ///             {"Controller", "Article"},
    ///             {"Action", "Index"},
    ///             {"groupUrl", groupUrl},
    ///         });
    /// </summary>
    public class RouteHelper
    {
        private UrlHelper Url;

        public RouteHelper(RequestContext requestContext)
        {
            Url = new UrlHelper(requestContext);
        }

        public static RouteHelper Current
        {
            get { return new RouteHelper(HttpContext.Current.Request.RequestContext); }
        }

        public string Index(string groupUrl = "", string channelUrl = "")
        {
            return ArticleUrl("Index", groupUrl, channelUrl);
        }

        public string Detail(string groupUrl = "", string channelUrl = "")
        {
            return ArticleUrl("Detail", groupUrl, channelUrl);
        }

        private string ArticleUrl(string actionName, string groupUrl = "", string channelUrl = "")
        {
            var route = new RouteValueDictionary {{"Controller", "Article"}};

            if (groupUrl != "")
            {
                route.Add("groupUrl", groupUrl);
            }
            else if (channelUrl != "")
            {
                route.Add("channelUrl", channelUrl);
            }
            else
            {
                throw new ArgumentException("GroupUrl and ChannelUrl must be have one.");
            }

            return Url.Action(actionName, route);
        }
    }
}