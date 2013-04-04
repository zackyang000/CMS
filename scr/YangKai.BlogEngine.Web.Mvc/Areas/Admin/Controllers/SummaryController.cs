using System.Web.Mvc;
using YangKai.BlogEngine.ServiceProxy;
using YangKai.BlogEngine.Web.Mvc.Filters;

namespace YangKai.BlogEngine.Web.Mvc.Areas.Admin.Controllers
{
    [UserAuthorize]
    public class SummaryController : Controller
    {
        // 首页
        // GET: /Admin/Home/
        public ActionResult Index()
        {
            return View();
        }
    }
}
