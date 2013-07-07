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
        IDictionary<Category, int> StatGroupByCategory(string groupUrl);
        IList<Category> GetCategories(string groupUrl);
        IList<Channel> FindAllByNotDeletion();
        Channel GetChannel(string channelUrl);
        Comment GetComment(Guid commentId);
        IList<Comment> GetComments(Guid postId);
        IList<Comment> GetCommentsRecent(string channelUrl);
        Int32 GetCommentsCount(Guid postId);
        Group GetGroup(Guid key);
        Group GetGroup(string groupUrl);
        IList<Group> GetGroupsByNotDeletion();
        IList<Group> GetGroupsByChannelUrl(string channelUrl);
        IList<Group> GetGroupsByGroupUrl(string groupUrl);
        TagsDictionary GetTagsList(string groupUrl);
        Post Find(Guid postId);
        Post Find(string titleUrl);
        CalendarDictionary GroupByCalendar(string channelUrl);
        IList<Post> FindAll(string channelUrl);

        PageList<Post> FindAll(int pageIndex, int pageSize, string channelUrl, string groupUrl,
                                               string categoryUrl, string tagName, DateTime? calendar, string searchKey);

        PageList<Post> FindAllByNormal(int pageIndex, int pageSize, string channelUrl, string groupUrl,
                                                       string categoryUrl, string tagName, DateTime? calendar, string searchKey);

        IList<Post> FindAllByNormal(int count);
        IList<Post> FindAllByTag(Guid postId, int count);
        IList<Post> FindAllByRandom(Guid postId, int count);
        Post PrePost(Guid postId);
        Post NextPost(Guid postId);
    }
}