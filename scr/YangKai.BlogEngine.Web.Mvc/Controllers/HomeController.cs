using System.Collections.Generic;
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
            return View();
        }

        [ActionName("layout-header")]
        public PartialViewResult Nav()
        {
            var channels = QueryFactory.Instance.Post.FindAllByNotDeletion();
            return PartialView(channels);
        }
    }
}