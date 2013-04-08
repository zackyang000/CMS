using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace YangKai.BlogEngine.Web.Mvc.Common
{
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
                route.Add("GroupUrl", groupUrl);
            }
            else if (channelUrl != "")
            {
                route.Add("ChannelUrl", channelUrl);
            }
            else
            {
                throw new ArgumentException("GroupUrl and ChannelUrl must be have one.");
            }

            return Url.Action(actionName, route);
        }
    }
}