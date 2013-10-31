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
            feed.Write(AppDomain.CurrentDomain.BaseDirectory + Config.Path.ARTICLES_RSS_PATH);
        }

        private RssItem BuildPostItem(Post item)
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
            foreach (var item in data)
            {
                channel.Items.Add(BuildCommentItem(item));
            }
            var feed = new RssFeed(Encoding.UTF8);
            feed.Channels.Add(channel);
            feed.Write(AppDomain.CurrentDomain.BaseDirectory + Config.Path.COMMENTS_RSS_PATH);
        }

        private RssItem BuildCommentItem(Comment item)
        {
            item.Post = Proxy.Repository<Post>().Get(item.PostId);

            var link = string.Format("{0}/#!/post/{1}", Config.URL.Domain, item.Post.Url);

            var rssItem = new RssItem
            {
                Title = item.Content,
                Link = new Uri(link),
                Description = string.Format("{0}<br /><br />评论于:《{1}》", item.Content.Replace("\r","<br />"), item.Post.Title),
                PubDate = item.CreateDate,
                Author = item.Author,
                Guid = new RssGuid {Name = item.CommentId.ToString()}
            };
            return rssItem;
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
            foreach (var item in data)
            {
                channel.Items.Add(BuildIssueItem(item));
            }
            var feed = new RssFeed(Encoding.UTF8);
            feed.Channels.Add(channel);
            feed.Write(AppDomain.CurrentDomain.BaseDirectory + Config.Path.ISSUES_RSS_PATH);
        }

        private RssItem BuildIssueItem(Issue item)
        {
            var link = string.Format("{0}/#!/issue", Config.URL.Domain);

            var rssItem = new RssItem
            {
                Title = item.Title,
                Link = new Uri(link),
                Description = string.Format("{0}:{1}<br />","Project",item.Project)
                + string.Format("{0}:{1}<br /><br />", "Status", item.Statu)
                +item.Content.Replace("\r", "<br />"),
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