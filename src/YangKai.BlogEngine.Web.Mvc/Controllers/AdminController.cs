using System.Web.Mvc;
using YangKai.BlogEngine.Web.Mvc.Filters;

namespace YangKai.BlogEngine.Web.Mvc.Controllers
{
    public class AdminController : Controller
    {
        [UserAuthorizeForPage]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        public JsonResult GetUser()
        {
            return Json(Current.User,JsonRequestBehavior.AllowGet);
        }
    }
}