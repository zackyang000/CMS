using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace YangKai.BlogEngine.Common
{
    public class Site
    {
        /// <summary>
        /// 物理根路径
        /// </summary>
        public static readonly string ROOT_PATH = System.Web.HttpContext.Current.Server.MapPath("~/");

        /// <summary>
        /// 页面标题(head_title)
        /// </summary>
        public const string PAGE_TITLE = " - iShare";

        /// <summary>
        /// 网站描述
        /// </summary>
        public const string DESCRIPTION = "iShare描述";

        /// <summary>
        /// 站长名字
        /// </summary>
        public const string NAME = "杨凯";

        /// <summary>
        /// 站长昵称
        /// </summary>
        public const string NICKNAME = "我是你的猪";

        /// <summary>
        /// 站长EMail
        /// </summary>
        public const string WEB_MASTER_EMAIL = "zackyang@outlook.com";

        /// <summary>
        /// 站长头像
        /// </summary>
        public static readonly string AVATAR_PATH = HttpContext.Current.Server.MapPath("~/Content/Image/myAvatar.png");
    }
}
