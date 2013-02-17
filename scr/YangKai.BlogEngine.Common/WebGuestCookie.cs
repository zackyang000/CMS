using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YangKai.BlogEngine.Common
{
    public enum LoginAPIEnum
    {
        QQ
    }

    public class WebGuestCookie
    {
        #region [Private Const]

        public const string KName = "guestname";
        public const string KEmail = "guestemail";
        public const string KUrl = "guesturl";
        public const string KPic = "guestpic";
        public const string KLoginAPI = "guestapi";

        #endregion

        #region [Properties]

        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Url { get; private set; }
        public string Pic {get; private set; }
        public string LoginAPI { get; private set; }

        #endregion

        #region [Constructor]

        private WebGuestCookie()
        {
            Name = Cookie.Load(WebGuestCookie.KName);
            Email = Cookie.Load(WebGuestCookie.KEmail);
            Url = Cookie.Load(WebGuestCookie.KUrl);
            Pic = Cookie.Load(WebGuestCookie.KPic);
            LoginAPI = Cookie.Load(WebGuestCookie.KLoginAPI);
        }

        private WebGuestCookie(string name, string email, string url,string pic,string loginAPI, bool IsRemember)
        {
            Name = name;
            Email = email;
            Url = url;
            Pic = pic;
            LoginAPI = loginAPI;
            Save(IsRemember);
        }

        #endregion

        #region [Public Methods]

        /// <summary>
        /// 保存cookies初始化类
        /// </summary>
        public static WebGuestCookie Save(string name, string email, string url, bool isRemember)
        {
            var data = Load();
            return new WebGuestCookie(name, email, url, data.Pic, data.LoginAPI, isRemember);
        }

        /// <summary>
        /// 保存cookies初始化类
        /// </summary>
        public static WebGuestCookie Save(string name, string pic, LoginAPIEnum loginAPI, bool isRemember)
        {
            var data = WebGuestCookie.Load();
            return new WebGuestCookie(name,data.Email,data.Url, pic, loginAPI.ToString(), isRemember);
        }

        /// <summary>
        /// 读取cookies初始化类
        /// </summary>
        public static WebGuestCookie Load()
        {
            return new WebGuestCookie();
        }

        /// <summary>
        /// 移除cookies
        /// </summary>
        public static void Remove()
        {
            Cookie.Remove(WebGuestCookie.KName);
            Cookie.Remove(WebGuestCookie.KEmail);
            Cookie.Remove(WebGuestCookie.KUrl);
            Cookie.Remove(WebGuestCookie.KPic);
            Cookie.Remove(WebGuestCookie.KLoginAPI);
        }

        public static bool IsLoginAPI
        {
            get { return Load().LoginAPI != string.Empty; }
        }

        #endregion

        #region [Private Methods]

        private void Save(bool isRemember)
        {
            if (isRemember)
            {
                Cookie.Add(WebGuestCookie.KName, Name, 300);
                Cookie.Add(WebGuestCookie.KEmail, Email, 300);
                Cookie.Add(WebGuestCookie.KUrl, Url, 300);
                Cookie.Add(WebGuestCookie.KPic, Pic, 300);
                Cookie.Add(WebGuestCookie.KLoginAPI, LoginAPI, 300);
            }
            else
            {
                Cookie.Add(WebGuestCookie.KName, Name);
                Cookie.Add(WebGuestCookie.KEmail, Email);
                Cookie.Add(WebGuestCookie.KUrl, Url);
                Cookie.Add(WebGuestCookie.KPic, Pic);
                Cookie.Add(WebGuestCookie.KLoginAPI, LoginAPI);
            }
        }

        #endregion
    }
}
