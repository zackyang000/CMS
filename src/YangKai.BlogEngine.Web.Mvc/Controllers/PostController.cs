using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Http;
using System.Web.Http.OData.Query;
using System.Web.Mvc;
using System.Linq;
using AtomLab.Core;
using Webdiyer.WebControls.Mvc;
using YangKai.BlogEngine.Common;
using YangKai.BlogEngine.Domain;
using YangKai.BlogEngine.Service;
using YangKai.BlogEngine.Web.Mvc.Extension;

namespace YangKai.BlogEngine.Web.Mvc.Controllers
{
    public class PostController : ApiController
    {
        [Queryable(AllowedQueryOptions = AllowedQueryOptions.All)]
        public IQueryable<Post> Get(ODataQueryOptions options)
        {
            var data = Proxy.ODataRepository<Post>().GetAll(p => !p.IsDeleted);
            PageHelper.SetLinkHeader(data, options, Request);
            return data;

            //保存搜索记录
            //            if (!string.IsNullOrEmpty(search))
            //            {
            //                var log = Log.CreateSearchLog(search);
            //                Proxy.Repository.Log.Add(log);
            //            }
        }

        public Post Get(string id)
        {
            var data = Proxy.Repository<Post>().Get(p => p.Url == id);

            if (!Current.IsLogin)
            {
                if (data == null || data.PostStatus == (int)PostStatusEnum.Trash)
                {
                    return null;
                }
            }

            data.ReplyCount++;
            Proxy.Repository<Post>().Update(data);

            return data;
        }

        public object Get(Guid id, string action)
        {
            if (action == "nav")//上一篇 & 下一篇
            {
                var post = Proxy.Repository<Post>().Get(id);
               var prePost = GetPrePost(post)??new Post();
               var nextPost = GetNextPost(post) ?? new Post();

                var list = new List<Post>();
                list.AddRange(new List<Post> { prePost , nextPost});
                return list.Select(p => new { p.Title, p.Url });
            }
            if (action == "related")//相关文章
            {
                var post = Proxy.Repository<Post>().Get(id);
                IList<Post> result = new List<Post>();
                if (post.Tags != null)
                {
                    foreach (Tag tag in post.Tags)
                    {
                        Proxy.Repository<Tag>().GetAll(p => p.Name == tag.Name)
                                      .Select(p => p.Post).ToList().ForEach(result.Add);
                    }
                }
                return result.Distinct().Where(p => p.PostId != id && p.Group == post.Group).Take(10).ToList();
            }
            return null;
        }

        private Post GetPrePost(Post entity)
        {
            Expression<Func<Post, bool>> specExpr = p => p.PubDate < entity.PubDate
                                                         && p.PostStatus == (int)PostStatusEnum.Publish
                                                         && p.GroupId == entity.GroupId;
            var result = Proxy.Repository<Post>().GetAll(1, specExpr,
                                new OrderByExpression<Post, DateTime>(
                                    p => p.PubDate, OrderMode.DESC));
            return result.FirstOrDefault();
        }

        private Post GetNextPost(Post entity)
        {
            Expression<Func<Post, bool>> specExpr =
                p => p.PubDate > entity.PubDate
                     && p.PostStatus == (int)PostStatusEnum.Publish
                     && p.GroupId == entity.GroupId;
            var result = Proxy.Repository<Post>().GetAll(1, specExpr,
                                new OrderByExpression<Post, DateTime>(
                                    p => p.PubDate));
            return result.FirstOrDefault();
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