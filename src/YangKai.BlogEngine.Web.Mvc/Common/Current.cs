using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Http.OData.Query;
using AtomLab.Core;
using AtomLab.Utility;
using YangKai.BlogEngine.Common;
using YangKai.BlogEngine.Domain;
using YangKai.BlogEngine.Service;
using YangKai.BlogEngine.Web.Mvc.Extension;
using YangKai.BlogEngine.Web.Mvc.Filters;

namespace YangKai.BlogEngine.Web.Mvc
{
    public class Current
    {
        public static User User
        {
            get
            {
                return new User
                {
                    UserName = EncryptionCookieHelper.Load("__Username__"),
                    LoginName = EncryptionCookieHelper.Load("__LoginName__"),
                    Email = EncryptionCookieHelper.Load("__Email__"),
                    Avatar = EncryptionCookieHelper.Load("__Avatar__"),
                    Password = EncryptionCookieHelper.Load("__Pwd__"),
                };
            }
            set
            {
                if (value != null)
                {
                    EncryptionCookieHelper.Add("__Username__", value.UserName, 180);
                    EncryptionCookieHelper.Add("__LoginName__", value.LoginName, 180);
                    EncryptionCookieHelper.Add("__Email__", value.Email, 180);
                    EncryptionCookieHelper.Add("__Avatar__", value.Avatar, 180);
                    EncryptionCookieHelper.Add("__Pwd__", value.Password, 180);
                }
                else
                {
                    EncryptionCookieHelper.Remove("__Username__");
                    EncryptionCookieHelper.Remove("__LoginName__");
                    EncryptionCookieHelper.Remove("__Email__");
                    EncryptionCookieHelper.Remove("__Avatar__");
                    EncryptionCookieHelper.Remove("__Pwd__");
                }
            }
        }

        /// <summary>
        /// 是否已登录
        /// </summary>
        public static bool IsLogin
        {
            get
            {
                return !string.IsNullOrEmpty(User.UserName);
            }
        }
    }
}