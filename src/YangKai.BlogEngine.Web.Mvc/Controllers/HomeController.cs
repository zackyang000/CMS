using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using YangKai.BlogEngine.Proxy;

namespace YangKai.BlogEngine.Web.Mvc.Controllers
{
    public class HomeController : Controller
    {
        [ActionName("index")]
        public ActionResult Index()
        {
            var channels = Repository.Channel.GetAll(p => !p.IsDeleted).ToList();
            return View(channels);
        }
    }
}