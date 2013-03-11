using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YangKai.BlogEngine.Common
{
    public class Config
    {
        public class Path
        {
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
        }

        public class URL
        {
            /// <summary>
            /// 网站根域名
            /// </summary>
            public const string BASE_URL = "http://www.woshinidezhu.com";
        }

        public class Literal
        {
            /// <summary>
            /// 网站标题
            /// </summary>
            public const string BASE_TITLE = "iShare";

            /// <summary>
            /// 版权信息
            /// </summary>
            public const string COPYRIGHT = "&copy; Powered by 我是你的猪(YangKai QQ:83448327), 2008-2013, All Rights Reserved. 蜀ICP备09016538号.";
        }
    }
}