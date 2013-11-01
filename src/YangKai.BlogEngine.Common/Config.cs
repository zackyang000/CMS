using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace YangKai.BlogEngine.Common
{
    public class Config
    {
        public class Path
        {
            /// <summary>
            /// 物理根路径
            /// </summary>
            public static readonly string PHYSICAL_ROOT_PATH = AppDomain.CurrentDomain.BaseDirectory;
            //public static readonly string PHYSICAL_ROOT_PATH = HttpContext.Current.Server.MapPath("~/");

            /// <summary>
            /// 文章RSS文件路径
            /// </summary>
            public const string ARTICLES_RSS_PATH = "/feed_articles.xml";

            /// <summary>
            /// 评论RSS文件路径
            /// </summary>
            public const string COMMENTS_RSS_PATH = "/feed_comments.xml";

            /// <summary>
            /// Issues RSS文件路径
            /// </summary>
            public const string ISSUES_RSS_PATH = "/feed_issues.xml";

            /// <summary>
            /// logo图片路径
            /// </summary>
            public const string LOGO_PIC_PATH = "/Content/Image/logo.gif";

            /// <summary>
            /// 默认avatar头像地址
            /// </summary>
            public const string AUTHOR_DEFAULT_AVATAR_PATH = "/Content/img/avatar.png";

            /// <summary>
            /// 自动下载图片文件夹
            /// </summary>
            public const string REMOTE_PICTURE_FOLDER = "/upload/offsite/";
        }

        public class URL
        {
            /// <summary>
            /// 网站域名
            /// </summary>
            public static string Domain
            {
                get
                {
                    return GetConfig("Domain");
                }
            }
        }

        public class Literal
        {
            /// <summary>
            /// 网站标题
            /// </summary>
            public static string SITE_NAME
            {
                get
                {
                    return GetConfig("SiteName");
                }
            }

            /// <summary>
            /// 版权信息
            /// </summary>
            public static string COPYRIGHT
            {
                get
                {
                    return GetConfig("Copyright");
                }
            }
        }

        private static string GetConfig(string key)
        {
            //TODO:需要缓存结果以减少I/O
            var value = ConfigurationManager.AppSettings[key];
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ConfigurationErrorsException(string.Format("the '{0}' must not be null or empty.", key));
            }
            return value;
        }
    }
}