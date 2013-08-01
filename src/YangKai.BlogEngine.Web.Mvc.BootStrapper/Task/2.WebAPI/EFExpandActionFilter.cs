using System;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Web;
using System.Web.Http.Filters;

namespace YangKai.BlogEngine.Web.Mvc.BootStrapper
{
    /// <summary>
    /// Adds $expand support for entity framework entities exposed over ASP.Net Web API
    /// </summary>
    /// <remarks>
    /// Remember to add it to the Filters for your configuration
    /// </remarks>
    public class EFExpandActionFilter : ActionFilterAttribute
    {
        /// <summary>
        /// Called when the action is executed.
        /// </summary>
        /// <param name="actionExecutedContext">The action executed context.</param>
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            var querystringParams = HttpUtility.ParseQueryString(actionExecutedContext.Request.RequestUri.Query);
            var expands = querystringParams["$expand"];

            if (string.IsNullOrWhiteSpace(expands)) return;

            dynamic responseObject;
            actionExecutedContext.Response.TryGetContentValue(out responseObject);

            expands.Split(',').Select(s => s.Trim()).ToList().ForEach(
                expand => responseObject = responseObject.Include(expand));
            ((ObjectContent) actionExecutedContext.Response.Content).Value = responseObject;
        }
    }
}