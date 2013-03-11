using System;
using System.Web.Mvc;

namespace YangKai.BlogEngine.Web.Mvc.Controllers
{
    public class AboutController : Controller
    {
        //
        // πÿ”⁄±æ’æ
        // Get: /about
        public ActionResult Index()
        {
            return View();
        }

        //
        // ÃΩ’Î
        // Get: /about/probe
        public ActionResult Probe()
        {
            return View();
        }
    }
}
