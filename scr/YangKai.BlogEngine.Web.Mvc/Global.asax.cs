using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AtomLab.Utility;
using YangKai.BlogEngine.Infrastructure;
using YangKai.BlogEngine.Modules.CommonModule.Objects;
using YangKai.BlogEngine.Modules.PostModule.Objects;
using YangKai.BlogEngine.ServiceProxy;
using YangKai.BlogEngine.Web.Mvc.BootStrapper;

namespace YangKai.BlogEngine.Web.Mvc
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            Initializer.Initialize();
        }

        protected void Session_Start()
        {
            var context = HttpContext.Current;
            Task.Factory.StartNew(() =>
                {
                    var visitLog = Log.CreateSiteVisitLog();
                    visitLog.Ip = context.Request.UserHostAddress;
                    visitLog.Browser = context.Request.Browser.Browser;
                    visitLog.BrowserVersion = context.Request.Browser.Version;
                    visitLog.Os = Function.GetOSName(context);
                    if (!visitLog.IsRobot)
                    {
                        CommandFactory.Create(visitLog);
                    }
                });
        }

        protected void Application_BeginRequest()
        {

        }

        protected void Application_AuthenticateRequest()
        {

        }

        protected void Application_EndRequest(object sender, EventArgs e)
        {
//            HttpContext context = HttpContext.Current;
//
//            var temStatusCode = context.ApplicationInstance.Response.StatusCode;
//            if (temStatusCode==500)
//            {
//                context.Response.Clear();
//                context.Response.ClearHeaders();
//
//                context.Response.Expires = 0;
//
//                context.Response.StatusCode = 500;
//            }
//            if (temStatusCode == 401)
//            {
//                context.Response.Clear();
//                context.Response.ClearHeaders();
//
//                context.Response.Expires = 0;
//
//                context.Response.StatusCode = 401;
//            }
        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End()
        {

        }

        protected void Application_End()
        {

        }
    }
}