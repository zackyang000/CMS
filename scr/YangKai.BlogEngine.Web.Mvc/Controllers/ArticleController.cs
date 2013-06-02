using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Mvc;
using System.Linq;
using AtomLab.Domain.Infrastructure;
using Webdiyer.WebControls.Mvc;
using YangKai.BlogEngine.Common;
using YangKai.BlogEngine.Modules.CommonModule.Objects;
using YangKai.BlogEngine.Modules.PostModule.Commands;
using YangKai.BlogEngine.Modules.PostModule.Objects;
using YangKai.BlogEngine.ServiceProxy;
using YangKai.BlogEngine.Web.Mvc.Models;

namespace YangKai.BlogEngine.Web.Mvc.Controllers
{
    public class ArticleController : ApiController
    {
        public PageList<PostViewModel> Get(int? page = 1, string channel = null, string group = null,
                          string category = null, string tag = null,
                          string date = null, string search = null)
        {
            DateTime? calendar = null;
            if (!string.IsNullOrEmpty(date))
            {
                DateTime myDate;
                if (DateTime.TryParse(date + "-01", out myDate))
                {
                    calendar = myDate;
                }
            }

            var data = QueryFactory.Instance.Post.FindAllByNormal(page ?? 1, Config.Setting.PAGE_SIZE, channel, group,
                                                                  category, tag, calendar, search);

            //保存搜索记录
            if (!string.IsNullOrEmpty(search))
            {
                var log = Log.CreateSearchLog(search);
                CommandFactory.Instance.Create(log);
            }

            return new PageList<PostViewModel>(Config.Setting.PAGE_SIZE)
                {
                    DataList = data.DataList.ToViewModels(),
                    TotalCount = data.TotalCount
                };
        }

        public PostViewModel Get(string id)
        {
            var data = QueryFactory.Instance.Post.Find(id);

            if (data == null || data.PostStatus == (int) PostStatusEnum.Trash)
            {
                return null;
            }

            CommandFactory.Instance.Run(new PostBrowseEvent {PostId = data.PostId});

            return data.ToViewModel();
        }

        public object Get(Guid id,string action)
        {
            if (action == "nav")//上一篇 & 下一篇
            {
                var prePost = QueryFactory.Instance.Post.PrePost(id) ?? new Post();
                var nextPost = QueryFactory.Instance.Post.NextPost(id) ?? new Post();

                var list = new List<Post>();
                list.AddRange(new List<Post> { prePost , nextPost});
                return list.Select(p => new { p.Title, p.Url });
            }
            if (action == "related")//相关文章
            {
                return QueryFactory.Instance.Post.FindAllByTag(id, 7).ToViewModels();
            }
            return null;
        }

        //public ActionResult Calendar(string channelUrl)
        //{
        //    if (!string.IsNullOrEmpty(channelUrl))
        //    {
        //        ViewBag.Channel = QueryFactory.Instance.Post.GetChannel(channelUrl);
        //    }
        //    ViewBag.CalendarList = QueryFactory.Instance.Post.GroupByCalendar(channelUrl);
        //    var data = QueryFactory.Instance.Post.FindAll(channelUrl);
        //    return View(data);
        //}

        ////相关文章 || 随便看看  
        //[ActionName("detail-related")]
        //public ActionResult PostRelated(Guid postId)
        //{
        //    var data = QueryFactory.Instance.Post.FindAllByTag(postId, 7);
        //    ViewBag.IsExistPostRelated = true;
        //    if (data.Count == 0)
        //    {
        //        data = QueryFactory.Instance.Post.FindAllByRandom(postId, 7);
        //        ViewBag.IsExistRelatedPosts = false;
        //    }
        //    return View(data);
        //}
    }
}