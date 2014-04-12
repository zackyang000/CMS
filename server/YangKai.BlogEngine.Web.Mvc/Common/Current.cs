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
using YangKai.BlogEngine.Web.Mvc.Filters;

namespace YangKai.BlogEngine.Web.Mvc
{
    public class Current
    {
        public static User User
        {
            get
            {
               
                var security = Proxy.Security();
                var user = security.AutoLogin();
                if (user != null)
                {
                    return user;
                }
                else
                {
                    return null;
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
                return User!=null;
            }
        }
    }
}