using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AtomLab.Domain.Infrastructure;
using YangKai.BlogEngine.Model;
using YangKai.BlogEngine.Modules.PostModule.Objects;

namespace YangKai.BlogEngine.Modules.PostModule.Repositories
{
    public class PostRepository : GuidRepository<Post>
    {
        private readonly TagRepository _tagRepository;

        public PostRepository(IUnitOfWork context)
            : base(context)
        {
            _tagRepository=new TagRepository(context);
        }

        public CalendarDictionary GetPostsByCalendar(string channelUrl)
                {
                    var data = GetAll(p => p.PostStatus == (int)PostStatusEnum.Publish && p.Group.Channel.Url == channelUrl)
                    .GroupBy(p => new
                    {
                        year = p.PubDate.Year,
                        month = p.PubDate.Month
                    })
                    .Select(g => new
                    {
                        year = g.Key.year,
                        month = g.Key.month,
                        count = g.Count()
                    });
                    var result= data.ToDictionary(k => DateTime.Parse(string.Format("{0}.{1}.1", k.year, k.month)), v => v.count);
                   return new CalendarDictionary(){Data=result};
                }

        public IList<Post> FindAllByTag(Guid postId, int count)
        {
            //TODO:代码仅完成功能,可能存在严重问题
            Post thePost = Get(postId);
            IList<Post> result1 = new List<Post>();
            if (thePost.Tags != null)
                foreach (Tag tag in thePost.Tags)
                {
                    var result = (from a in _tagRepository.GetAll()
                                  where a.Name == tag.Name
                                  where a.Post.Group.Url == tag.Post.Group.Url
                                  orderby Guid.NewGuid()
                                  select a.Post).ToList();
                    result.ToList().ForEach(result.Add);
                }
            return result1.Distinct().Where(p => p.PostId != postId).Take(count).ToList();
        }

        public Post GetPrePost(Guid postId)
                {
                    var entity = Get(postId);
                    Expression<Func<Post, bool>> specExpr = p => p.PubDate < entity.PubDate
                        && p.PostStatus == (int)PostStatusEnum.Publish
                        && p.GroupId == entity.GroupId;
                    var result = GetAll(1, specExpr,
                        new OrderByExpression<BlogEngine.Modules.PostModule.Objects.Post, DateTime>(p => p.PubDate, OrderMode.DESC));
                    return result.FirstOrDefault();
                }

        public Post GetNextPost(Guid postId)
                {
                    var entity = Get(postId);
                    Expression<Func<BlogEngine.Modules.PostModule.Objects.Post, bool>> specExpr = p => p.PubDate > entity.PubDate
                        && p.PostStatus == (int)PostStatusEnum.Publish
                        && p.GroupId == entity.GroupId;
                    var result = GetAll(1, specExpr,
                        new OrderByExpression<BlogEngine.Modules.PostModule.Objects.Post, DateTime>(p => p.PubDate));
                    return result.FirstOrDefault();
                }



        public List<Post> FindAll( int pageIndex, int pageSize, string channelUrl, string groupUrl,
                             string categoryUrl, string tagName, DateTime? calendar, string searchKey, PostStatusEnum? postStatu)
                {
                    //TODO 此处应该为空
                    Expression<Func<Post, bool>> specExpr = p => p.PostStatus == (int)PostStatusEnum.Publish;

                    if (postStatu == null)
                    {
                        specExpr = p => true;
                    }
                    else
                    {
                        specExpr = p => p.PostStatus == (int)postStatu;
                    }
                    if (channelUrl.HasValue())
                    {
                        specExpr = specExpr.And(p => p.Group.Channel.Url == channelUrl);
                    }
                    if (groupUrl.HasValue())
                    {
                        specExpr = specExpr.And(p => p.Group.Url == groupUrl);
                    }
        
                    if (calendar.HasValue)
                    {
                        specExpr = specExpr.And(p => p.PubDate.Year == calendar.Value.Year
                                    && p.PubDate.Month == calendar.Value.Month);
                    }
                    if (searchKey.HasValue())
                    {
                        specExpr = specExpr.And(p => p.Title.Contains(searchKey));
                    }
                    if (!string.IsNullOrEmpty(categoryUrl))
                    {
                        //TODO:specExpr查询方法有误,直接使用linq返回结果.
                        //specExpr = specExpr.And(new PostsByCategoryUrlCriteria(CategoryUrl).Query);
                        var data = from a in GetAll()
                                   where a.Categorys.Any(p => p.Url == categoryUrl)
                                   where a.Group.Url == groupUrl
                                   where a.PostStatus == (int)PostStatusEnum.Publish
                                   where a.PubDate <= DateTime.Now
                                   orderby a.PubDate descending
                                   select a;
                      return data.ToList();
                    }
                    if (!string.IsNullOrEmpty(tagName))
                    {
                        //TODO:specExpr查询方法有误,直接使用linq返回结果.
                        //specExpr = specExpr.And(new PostsByTagNameCriteria(TagName).Query);
                        var data = from a in GetAll()
                                   where a.Tags.Any(p => p.Name == tagName)
                                   where a.Group.Url == groupUrl
                                   where a.PostStatus == (int)PostStatusEnum.Publish
                                   where a.PubDate <= DateTime.Now
                                   orderby a.PubDate descending
                                   select a;
                        return data.ToList();
                    }

                    var result =base.GetPage(pageIndex, pageSize, specExpr,
                        new OrderByExpression<Post, DateTime>(x => x.PubDate, OrderMode.DESC)).DataList;

                    return result.ToList();
                }

        public PageList<Post> FindAllByPaged(int pageIndex, int pageSize, string channelUrl, string groupUrl,
                             string categoryUrl, string tagName, DateTime? calendar, string searchKey, PostStatusEnum? postStatu)
                {
                    //TODO 应该为空
            Expression<Func<Post, bool>> specExpr;

                    if (postStatu == null)
                    {
                        specExpr = p => true;
                    }
                    else
                    {
                        specExpr = p => p.PostStatus == (int)postStatu;
                    }
                    if (channelUrl.HasValue())
                    {
                        specExpr = specExpr.And(p => p.Group.Channel.Url == channelUrl);
                    }
                    if (groupUrl.HasValue())
                    {
                        specExpr = specExpr.And(p => p.Group.Url == groupUrl);
                    }
        
                    if (calendar.HasValue)
                    {
                        specExpr = specExpr.And(p => p.PubDate.Year == calendar.Value.Year
                                    && p.PubDate.Month == calendar.Value.Month);
                    }
                    if (searchKey.HasValue())
                    {
                        specExpr = specExpr.And(p => p.Title.Contains(searchKey));
                    }
                    if (!string.IsNullOrEmpty(categoryUrl))
                    {
                        //TODO:specExpr查询方法有误,直接使用linq返回结果.
                        //specExpr = specExpr.And(p => p.Categorys.Any(q=>q.Url == e.CategoryUrl));
                        var data = from a in GetAll()
                                   where a.Categorys.Any(p => p.Url == categoryUrl)
                                   where a.Group.Url == groupUrl
                                   where a.PostStatus == (int)PostStatusEnum.Publish
                                   where a.PubDate <= DateTime.Now
                                   orderby a.PubDate descending
                                   select a;
                        var result = new PageList<BlogEngine.Modules.PostModule.Objects.Post>() { DataList = data.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList(), TotalCount = data.Count() };
                        return result;
                    }
                    if (!string.IsNullOrEmpty(tagName))
                    {
                        //TODO:specExpr查询方法有误,直接使用linq返回结果.
                        //specExpr = specExpr.And(new PostsByTagNameCriteria(TagName).Query);
                        var data = from a in GetAll()
                                   where a.Tags.Any(p => p.Name == tagName)
                                   where a.Group.Url == groupUrl
                                   where a.PostStatus == (int)PostStatusEnum.Publish
                                   where a.PubDate <= DateTime.Now
                                   orderby a.PubDate descending
                                   select a;
                        var result = new PageList<Post>() { DataList = data.Skip((pageIndex - 1) *pageSize).Take(pageSize).ToList(), TotalCount = data.Count() };
                        return result; 
                    }

                    var result1 = GetPage(pageIndex, pageSize, specExpr,
                      new OrderByExpression<Post, DateTime>(x => x.PubDate, OrderMode.DESC));
                    return result1; 
                }

        public new int Count()
        {
            return Count(p => p.PostStatus==(int)PostStatusEnum.Publish);
        }

        public new Post Get(Expression<Func<Post, bool>> spec)
        {
            return base.Get(spec);
        }
    }
}