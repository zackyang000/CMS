//===========================================================
// Copyright @ 2010 YangKai. All Rights Reserved.
// Framework: 4.0
// Author: 杨凯
// Email: yangkai-13896222@sohu.com
// QQ: 83448327
// CreateTime: 2/12/2011 2:07:39 PM
//===========================================================

using System;
using System.Web;
using System.Linq;
using System.Text;
using AtomLab.Domain;
using AtomLab.Domain.Infrastructure;
using Microsoft.Practices.Unity;
using Rss;
using YangKai.BlogEngine.Common;
using YangKai.BlogEngine.Modules.PostModule.Objects;

namespace YangKai.BlogEngine.Modules.PostModule.Services
{
    public class Rss
    {
        private readonly Repository<Post, Guid> _postRepository = InstanceLocator.Current.GetInstance<Repository<Post, Guid>>();
        private readonly Repository<Comment, Guid> _commentRepository = InstanceLocator.Current.GetInstance<Repository<Comment, Guid>>();

        #region  Public Static Methods

        public static void BuildPostRss()
        {
            var rss = InstanceLocator.Current.GetInstance<Rss>();
            rss.BuildPost();
        }

        public static void BuildCommentRss()
        {
            var rss = InstanceLocator.Current.GetInstance<Rss>();
            rss.BuildComment();
        }

        #endregion

        #region Private Methods

        private void BuildPost()
        {
            var data = _postRepository.GetAll(p => p.PostStatus == (int)PostStatusEnum.Publish).OrderByDescending(p => p.CreateDate).Take(50);
            var feed = new RssFeed(Encoding.UTF8);
            var channel = BuildPostRssChannel();
            foreach (var item in data)
            {
                channel.Items.Add(BuildPostRssItem(item));
            }
            feed.Channels.Add(channel);
            feed.Write(AppDomain.CurrentDomain.BaseDirectory + Config.Path.ARTICLES_RSS_PATH);
        }

        private RssChannel BuildPostRssChannel()
        {
            var now = DateTime.Now;
            var channel = new RssChannel
                              {
                                  Title = Config.Literal.SITE_NAME,
                                  Link = new Uri(Config.URL.Domain),
                                  Description = Config.Literal.DESCRIPTION,
                                  PubDate = now,
                                  LastBuildDate = now,
                                  Language = "zh-cn",
                                  Copyright = Config.Literal.COPYRIGHT
                              };
            return channel;
        }

        private RssItem BuildPostRssItem(Post item)
        {
            var link = string.Format("{0}/#!/post/{1}", Config.URL.Domain, item.Url);

            var rssItem = new RssItem
                              {
                                  Title = item.Title,
                                  Link = new Uri(link),
                                  Description = item.Content,
                                  PubDate = item.PubDate,
                                  //Author = item.PubAdmin.Name, //TODO:ef出错
                                  Guid = new RssGuid {Name = item.PostId.ToString()}
                              };
            foreach (var cat in item.Categorys)
            {
                rssItem.Categories.Add(new RssCategory
                                           {
                                               Name = string.Format("{0} - {1}", item.Group.Name, cat.Name)
                                           });
            }
            return rssItem;
        }

        private void BuildComment()
        {
            var data = _commentRepository.GetAll(p => !p.IsDeleted).OrderByDescending(p => p.CreateDate).Take(50);
            var feed = new RssFeed(Encoding.UTF8);
            var channel = BuildCommentRssChannel();
            foreach (var item in data)
            {
                channel.Items.Add(BuildCommentRssItem(item));
            }
            feed.Channels.Add(channel);
            feed.Write(AppDomain.CurrentDomain.BaseDirectory+Config.Path.COMMENTS_RSS_PATH);
        }

        private RssChannel BuildCommentRssChannel()
        {
            var now = DateTime.Now;
            var channel = new RssChannel
                              {
                                  Title = string.Format("{0} - 评论", Config.Literal.SITE_NAME),
                                  Link = new Uri(Config.URL.Domain),
                                  Description = string.Format("{0} - 评论", Config.Literal.DESCRIPTION),
                                  PubDate = now,
                                  LastBuildDate = now,
                                  Language = "zh-cn",
                                  Copyright = Config.Literal.COPYRIGHT
                              };
            return channel;
        }

        private RssItem BuildCommentRssItem(Comment item)
        {
            var link = string.Format("{0}/#!/post/{1}", Config.URL.Domain, item.Post.Url);

            var rssItem = new RssItem
                              {
                                  Title = item.Content,
                                  Link = new Uri(link),
                                  Description = string.Format("{0}<br />发表于:{1}", item.Content, item.Post.Title),
                                  PubDate = item.CreateDate,
                                  Author = item.Author,
                                  Guid = new RssGuid {Name = item.CommentId.ToString()}
                              };
            foreach (var cat in item.Post.Categorys)
            {
                rssItem.Categories.Add(new RssCategory
                                           {
                                               Name = string.Format("{0} - {1}", item.Post.Group.Name, cat.Name)
                                           });
            }
            return rssItem;
        }

        #endregion
    }
}