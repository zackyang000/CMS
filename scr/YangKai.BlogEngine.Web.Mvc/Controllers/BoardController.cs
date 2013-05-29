using System;
using System.Linq;
using System.Web.Mvc;
using AtomLab.Utility;
using YangKai.BlogEngine.Common;
using YangKai.BlogEngine.Modules.BoardModule.Events;
using YangKai.BlogEngine.ServiceProxy;
using YangKai.BlogEngine.Web.Mvc.Filters;
using YangKai.BlogEngine.Web.Mvc.Models;

namespace YangKai.BlogEngine.Web.Mvc.Controllers
{
    public class BoardController : Controller
    {
        // 留言页面
        [AcceptVerbs(HttpVerbs.Get)]
        public ViewResult Index()
        {
            var cookie = WebGuestCookie.Load();
            var viewModel = new BoardViewModel { Author = cookie.Name };
            return View(viewModel);
        }
    }
}