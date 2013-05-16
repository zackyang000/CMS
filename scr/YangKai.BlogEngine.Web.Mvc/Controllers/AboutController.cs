using System;
using System.Web.Mvc;
using YangKai.BlogEngine.Web.Mvc.Filters;

namespace YangKai.BlogEngine.Web.Mvc.Controllers
{
    public class AboutController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
