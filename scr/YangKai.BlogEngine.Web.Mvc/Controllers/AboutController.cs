using System;
using System.Web.Mvc;

namespace YangKai.BlogEngine.Web.Mvc.Controllers
{
    public class AboutController : Controller
    {
        //
        // πÿ”⁄±æ’æ
        // Get: /About
        public ActionResult Index()
        {
            return View();
        }

        //
        // RazirÃΩ’Î
        // Get: /About/Probe
        public ActionResult Probe()
        {
            return View();
        }
    }
}
