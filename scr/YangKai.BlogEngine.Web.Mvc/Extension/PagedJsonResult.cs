using System;
using System.Diagnostics;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using AtomLab.Domain.Infrastructure;

namespace YangKai.BlogEngine.Web.Mvc
{
    public class PagedJsonResult<T> : JsonResult
    {
        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            HttpResponseBase response = context.HttpContext.Response;

            response.ContentType = !String.IsNullOrEmpty(ContentType) ? ContentType : "application/json";

            if (ContentEncoding != null)
            {
                response.ContentEncoding = ContentEncoding;
            }

            if (Data == null) return;

            var data = Data as PageList<T>;
            if (data == null)
            {
                Debug.Assert(false, "该方法只适用于PageList<T>,若仅返回Json,请直接使用Json(object data)方法.");
            }
            Data = new
                {
                    data = data.DataList,
                    count = data.TotalCount,
                };
            var serializer = new JavaScriptSerializer();
            response.Write(serializer.Serialize(Data));
        }
    }
}