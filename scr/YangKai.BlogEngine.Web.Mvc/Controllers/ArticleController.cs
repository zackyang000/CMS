using System;
using System.Web.Mvc;
using System.Linq;
using Webdiyer.WebControls.Mvc;
using YangKai.BlogEngine.Common;
using YangKai.BlogEngine.Modules.CommonModule.Objects;
using YangKai.BlogEngine.Modules.PostModule.Commands;
using YangKai.BlogEngine.Modules.PostModule.Objects;
using YangKai.BlogEngine.ServiceProxy;

namespace YangKai.BlogEngine.Web.Mvc.Controllers
{
    public class ArticleController : Controller
    {
        public ActionResult Index(int? id,string channelUrl,  string mainClassUrl, string c, string t, string d, string k)
        {
            bool hasChannel=!string.IsNullOrEmpty(channelUrl);
            bool hasMainClass=!string.IsNullOrEmpty(mainClassUrl);
            Channel channel = null;
            Group mainClass = null;
            if(hasMainClass)
            {
                mainClass=QueryFactory.Post.GetGroup(mainClassUrl);
                channel=mainClass.Channel;
            }
            else if (hasChannel)
            {
                channel = QueryFactory.Post.GetChannel(channelUrl);
            }
            ViewBag.Group = mainClass;
            ViewBag.Channel= channel;
            ViewBag.Title =string.Format(Config.Format.PAGE_TITLE,channel.Name  );

            ViewBag.Keywords = string.Empty;
            ViewBag.Description = channel.Description;
            ViewBag.SubCaption = channel.Description;
            return View();
        }

        /// <summary>
        /// 供Ajax分页使用
        /// </summary>
        /// <param name="id"></param>
        /// <param name="channelUrl">Channel</param>
        /// <param name="mainClassUrl">Group</param>
        /// <param name="c">Category</param>
        /// <param name="t">Tag</param>
        /// <param name="d">Date</param>
        /// <param name="k">Key</param>
        /// <returns></returns>
        [ActionName("index-list")]
        public ActionResult List(int? id, string channelUrl, string mainClassUrl, string c, string t, string d, string k)
        {
            var data = QueryFactory.User.IsLogin()
                           ? QueryFactory.Post.FindAll(id ?? 1, 20, channelUrl, mainClassUrl, c, t, null, k)
                           : QueryFactory.Post.FindAllByNormal(id ?? 1, 20, channelUrl, mainClassUrl, c, t, null, k);
            //搜索记录
            if (!string.IsNullOrEmpty(k))
            {
                var log = Log.CreateSearchLog(k);
                CommandFactory.Create(log);
            }

            var pagedList = new PagedList<Post>(data.DataList, id ?? 1, 20, data.TotalCount);
            return View(pagedList);
        }

        public ActionResult Detail(string id, string mainClassUrl)
        {
            Post data = QueryFactory.Post.Find(id);

            if (!QueryFactory.User.IsLogin())
            {
                if (data == null) return View("_NotFound");
                if (data.PostStatus == (int)PostStatusEnum.Trash) return View("_Removed");
            }
            CommandFactory.Run(new PostBrowseEvent() {PostId = data.PostId});

            var mainClass = QueryFactory.Post.GetGroup(mainClassUrl);
            var    channel = mainClass.Channel;
            ViewBag.Group = mainClass;
            ViewBag.Channel = channel;

            ViewBag.Title =string.Format(Config.Format.PAGE_TITLE,  data.Title );

            ViewBag.Keywords = data.Tags != null ? string.Join(",", data.Tags.Select(p => p.Name).ToList()) : string.Empty;
            ViewBag.Description = data.Title;
            ViewBag.SubCaption = data.Group.Channel.Description;

            return View(data);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult FastDetail(Guid postId)
        {
            try
            {
                var data = QueryFactory.Post.Find(postId);
                if (data.PostStatus == (int)PostStatusEnum.Trash)
                {
                    return Json(new { success = false, reason = "文章已被删除." });
                }
                return Json(new {success=true, content = data.Pages.FirstOrDefault().Content });
            }
            catch (Exception e)
            {
                return Json(new { success = false, reason = e.Message });
            }
        }

        [ActionName("calendar")]
        public ActionResult GetCalendar(string channelUrl)
        {
            if (!string.IsNullOrEmpty(channelUrl))
            {
                ViewBag.Channel = QueryFactory.Post.GetChannel(channelUrl);
            }
            ViewBag.CalendarList = QueryFactory.Post.GroupByCalendar(channelUrl);
            var data = QueryFactory.Post.FindAll(channelUrl);
            return View(data);
        }

        //上一篇 && 下一篇
        public ActionResult PostNavi(Guid postId)
        {
            ViewBag.PrePost = QueryFactory.Post.PrePost(postId);
            ViewBag.NextPost = QueryFactory.Post.NextPost(postId);
            return View();
        }

        //相关文章 || 随便看看  
        public ActionResult PostRelated(Guid postId)
        {
            var data = QueryFactory.Post.FindAllByTag(postId, 7);
            ViewBag.IsExistPostRelated = true;
            if (data.Count == 0)
            {
                data = QueryFactory.Post.FindAllByRandom(postId, 7);
                ViewBag.IsExistRelatedPosts = false;
            }
            return View(data);
        }

        #region 侧栏

        //标签分组
        [AcceptVerbs(HttpVerbs.Get)]
        [ChildActionOnly]
        [OutputCache(Duration = 120)]
        public PartialViewResult GroupByCategory(string mainClassUrl)
        {
            ViewBag.GroupUrl = mainClassUrl;
            return PartialView(QueryFactory.Post.StatGroupByCategory(mainClassUrl));
        }

        ////最多浏览
        //[AcceptVerbs(HttpVerbs.Get)]
        //[ChildActionOnly]
        //[OutputCache(Duration = 120)]
        //public PartialViewResult RankByViewCount(string mainClassUrl)
        //{
        //    ViewBag.GroupUrl = mainClassUrl;
        //    return PartialView("RankList", _postServices.GetListByViewCount(mainClassUrl));
        //}

        ////最多回复
        //[AcceptVerbs(HttpVerbs.Get)]
        //[ChildActionOnly]
        //[OutputCache(Duration = 120)]
        //public PartialViewResult RankByReplyCount(string mainClassUrl)
        //{
        //    ViewBag.GroupUrl = mainClassUrl;
        //    return PartialView("RankList", _postServices.GetListByReplyCount(mainClassUrl));
        //}

        #endregion
    }
}