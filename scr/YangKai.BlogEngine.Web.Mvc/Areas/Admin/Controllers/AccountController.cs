using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YangKai.BlogEngine.ServiceProxy;

namespace YangKai.BlogEngine.Web.Mvc.Areas.Admin.Controllers
{
    public class AccountController : Controller
    {
        // 登录页面
        // GET: /Admin/Account/Login
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Login()
        {
            return View();
        }

        // 登录请求
        // POST: /Admin/Account/Login
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Login(FormCollection collection)
        {
            try
            {
                var username = collection["user"];
                var password = collection["pwd"];
                var isRemember = Convert.ToBoolean(collection["remember"]);
                bool login = QueryFactory.User.AccountLoginValidate(username, password, isRemember, 60);
                return Json(new { result = login });
            }
            catch (Exception e)
            {
                return Json(new { result = false, reason = e.Message });
            }
        }

        // 注销请求
        // POST: /Admin/Account/LoginOff
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult LoginOff(FormCollection collection)
        {
            try
            {
                QueryFactory.User.LoginOff();
                return Json(new { result = true });
            }
            catch (Exception e)
            {
                return Json(new { result = false, reason = e.Message });
            }
        }
    }
}
