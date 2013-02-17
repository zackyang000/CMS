using System;
using System.Collections.Generic;
using System.Web.Mvc;
using AtomLab.Utility;
using YangKai.BlogEngine.Common;
using YangKai.BlogEngine.Modules.PostModule.Commands;
using YangKai.BlogEngine.Modules.PostModule.Objects;
using YangKai.BlogEngine.ServiceProxy;

namespace YangKai.BlogEngine.Web.Mvc.Controllers
{
    public class CommentController : Controller
    {
        //
        // 评论列表页面
        // Get: /comment/list/{PostId}
        [ActionName("list")]
        [AcceptVerbs(HttpVerbs.Get)]
        public PartialViewResult List(string id)
        {
            Post post = QueryFactory.Post.Find(id);
            IList<Comment> comments = QueryFactory.Post.GetComments(post.PostId);
            return PartialView(comments);
        }

        //
        // 最新评论页面
        // Get: /comment/recentcomments?channelurl={ChannelUrl}&mainclassurl={MainClassUrl}
        [ActionName("recentcomments")]
        [AcceptVerbs(HttpVerbs.Get)]
        [OutputCache(Duration = 300)]
        public PartialViewResult RecentComments(string channelUrl, string mainClassUrl)
        {
            if (string.IsNullOrEmpty(channelUrl))
                channelUrl = QueryFactory.Post.GetGroup(mainClassUrl).Channel.Url;
            return PartialView(QueryFactory.Post.GetCommentsNewest(channelUrl));
        }

        //
        // 添加评论页面
        // Get: /comment/add/{PostId}
        [ActionName("add")]
        [AcceptVerbs(HttpVerbs.Get)]
        public PartialViewResult Add(string id)
        {
            ViewData["ArticleId"] = QueryFactory.Post.Find(id).PostId; //添加评论时使用

            WebGuestCookie cookie = WebGuestCookie.Load();
            var entity = new Comment
                             {
                                     Author = cookie.Name,
                                     Email = cookie.Email,
                                     Url = cookie.Url,
                                     Pic = cookie.Pic
                             };
            return PartialView(entity);
        }

        //
        // 添加评论
        // Post: /comment/add
        [ActionName("add")]
        [AcceptVerbs(HttpVerbs.Post)]
        //[BuildCommentRssAttribute]
        public JsonResult Add(Comment data)
        {
            data.Pic = WebGuestCookie.Load().Pic ?? string.Empty;
            data.Ip = Request.UserHostAddress;
            data.Address = IpLocator.GetIpLocation(data.Ip);

          

            try
            {
                if (WebMasterCookie.IsLogin)
                {
                    var admin = WebMasterCookie.Load();
                    data.Author = admin.Name;
                    data.IsAdmin = true;
                }
                else
                {
                    WebGuestCookie.Save(data.Author, data.Email, data.Url, true);
                }
                CommandFactory.CreateComment(data);
                int index = QueryFactory.Post.GetCommentsCount(data.PostId);
                return Json(new {result = true, index});
            }
            catch (Exception e)
            {
                return Json(new {result = false, reason = e.Message});
            }
        }

        //
        // 删除评论
        // Post: /comment/delete
        [ActionName("delete")]
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult Delete(Guid id)
        {
            try
            {
                CommandFactory.Run(new CommentDeleteEvent(){CommentId = id});
                return Json(new {result = true});
            }
            catch (Exception e)
            {
                return Json(new {result = false, reason = e.Message});
            }
        }

        //
        // 恢复评论
        // Post: /comment/renew
        [ActionName("renew")]
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult Renew(Guid id)
        {
            try
            {
                CommandFactory.Run(new CommentRenewEvent() { CommentId = id }); 
                return Json(new {result = true});
            }
            catch (Exception e)
            {
                return Json(new {result = false, reason = e.Message});
            }
        }

        //
        // 注销
        // Post: /comment/login-off
        [ActionName("login-off")]
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult LoginOff()
        {
            WebGuestCookie.Remove();
            return Json(new {result = true});
        }
    }
}