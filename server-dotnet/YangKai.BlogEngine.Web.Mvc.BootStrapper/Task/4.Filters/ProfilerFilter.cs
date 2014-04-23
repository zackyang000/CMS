using System;
using System.Diagnostics;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace YangKai.BlogEngine.Web.Mvc.BootStrapper
{
    public class ProfilerFilter : ActionFilterAttribute
    {
        private const string Key = "__profiler__";

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var stopWatch = new Stopwatch();
            actionContext.Request.Properties[Key] = stopWatch;
            stopWatch.Start();
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            if (!actionExecutedContext.Request.Properties.ContainsKey(Key))
            {
                return;
            }

            var stopWatch = actionExecutedContext.Request.Properties[Key] as Stopwatch;
            if (stopWatch != null)
            {
                stopWatch.Stop();
                var actionName = actionExecutedContext.ActionContext.ActionDescriptor.ActionName;
                var controllerName = actionExecutedContext.ActionContext.ActionDescriptor.ControllerDescriptor.ControllerName;
                Debug.Print("[{0}] Profiler: Execute {1}-{2} cost {3}.", DateTime.Now.ToString("G"), controllerName, actionName, stopWatch.Elapsed);
            }

        }
    }
}