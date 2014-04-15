using System;
using System.IO;
using System.Linq;
using AtomLab.Core;
using RazorEngine;
using Rss;
using YangKai.BlogEngine.Common;
using YangKai.BlogEngine.Domain;
using Encoding = System.Text.Encoding;

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
            var data =
                Proxy.Repository<Post>()
                    .GetAll(p => p.PostStatus == (int) PostStatusEnum.Publish)
                    .OrderByDescending(p => p.CreateDate)
                    .Take(10);
            var channel = new RssChannel
            {
                Title = string.Format("{0} - 文章", Config.Literal.SITE_NAME),
                Link = new Uri(Config.URL.Domain),
                Description = string.Format("{0} - 文章", Config.Literal.SITE_NAME),
                PubDate = DateTime.Now,
                LastBuildDate = DateTime.Now,
                Language = "zh-cn",
                Copyright = Config.Literal.COPYRIGHT
            };
            foreach (var item in data)
            {
                channel.Items.Add(CreatePostItem(item));
            }
            var feed = new RssFeed(Encoding.UTF8);
            feed.Channels.Add(channel);
            feed.Write(Config.Path.PHYSICAL_ROOT_PATH + Config.Path.ARTICLES_RSS_PATH);
        }

        private RssItem CreatePostItem(Post item)
        {
            return new RssItem
            {
                Title = item.Title ?? string.Empty,
                Link = new Uri(string.Format("{0}/post/{1}", Config.URL.Domain, item.Url ?? string.Empty)),
                Description = item.Content ?? string.Empty,
                PubDate = item.PubDate,
                Author = item.CreateUser ?? string.Empty,
                Guid = new RssGuid {Name = item.PostId.ToString()}
            };
        }

        public void BuildComment()
        {
            var data =Proxy.Repository<Comment>()
                .GetAll(p => !p.IsDeleted)
                .OrderByDescending(p => p.CreateDate)
                .Take(50);
            var channel = new RssChannel
            {
                Title = string.Format("{0} - 评论", Config.Literal.SITE_NAME),
                Link = new Uri(Config.URL.Domain),
                Description = string.Format("{0} - 评论", Config.Literal.SITE_NAME),
                PubDate = DateTime.Now,
                LastBuildDate = DateTime.Now,
                Language = "zh-cn",
                Copyright = Config.Literal.COPYRIGHT
            };

            var template = File.ReadAllText(Config.Path.PHYSICAL_ROOT_PATH + "bin/RSS/comment.cshtml");

            foreach (var item in data)
            {
                channel.Items.Add(CreateCommentItem(item, template));
            }
            var feed = new RssFeed(Encoding.UTF8);
            feed.Channels.Add(channel);
            feed.Write(Config.Path.PHYSICAL_ROOT_PATH + Config.Path.COMMENTS_RSS_PATH);
        }

        private RssItem CreateCommentItem(Comment item,string template)
        {
            item.Post = Proxy.Repository<Post>().Get(item.PostId);

            return new RssItem
            {
                Title = "Re:"+item.Post.Title,
                Link = new Uri(string.Format("{0}/post/{1}", Config.URL.Domain, item.Post.Url)),
                Description = Razor.Parse(template, new
                {
                    Author = item.Author ?? string.Empty,
                    Content = item.Content ?? string.Empty,
                    CreateDate = item.CreateDate,
                    Title = item.Post.Title ?? string.Empty,
                    User = item.Post.CreateUser ?? string.Empty,
                }),
                PubDate = item.CreateDate,
                Author = item.Author,
                Guid = new RssGuid {Name = item.CommentId.ToString()}
            };
        }
    }
}