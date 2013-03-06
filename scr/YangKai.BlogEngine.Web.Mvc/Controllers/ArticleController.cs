using System;
using System.Web.Mvc;
using System.Linq;
using Webdiyer.WebControls.Mvc;
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
            ViewBag.MainClass = mainClass;
            ViewBag.Channel= channel;
            ViewBag.Title = channel.Name + YangKai.BlogEngine.Common.Site.PAGE_TITLE;
            ViewBag.Keywords = string.Empty;
            ViewBag.Description = channel.Description;
            ViewBag.SubCaption = channel.Description;

            return View();
        }

        /// <summary>
        /// 供Ajax分页使用
        /// </summary>
        /// <param name="id"></param>
        /// <param name="channelUrl"></param>
        /// <param name="mainClassUrl"></param>
        /// <param name="c"></param>
        /// <param name="t"></param>
        /// <param name="d"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        [ActionName("index-list")]
        public ActionResult List(int? id, string channelUrl, string mainClassUrl, string c, string t, string d, string k)
        {
            DateTime? calendar = null;
            if (!string.IsNullOrEmpty(d))
            {
                int year;
                int month;
                //TODO:正则
                bool a1 = Int32.TryParse(d.Substring(0, 4), out year);
                bool a2 = Int32.TryParse(d.Substring(5, 2), out month);
                bool a3 = d.Length == 7;
                bool a4 = d.Substring(4, 1) == "-";
                //TODO:错误提示,用户体验.
                if (!(a1 && a2 && a3 && a4)) return HttpNotFound();
                calendar = new DateTime(year, month, 1);
            }
            var data = QueryFactory.User.IsLogin()
                           ? QueryFactory.Post.FindAll(id ?? 1, 20, channelUrl, mainClassUrl, c, t, calendar, k)
                           : QueryFactory.Post.FindAllByNormal(id ?? 1, 20, channelUrl, mainClassUrl, c, t,
                                                           calendar, k);
          
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

            if (!YangKai.BlogEngine.ServiceProxy.QueryFactory.User.IsLogin())
            {
                if (data == null) return View("_NotFound");
                if (data.PostStatus == (int)PostStatusEnum.Trash) return View("_Removed");
            }
            CommandFactory.Run(new PostBrowseEvent() {PostId = data.PostId});

            var mainClass = QueryFactory.Post.GetGroup(mainClassUrl);
            var    channel = mainClass.Channel;
            ViewBag.MainClass = mainClass;
            ViewBag.Channel = channel;

            ViewBag.Title = data.Title + YangKai.BlogEngine.Common.Site.PAGE_TITLE;
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
            ViewData["mainClassUrl"] = mainClassUrl;
            return PartialView(QueryFactory.Post.StatGroupByCategory(mainClassUrl));
        }

        ////最多浏览
        //[AcceptVerbs(HttpVerbs.Get)]
        //[ChildActionOnly]
        //[OutputCache(Duration = 120)]
        //public PartialViewResult RankByViewCount(string mainClassUrl)
        //{
        //    ViewData["MainClassUrl"] = mainClassUrl;
        //    return PartialView("RankList", _postServices.GetListByViewCount(mainClassUrl));
        //}

        ////最多回复
        //[AcceptVerbs(HttpVerbs.Get)]
        //[ChildActionOnly]
        //[OutputCache(Duration = 120)]
        //public PartialViewResult RankByReplyCount(string mainClassUrl)
        //{
        //    ViewData["MainClassUrl"] = mainClassUrl;
        //    return PartialView("RankList", _postServices.GetListByReplyCount(mainClassUrl));
        //}

        #endregion
    }
}