using System;
using System.Web.Mvc;
using AtomLab.Utility;
using YangKai.BlogEngine.Common;
using YangKai.BlogEngine.Modules.BoardModule.Events;
using YangKai.BlogEngine.Modules.BoardModule.Objects;
using YangKai.BlogEngine.ServiceProxy;

namespace YangKai.BlogEngine.Web.Mvc.Controllers
{
    public class BoardController : Controller
    {
        //
        // 留言列表页面
        // GET: /board
        [ActionName("index")]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Index()
        {
            var data = QueryFactory.Board.FindAll(Int32.MaxValue);
            return View(data);
        }

        //
        // 最新留言页面
        // GET: /board/sidebar-recent-messages
        [ActionName("recentmessages")]
        [AcceptVerbs(HttpVerbs.Get)]
        [OutputCache(Duration = 300)]
        public PartialViewResult RecentMessages()
        {
            return PartialView(QueryFactory.Board.FindAll(10));
        }

        //
        // 新增留言页面
        // GET: /board/add
        [ActionName("add")]
        [AcceptVerbs(HttpVerbs.Get)]
        public PartialViewResult Add()
        {
            var cookie = WebGuestCookie.Load();
            var entity = new Board
                             {
                                               Author = cookie.Name,
                             };
            return PartialView(entity);
        }

        //
        // 新增留言
        // POST: /board/add
        [ActionName("add")]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Add(Board data)
        {
            data.Ip = Request.UserHostAddress;
            data.Address = IpLocator.GetIpLocation(data.Ip);

            try
            {
                CommandFactory.CreateBoard(data);
                var index = QueryFactory.Board.Count();
                return Json(new {result = true, index});
            }
            catch (Exception e)
            {
                return Json(new {result = false, reason = e.Message});
            }
        }

        //
        // 删除留言
        // POST: /board/delete
        [ActionName("delete")]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Delete(Guid id)
        {
            try
            {
                CommandFactory.Run(new BoardDeleteEvent() { BoardId = id });
                return Json(new {result = true});
            }
            catch (Exception e)
            {
                return Json(new {result = false, reason = e.Message});
            }
        }
    }
}