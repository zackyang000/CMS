using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YangKai.BlogEngine.Common
{
    public class Config
    {
        public class Setting
        {
            /// <summary>
            /// 每页文章条数
            /// </summary>
            public const int PAGE_SIZE = 20;
        }

        public class Path
        {
            /// <summary>
            /// 物理根路径
            /// </summary>
            public static readonly string PHYSICAL_ROOT_PATH = HttpContext.Current.Server.MapPath("~/");

            /// <summary>
            /// 文章RSS文件路径
            /// </summary>
            public const string ARTICLES_RSS_PATH = "/feed_articles.xml";

            /// <summary>
            /// 评论RSS文件路径
            /// </summary>
            public const string COMMENTS_RSS_PATH = "/feed_comments.xml";

            /// <summary>
            /// logo图片路径
            /// </summary>
            public const string LOGO_PIC_PATH = "/Content/Image/logo.gif";

            /// <summary>
            /// 自动下载图片文件夹
            /// </summary>
            public const string REMOTE_PICTURE_FOLDER = "/upload/offsite/";
        }

        public class URL
        {
            /// <summary>
            /// 网站根域名
            /// </summary>
            public const string Domain = "http://www.woshinidezhu.com";
        }

        public class Literal
        {
            /// <summary>
            /// 网站标题
            /// </summary>
            public const string SITE_NAME = "iShare";

            /// <summary>
            /// 网站描述
            /// </summary>
            public const string DESCRIPTION = "Share Link Fun";

            /// <summary>
            /// 版权信息
            /// </summary>
            public const string COPYRIGHT = "&copy; Powered by YangKai , 2008-2013, All Rights Reserved. 蜀ICP备09016538号.";
        }

        public class Format
        {
            /// <summary>
            /// 页面标题格式
            /// </summary>
            public const string PAGE_TITLE = "{0} - iShare";
        }
    }
}