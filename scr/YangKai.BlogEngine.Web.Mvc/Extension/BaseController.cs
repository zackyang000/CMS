using System.Text;
using System.Web.Mvc;
using AtomLab.Domain.Infrastructure;

namespace YangKai.BlogEngine.Web.Mvc
{
    public class BaseController : Controller
    {
        protected JsonResult PagedJson<T>(PageList<T> data, string contentType = null, Encoding contentEncoding = null)
        {
            return new PagedJsonResult<T>()
                {
                    Data = data,
                    ContentType = contentType,
                    ContentEncoding = contentEncoding
                };
        }
    }
}