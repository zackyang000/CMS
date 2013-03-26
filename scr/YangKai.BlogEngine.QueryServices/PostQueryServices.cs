using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AtomLab.Domain;
using AtomLab.Domain.Infrastructure;
using YangKai.BlogEngine.IQueryServices;
using YangKai.BlogEngine.Model;
using YangKai.BlogEngine.Modules.PostModule.Objects;
using YangKai.BlogEngine.Modules.PostModule.Repositories;

namespace YangKai.BlogEngine.QueryServices
{
    public class PostQueryServices : IPostQueryServices
    {
        private readonly CategoryRepository _categoryRepository = InstanceLocator.Current.GetInstance<CategoryRepository>();
        private readonly ChannelRepository _channelRepository = InstanceLocator.Current.GetInstance<ChannelRepository>();
        private readonly CommentRepository _commentRepository = InstanceLocator.Current.GetInstance<CommentRepository>();
        private readonly GroupRepository _groupRepository = InstanceLocator.Current.GetInstance<GroupRepository>();
        private readonly PostRepository _postRepository = InstanceLocator.Current.GetInstance<PostRepository>();
        private readonly TagRepository _tagRepository = InstanceLocator.Current.GetInstance<TagRepository>();

        private const string PowerLess = "您没有相关权限.";

        public PostQueryServices(IUnitOfWork context)
        {
        }

        public Category GetCategory(Guid categoryId)
        {
            return _categoryRepository.Get(categoryId);
        }

        public Category GetCategory(string categoryUrl)
        {
            return _categoryRepository.Get(p => p.Url == categoryUrl);
        }

        public IList<PostStatInfo> StatGroupByCategory(string groupUrl)
        {
            throw new Exception("未实现");//TODO
        }

        public IList<Category> GetCategories(string groupUrl)
        {
            return _categoryRepository.GetAll(p => p.Group.Url == groupUrl).ToList();
        }

        public IList<Channel> FindAllByNotDeletion()
        {
            return _channelRepository.GetAll(p => p.IsDeleted == false).ToList();
        }

        public Channel GetChannel(string channelUrl)
        {
            return _channelRepository.Get(p => p.Url == channelUrl);
        }



        public Comment GetComment(Guid commentId)
        {
            return _commentRepository.Get(commentId);
        }

        public IList<Comment> GetComments(Guid postId)
        {
            return _commentRepository.GetAll(p => p.PostId == postId && !p.IsDeleted, new OrderByExpression<Comment, DateTime>(p => p.CreateDate)).ToList();
        }

        public IList<Comment> GetCommentsNewest(string channelUrl)
        {
            return
                _commentRepository.GetAll(10, p => p.Post.Group.Channel.Url == channelUrl && !p.IsDeleted,
                                          new OrderByExpression<Comment, DateTime>(p => p.CreateDate, OrderMode.DESC))
                                  .ToList();
        }

        public Int32 GetCommentsCount(Guid postId)
        {
            return _commentRepository.Count(p => p.PostId == postId && !p.IsDeleted);
        }



        public Group GetGroup(Guid key)
        {
            return _groupRepository.Get(key);
        }

        public Group GetGroup(string groupUrl)
        {
            return _groupRepository.Get(p => p.Url == groupUrl);
        }

        public IList<Group> GetGroupsByNotDeletion()
        {
            return _groupRepository.GetAll(p => !p.IsDeleted).ToList();
        }

        public IList<Group> GetGroupsByChannelUrl(string channelUrl)
        {
            return _groupRepository.GetAll(p => p.Channel.Url == channelUrl&&!p.IsDeleted).ToList();
        }

        public IList<Group> GetGroupsByGroupUrl(string groupUrl)
        {
            var group = _groupRepository.Get(p => p.Url == groupUrl && !p.IsDeleted);
            return _groupRepository.GetAll().Where(p => p.Channel.Url == group.Channel.Url && !p.IsDeleted).ToList();
        }

        //Tag列表
        public TagsDictionary GetTagsList(string groupUrl)
        {
            return _tagRepository.GetTagsFrequency(groupUrl);
        }

        public Post Find(Guid postId)
        {
            return _postRepository.Get(postId);
        }

        public Post Find(string titleUrl)
        {
            return _postRepository.Get(p => p.Url == titleUrl);
        }

        public CalendarDictionary GroupByCalendar(string channelUrl)
        {
            return _postRepository.GetPostsByCalendar(channelUrl);
        }

        public IList<Post> FindAll(string channelUrl)
        {
            return _postRepository.GetAll(p => p.Group.Channel.Url == channelUrl).ToList();
        }

        public PageList<Post> FindAll(int pageIndex, int pageSize, string channelUrl, string groupUrl,
                                      string categoryUrl, string tagName, DateTime? calendar, string searchKey)
        {
            return _postRepository.FindAllByPaged(pageIndex, pageSize, channelUrl, groupUrl,
                                                  categoryUrl, tagName, calendar, searchKey, null);
        }

        public PageList<Post> FindAllByNormal(int pageIndex, int pageSize, string channelUrl, string groupUrl,
                                              string categoryUrl, string tagName, DateTime? calendar, string searchKey)
        {
            return _postRepository.FindAllByPaged(pageIndex, pageSize, channelUrl, groupUrl,
                                                  categoryUrl, tagName, calendar, searchKey, PostStatusEnum.Publish);
        }

        public IList<Post> FindAllByNormal(int count)
        {
            return FindAllByNormal(1, count, null, null, null, null, null, null).DataList;
        }

        public IList<Post> FindAllByTag(Guid postId, int count)
        {
            return _postRepository.FindAllByTag(postId, count);
        }

        public IList<Post> FindAllByRandom(Guid postId, int count)
        {
            var post = Find(postId);

            Expression<Func<Post, bool>> specExpr = p => p.Group.Url == post.Group.Url
                                                          && p.PostStatus == (int)PostStatusEnum.Publish;
            return _postRepository.GetAll(specExpr).OrderBy(x => Guid.NewGuid()).Take(count).ToList();
        }

        public Post PrePost(Guid postId)
        {
            return _postRepository.GetPrePost(postId);
        }

        public Post NextPost(Guid postId)
        {
            return _postRepository.GetNextPost(postId);
        }
    }
}
