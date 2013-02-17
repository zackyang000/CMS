using System;
using System.Collections.Generic;
using AtomLab.Domain.Infrastructure;
using YangKai.BlogEngine.Model;
using YangKai.BlogEngine.Modules.PostModule.Objects;

namespace YangKai.BlogEngine.IQueryServices
{
    public interface IPostQueryServices
    {
        Category GetCategory(Guid categoryId);
        Category GetCategory(string categoryUrl);
        IList<PostStatInfo> StatGroupByCategory(string mainClassUrl);
        IList<Category> GetCategories(string mainClassUrl);
        IList<Channel> FindAllByNotDeletion();
        Channel GetChannel(string channelUrl);
        Comment GetComment(Guid commentId);
        IList<Comment> GetComments(Guid postId);
        IList<Comment> GetCommentsNewest(string channelUrl);
        Int32 GetCommentsCount(Guid postId);
        Group GetGroup(Guid key);
        Group GetGroup(string mainClassUrl);
        IList<Group> GetGroupsByNotDeletion();
        IList<Group> GetGroupsByChannelUrl(string channelUrl);
        IList<Group> GetGroupsByGroupUrl(string mainClassUrl);
        TagsDictionary GetTagsList(string mainClassUrl);
        Post Find(Guid postId);
        Post Find(string titleUrl);
        CalendarDictionary GroupByCalendar(string channelUrl);
        IList<Post> FindAll(string channelUrl);

        PageList<Post> FindAll(int pageIndex, int pageSize, string channelUrl, string mainClassUrl,
                                               string categoryUrl, string tagName, DateTime? calendar, string searchKey);

        PageList<Post> FindAllByNormal(int pageIndex, int pageSize, string channelUrl, string mainClassUrl,
                                                       string categoryUrl, string tagName, DateTime? calendar, string searchKey);

        IList<Post> FindAllByNormal(int count);
        IList<Post> FindAllByTag(Guid postId, int count);
        IList<Post> FindAllByRandom(Guid postId, int count);
        Post PrePost(Guid postId);
        Post NextPost(Guid postId);
    }
}