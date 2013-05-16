using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AtomLab.Utility;
using YangKai.BlogEngine.Common;
using YangKai.BlogEngine.Modules.PostModule.Commands;
using YangKai.BlogEngine.Modules.PostModule.Objects;
using YangKai.BlogEngine.ServiceProxy;
using YangKai.BlogEngine.Web.Mvc.Filters;
using YangKai.BlogEngine.Web.Mvc.Models;

namespace YangKai.BlogEngine.Web.Mvc.Controllers
{
    public class CommentController : Controller
    {
        // 评论页面
        [AcceptVerbs(HttpVerbs.Get)]
        public PartialViewResult Index(string id)
        {
            ViewBag.PostId = QueryFactory.Instance.Post.Find(id).PostId; //添加评论时使用

            WebGuestCookie cookie = WebGuestCookie.Load();
            var entity = new CommentViewModel
            {
                Author = cookie.Name,
                Email = cookie.Email,
                Url = cookie.Url
            };

            return PartialView(entity);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult List(Guid id)
        {
            IList<Comment> comments = QueryFactory.Instance.Post.GetComments(id);
            if (!WebMasterCookie.IsLogin)
            {
                comments = comments.Where(p => !p.IsDeleted).ToList();
            }
            return Json(comments.ToViewModels(),JsonRequestBehavior.AllowGet);
        }

        // 添加评论
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult Add(CommentViewModel viewModel)
        {
            var entity = viewModel.ToEntity();
            entity.Pic = WebGuestCookie.Load().Pic ?? string.Empty;
            entity.Ip = Request.UserHostAddress;
            entity.Address = IpLocator.GetIpLocation(entity.Ip);

            try
            {
                if (WebMasterCookie.IsLogin)
                {
                    var admin = WebMasterCookie.Load();
                    entity.Author = admin.Name;
                    entity.IsAdmin = true;
                }
                else
                {
                    WebGuestCookie.Save(entity.Author, entity.Email, entity.Url, true);
                }
                CommandFactory.Instance.Create(entity);
                return Json(new { result = true, model = entity.ToViewModel() });
            }
            catch (Exception e)
            {
                return Json(new {result = false, reason = e.Message});
            }
        }

        // 删除评论
        [UserAuthorize]
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult Delete(Guid id)
        {
            try
            {
                CommandFactory.Instance.Run(new CommentDeleteEvent() { CommentId = id });
                return Json(new {result = true});
            }
            catch (Exception e)
            {
                return Json(new {result = false, reason = e.Message});
            }
        }

        // 恢复评论
        [UserAuthorize]
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult Renew(Guid id)
        {
            try
            {
                CommandFactory.Instance.Run(new CommentRenewEvent() { CommentId = id }); 
                return Json(new {result = true});
            }
            catch (Exception e)
            {
                return Json(new {result = false, reason = e.Message});
            }
        }

        // 最新评论
        [AcceptVerbs(HttpVerbs.Get)]
        [OutputCache(CacheProfile = "side")]
        public JsonResult Recent(string channelurl)
        {
            var comments = QueryFactory.Instance.Post.GetCommentsRecent(channelurl);
            return Json(comments.ToViewModels(), JsonRequestBehavior.AllowGet);
        }
    }
}