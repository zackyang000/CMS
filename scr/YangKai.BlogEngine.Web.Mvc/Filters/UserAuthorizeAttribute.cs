using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using YangKai.BlogEngine.Common;

namespace YangKai.BlogEngine.Web.Mvc.Filters
{
    public class UserAuthorizeAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (!WebMasterCookie.IsLogin)
            {
                //TODO:根据不同的请求应返回不同的代码  json/action  result
//                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary
//                    {
//                        {"controller", "Admin"},
//                        {"action", "login"}
//                    });
//                return;
            }
        }
    }
}