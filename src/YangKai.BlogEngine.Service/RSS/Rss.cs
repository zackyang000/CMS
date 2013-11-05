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
                    .Take(50);
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
                channel.Items.Add(BuildPostItem(item));
            }
            var feed = new RssFeed(Encoding.UTF8);
            feed.Channels.Add(channel);
            feed.Write(Config.Path.PHYSICAL_ROOT_PATH + Config.Path.ARTICLES_RSS_PATH);
        }

        private RssItem BuildPostItem(Post item)
        {
            var link = string.Format("{0}/post/{1}", Config.URL.Domain, item.Url);

            var rssItem = new RssItem
            {
                Title = item.Title,
                Link = new Uri(link),
                Description = item.Content,
                PubDate = item.PubDate,
                Author = item.CreateUser,
                Guid = new RssGuid {Name = item.PostId.ToString()}
            };
            return rssItem;
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
                channel.Items.Add(BuildCommentItem(item, template));
            }
            var feed = new RssFeed(Encoding.UTF8);
            feed.Channels.Add(channel);
            feed.Write(Config.Path.PHYSICAL_ROOT_PATH + Config.Path.COMMENTS_RSS_PATH);
        }

        private RssItem BuildCommentItem(Comment item,string template)
        {
            item.Post = Proxy.Repository<Post>().Get(item.PostId);

            var link = string.Format("{0}/post/{1}", Config.URL.Domain, item.Post.Url);

            return new RssItem
            {
                Title = "Re:"+item.Post.Title,
                Link = new Uri(link),
                Description = Razor.Parse(template, new
                {
                    Author = item.Author,
                    Content = item.Content,
                    CreateDate = item.CreateDate,
                    Title = item.Post.Title,
                    User = item.Post.CreateUser,
                }),
                PubDate = item.CreateDate,
                Author = item.Author,
                Guid = new RssGuid {Name = item.CommentId.ToString()}
            };
        }

        public void BuildIssue()
        {
            var data =
                Proxy.Repository<Issue>()
                    .GetAll(p=>!p.IsDeleted)
                    .OrderByDescending(p => p.CreateDate)
                    .Take(50);
            var channel = new RssChannel
            {
                Title = string.Format("{0} - Issues", Config.Literal.SITE_NAME),
                Link = new Uri(Config.URL.Domain),
                Description = string.Format("{0} - Issues", Config.Literal.SITE_NAME),
                PubDate = DateTime.Now,
                LastBuildDate = DateTime.Now,
                Language = "zh-cn",
                Copyright = Config.Literal.COPYRIGHT
            };

            var template = File.ReadAllText(Config.Path.PHYSICAL_ROOT_PATH + "bin/RSS/issue.cshtml");

            foreach (var item in data)
            {
                channel.Items.Add(BuildIssueItem(item, template));
            }
            var feed = new RssFeed(Encoding.UTF8);
            feed.Channels.Add(channel);
            feed.Write(Config.Path.PHYSICAL_ROOT_PATH + Config.Path.ISSUES_RSS_PATH);
        }

        private RssItem BuildIssueItem(Issue item, string template)
        {
            var link = string.Format("{0}/issue", Config.URL.Domain);

            var rssItem = new RssItem
            {
                Title = item.Project+" - "+item.Title,
                Link = new Uri(link),
                Description = Razor.Parse(template, new
                {
                    Title = item.Title,
                    Project = item.Project,
                    Statu = item.Statu,
                    Content = item.Content,
                    Author = item.Author,
                    CreateDate = item.CreateDate,
                    Result = item.Result ?? string.Empty,
                    User=item.LastEditUser??string.Empty,
                    EditDate = item.LastEditDate ?? DateTime.Now,
                }),
                PubDate = item.CreateDate,
                Author = item.CreateUser,
                Guid = new RssGuid { Name = item.IssueId.ToString() }
            };

            if (item.Result!=null)
                rssItem.Description += string.Format("<br /><br />{0}:<br />{1}", item.LastEditUser,
                    item.Result != null ? item.Result.Replace("\r", "<br />") : "");

            return rssItem;
        }
    }
}