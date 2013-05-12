using System.Collections.Generic;
using System.Web.Mvc;
using YangKai.BlogEngine.Modules.PostModule.Objects;
using YangKai.BlogEngine.ServiceProxy;

namespace YangKai.BlogEngine.Web.Mvc.Controllers
{
    public class HomeController : Controller
    {

        [ActionName("index")]
        public ActionResult Index()
        {
            IList<Channel> entities = QueryFactory.Instance.Post.FindAllByNotDeletion();
            return View(entities);
        }

        [ActionName("layout-header-menu")]
        [ChildActionOnly]
        public PartialViewResult Menu()
        {
            IList<Channel> entities = QueryFactory.Instance.Post.FindAllByNotDeletion();
            return PartialView(entities);
        }

        [ActionName("layout-header-caption")]
        [ChildActionOnly]
        public PartialViewResult Caption(string channelUrl, string groupUrl)
        {
            Channel channel = null;
            if (!string.IsNullOrEmpty(groupUrl))
            {
                channel = QueryFactory.Instance.Post.GetGroup(groupUrl).Channel;
            }
            if (!string.IsNullOrEmpty(channelUrl))
            {
                channel = QueryFactory.Instance.Post.GetChannel(channelUrl);
            }
            return channel == null ? PartialView() : PartialView((object)channel.Description);
        }

        /// <summary>
        /// channelUrl与groupUrl有且应仅有一种,则为Post
        /// 列表或详细页面,才需要显示导航部分.否则返回空.
        /// </summary>
        [ActionName("layout-header-nav")]
        [ChildActionOnly]
        public PartialViewResult Nav(string channelUrl, string groupUrl)
        {
            IList<Group> entities = null;

            if (!string.IsNullOrEmpty(channelUrl))
            {
                entities = QueryFactory.Instance.Post.GetGroupsByChannelUrl(channelUrl);
                ViewBag.ChannelUrl = channelUrl;
            }
            if (!string.IsNullOrEmpty(groupUrl))
            {
                entities = QueryFactory.Instance.Post.GetGroupsByGroupUrl(groupUrl);
                ViewBag.ChannelUrl = QueryFactory.Instance.Post.GetGroup(groupUrl).Channel.Url;
            }

            if (Request.Url != null) ViewBag.Url = Request.Url.AbsolutePath.Substring(1);

            return entities == null ? PartialView() : PartialView(entities);
        }

        /// <summary>
        /// banner显示
        /// </summary>
        [ActionName("layout-header-banner")]
        [ChildActionOnly]
        public PartialViewResult Banner(string channelUrl, string groupUrl)
        {
            string bannerUrl = null;

            if (!string.IsNullOrEmpty(channelUrl))
            {
                var channel = QueryFactory.Instance.Post.GetChannel(channelUrl);
                bannerUrl = channel.BannerUrl;
            }
            if (!string.IsNullOrEmpty(groupUrl))
            {
                var group = QueryFactory.Instance.Post.GetGroup(groupUrl);
                bannerUrl = group.Channel.BannerUrl;
            }

            return PartialView((object)bannerUrl);
        }
    }
}