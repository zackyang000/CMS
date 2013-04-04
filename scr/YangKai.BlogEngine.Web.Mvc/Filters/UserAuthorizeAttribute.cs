using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using YangKai.BlogEngine.Common;

namespace YangKai.BlogEngine.Web.Mvc.Filters
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public class UserAuthorizeAttribute : FilterAttribute, IAuthorizationFilter
    {
        public  void OnAuthorization(AuthorizationContext filterContext)
        {
            if (!WebMasterCookie.IsLogin)
            {
                var returnType = ((ReflectedActionDescriptor) filterContext.ActionDescriptor).MethodInfo.ReturnType;
              
                if (returnType == typeof(JsonResult))
                {
                    filterContext.Result = new JsonResult()
                    {
                        Data = new { result = false, reason = "Please Lgoin in." },
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    };
                }
                else
                {
                    filterContext.Result = new RedirectToRouteResult(
                        new RouteValueDictionary
                        {
                            {"Area", "Admin"},
                            {"Controller", "Account"},
                            {"Action", "Login"}
                        });
                }
            }
        }
    }
}