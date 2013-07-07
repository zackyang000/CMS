using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using YangKai.BlogEngine.Common;

namespace YangKai.BlogEngine.Web.Mvc.Filters
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public class UserAuthorizeAttribute : System.Web.Http.AuthorizeAttribute
    {
        public override void OnAuthorization(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            if (!WebMasterCookie.IsLogin)
            {
                HandleUnauthorizedRequest(actionContext);
            }
        }
        protected override void HandleUnauthorizedRequest(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            throw new HttpResponseException(HttpStatusCode.Unauthorized);
        }
    }

    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public class UserAuthorizeForMVCAttribute : FilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (!WebMasterCookie.IsLogin)
            {
                var returnType = ((ReflectedActionDescriptor)filterContext.ActionDescriptor).MethodInfo.ReturnType;

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