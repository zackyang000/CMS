using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using YangKai.BlogEngine.Modules.PostModule.Objects;
using YangKai.BlogEngine.ServiceProxy;

namespace YangKai.BlogEngine.Web.Mvc.Controllers
{
    public class HomeController : Controller
    {
        [ActionName("index")]
        public ActionResult Index()
        {
            var channels = Query.Channel.GetAll(p => !p.IsDeleted).ToList();
            return View(channels);
        }
    }
}