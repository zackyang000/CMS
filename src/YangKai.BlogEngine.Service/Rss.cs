using System;
using System.Linq;
using System.Text;
using AtomLab.Core;
using Rss;
using YangKai.BlogEngine.Common;
using YangKai.BlogEngine.Domain;

namespace YangKai.BlogEngine.Service
{
    public class Rss
    {
        public static Rss Current
        {
            get { return new Rss(); }
        }

        public void BuildPost()
        {
            var data = Proxy.Repository<Post>().GetAll(p => p.PostStatus == (int)PostStatusEnum.Publish).OrderByDescending(p => p.CreateDate).Take(50);
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
                                  Description = Config.Literal.SITE_NAME,
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
                                  Author = item.CreateUser,
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

        public void BuildComment()
        {
            var data = Proxy.Repository<Comment>().GetAll(p => !p.IsDeleted).OrderByDescending(p => p.CreateDate).Take(50);
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
                                  Description = string.Format("{0} - 评论",  Config.Literal.SITE_NAME),
                                  PubDate = now,
                                  LastBuildDate = now,
                                  Language = "zh-cn",
                                  Copyright = Config.Literal.COPYRIGHT
                              };
            return channel;
        }

        private RssItem BuildCommentRssItem(Comment item)
        {
            item.Post = Proxy.Repository<Post>().Get(item.PostId);
            var link = string.Format("{0}/#!/post/{1}", Config.URL.Domain, item.Post.Url);

            var rssItem = new RssItem
                              {
                                  Title = item.Content,
                                  Link = new Uri(link),
                                  Description = string.Format("{0}<br /><br />《{1}》", item.Content, item.Post.Title),
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
    }
}