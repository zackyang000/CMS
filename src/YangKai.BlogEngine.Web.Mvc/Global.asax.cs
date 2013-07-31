using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using AtomLab.Utility;
using YangKai.BlogEngine.Domain;
using YangKai.BlogEngine.Infrastructure;
using YangKai.BlogEngine.Service;
using YangKai.BlogEngine.Web.Mvc.BootStrapper;
using YangKai.BlogEngine.Web.Mvc.Controllers;

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
                        try
                        {
                            Proxy.Repository<Log>().Add(visitLog);
                        }
                        catch
                        {
                        }
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