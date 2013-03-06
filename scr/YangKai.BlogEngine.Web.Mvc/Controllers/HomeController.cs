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
            return View();
        }

        [ActionName("layout-header-menu")]
        [ChildActionOnly]
        public PartialViewResult Menu()
        {
            IList<Channel> entities = QueryFactory.Post.FindAllByNotDeletion();
            return PartialView(entities);
        }

        [ActionName("layout-header-caption")]
        [ChildActionOnly]
        public PartialViewResult Caption(string channelUrl, string mainClassUrl)
        {
            Channel channel = null;
            if (!string.IsNullOrEmpty(mainClassUrl))
            {
                channel = QueryFactory.Post.GetGroup(mainClassUrl).Channel;
            }
            if (!string.IsNullOrEmpty(channelUrl))
            {
                channel = QueryFactory.Post.GetChannel(channelUrl);
            }
            return channel == null ? PartialView() : PartialView((object)channel.Description);
        }

        /// <summary>
        /// channelUrl与mainClassUrl有且应仅有一种,则为Post
        /// 列表或详细页面,才需要显示导航部分.否则返回空.
        /// </summary>
        [ActionName("layout-header-nav")]
        [ChildActionOnly]
        public PartialViewResult Nav(string channelUrl, string mainClassUrl)
        {
            IList<Group> entities = null;

            if (!string.IsNullOrEmpty(channelUrl))
            {
                entities = QueryFactory.Post.GetGroupsByChannelUrl(channelUrl);
                ViewData["channelUrl"] = channelUrl;
            }
            if (!string.IsNullOrEmpty(mainClassUrl))
            {
                entities = QueryFactory.Post.GetGroupsByGroupUrl(mainClassUrl);
                ViewData["channelUrl"] = QueryFactory.Post.GetGroup(mainClassUrl).Channel.Url;
            }

            if (Request.Url != null) ViewData["url"] = Request.Url.AbsolutePath.Substring(1);

            return entities == null ? PartialView() : PartialView(entities);
        }

        /// <summary>
        /// banner显示
        /// </summary>
        [ActionName("layout-header-banner")]
        [ChildActionOnly]
        public PartialViewResult Banner(string channelUrl, string mainClassUrl)
        {
            string bannerUrl = null;

            if (!string.IsNullOrEmpty(channelUrl))
            {
                var channel = QueryFactory.Post.GetChannel(channelUrl);
                bannerUrl = channel.BannerUrl;
            }
            if (!string.IsNullOrEmpty(mainClassUrl))
            {
                var group = QueryFactory.Post.GetGroup(mainClassUrl);
                bannerUrl = group.Channel.BannerUrl;
            }

            return PartialView((object)bannerUrl);
        }
    }
}