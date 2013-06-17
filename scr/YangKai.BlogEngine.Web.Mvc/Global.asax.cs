using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using AtomLab.Utility;
using YangKai.BlogEngine.Infrastructure;
using YangKai.BlogEngine.Modules.CommonModule.Objects;
using YangKai.BlogEngine.Modules.PostModule.Objects;
using YangKai.BlogEngine.ServiceProxy;
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
                            Command.Instance.Create(visitLog);
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
            Exception exception = Server.GetLastError();

            Response.Clear();

            var routeData = new RouteData();
            routeData.Values.Add("controller", "Home");

            var httpException = exception as HttpException;

            if (httpException == null)
            {
                routeData.Values.Add("action", "Index");
            }
            else
            {
                switch (httpException.GetHttpCode())
                {
                    case 404:
                        routeData.Values.Add("action", "Index");
                        break;
                    case 500:
                        routeData.Values.Add("action", "Index");
                        break;
                    default:
                        routeData.Values.Add("action", "Index");
                        break;
                }
            }

            //用于页面显示错误信息.
            //routeData.Values.Add("error", exception);

            Server.ClearError();

            Response.TrySkipIisCustomErrors = true;

            IController errorController = new HomeController();
            errorController.Execute(new RequestContext(
                 new HttpContextWrapper(Context), routeData));
        }

        protected void Session_End()
        {

        }

        protected void Application_End()
        {

        }
    }
}