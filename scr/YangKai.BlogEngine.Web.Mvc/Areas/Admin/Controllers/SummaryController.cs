using System.Web.Mvc;
using YangKai.BlogEngine.ServiceProxy;

namespace YangKai.BlogEngine.Web.Mvc.Areas.Admin.Controllers
{
    public class SummaryController : Controller
    {
        // 首页
        // GET: /Admin/Home/
        public ActionResult Index()
        {
            if (!QueryFactory.User.IsLogin())
            {
                return RedirectToAction("Login", "Account");
            }
            return View();
        }
    }
}
