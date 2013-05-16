using System;
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
    public class ArticleController : BaseController
    {
        public ActionResult Index(string type,int? id, string channelUrl, string groupUrl, string category, string tag, string date, string search)
        {
            if (string.IsNullOrEmpty(type))
            {
                bool hasChannel = !string.IsNullOrEmpty(channelUrl);
                bool hasGroup = !string.IsNullOrEmpty(groupUrl);
                Channel channel = null;
                Group group = null;

                if (hasChannel)
                {
                    channel = QueryFactory.Instance.Post.GetChannel(channelUrl);
                }
                else if (hasGroup)
                {
                    group = QueryFactory.Instance.Post.GetGroup(groupUrl);
                    channel = group.Channel;
                }
                else
                {
                    throw new ArgumentException("Must have a channelUrl or groupUrl!");
                }

                ViewBag.Group = group;
                ViewBag.Channel = channel;
                ViewBag.Title = string.Format(Config.Format.PAGE_TITLE, channel.Name);
                ViewBag.SubCaption = channel.Description;
                ViewBag.Keywords = string.Empty;
                ViewBag.Description = channel.Description;

                return View();
            }
            else
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

                var data = QueryFactory.Instance.User.IsLogin()
               ? QueryFactory.Instance.Post.FindAll(id ?? 1, 20, channelUrl, groupUrl, category, tag, calendar, search)
               : QueryFactory.Instance.Post.FindAllByNormal(id ?? 1, 20, channelUrl, groupUrl, category, tag, calendar, search);

                //保存搜索记录
                if (!string.IsNullOrEmpty(search))
                {
                    var log = Log.CreateSearchLog(search);
                    CommandFactory.Instance.Create(log);
                }

                var pagelist = new PageList<PostViewModel>()
                {
                    DataList = data.DataList.ToViewModels(),
                    TotalCount = data.TotalCount
                };

                return PagedJson(pagelist);
            }
        }

        public ActionResult Detail(string id, string groupUrl)
        {
            var data = QueryFactory.Instance.Post.Find(id);

            if (data == null)
            {
                return View("_NotFound");
            }
            if (!QueryFactory.Instance.User.IsLogin()&&(data.PostStatus == (int) PostStatusEnum.Trash))
            {
                return View("_Removed");
            }

            CommandFactory.Instance.Run(new PostBrowseEvent {PostId = data.PostId});

            ViewBag.Group = data.Group;
            ViewBag.Channel = data.Group.Channel;
            ViewBag.Title = string.Format(Config.Format.PAGE_TITLE, data.Title);
            ViewBag.SubCaption = data.Group.Channel.Description;
            ViewBag.Keywords = data.Tags != null ? string.Join(",", data.Tags.Select(p => p.Name).ToList()) : data.Title;
            ViewBag.Description = data.Title;

            return View(data.ToViewModel());
        }

        public ActionResult Calendar(string channelUrl)
        {
            if (!string.IsNullOrEmpty(channelUrl))
            {
                ViewBag.Channel = QueryFactory.Instance.Post.GetChannel(channelUrl);
            }
            ViewBag.CalendarList = QueryFactory.Instance.Post.GroupByCalendar(channelUrl);
            var data = QueryFactory.Instance.Post.FindAll(channelUrl);
            return View(data);
        }

        //相关文章 || 随便看看  
        public ActionResult PostRelated(Guid postId)
        {
            var data = QueryFactory.Instance.Post.FindAllByTag(postId, 7);
            ViewBag.IsExistPostRelated = true;
            if (data.Count == 0)
            {
                data = QueryFactory.Instance.Post.FindAllByRandom(postId, 7);
                ViewBag.IsExistRelatedPosts = false;
            }
            return View(data);
        }

        //上一篇 && 下一篇
        public ActionResult PostNavi(Guid postId)
        {
            ViewBag.PrePost = QueryFactory.Instance.Post.PrePost(postId);
            ViewBag.NextPost = QueryFactory.Instance.Post.NextPost(postId);
            return View();
        }
    }
}