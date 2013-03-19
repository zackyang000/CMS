
using AtomLab.Utility;

namespace YangKai.BlogEngine.Common
{
    public class WebGuestCookie
    {
        #region [Private Const]

        public const string KName = "guestname";
        public const string KEmail = "guestemail";
        public const string KUrl = "guesturl";
        public const string KPic = "guestpic";

        #endregion

        #region [Properties]

        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Url { get; private set; }
        public string Pic {get; private set; }

        #endregion

        #region [Constructor]

        private WebGuestCookie()
        {
            Name = EncryptionCookieHelper.Load(KName);
            Email = EncryptionCookieHelper.Load(KEmail);
            Url = EncryptionCookieHelper.Load(KUrl);
            Pic = EncryptionCookieHelper.Load(KPic);
        }

        private WebGuestCookie(string name)
        {
            Name = name;
            Email = EncryptionCookieHelper.Load(KEmail);
            Url = EncryptionCookieHelper.Load(KUrl);
            Pic = EncryptionCookieHelper.Load(KPic);
            Save(true);
        }

        private WebGuestCookie(string name, string email, string url,string pic, bool isRemember)
        {
            Name = name;
            Email = email;
            Url = url;
            Pic = pic;
            Save(isRemember);
        }

        #endregion

        #region [Public Methods]

        /// <summary>
        /// 保存cookies初始化类
        /// </summary>
        public static WebGuestCookie Save(string name)
        {
            return new WebGuestCookie(name);
        }

        /// <summary>
        /// 保存cookies初始化类
        /// </summary>
        public static WebGuestCookie Save(string name, string email, string url, bool isRemember)
        {
            var data = Load();
            return new WebGuestCookie(name, email, url, data.Pic,  isRemember);
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
            EncryptionCookieHelper.Remove(KName);
            EncryptionCookieHelper.Remove(KEmail);
            EncryptionCookieHelper.Remove(KUrl);
            EncryptionCookieHelper.Remove(KPic);
        }
        #endregion

        #region [Private Methods]

        private void Save(bool isRemember)
        {
            if (isRemember)
            {
                EncryptionCookieHelper.Add(KName, Name, 300);
                EncryptionCookieHelper.Add(KEmail, Email, 300);
                EncryptionCookieHelper.Add(KUrl, Url, 300);
                EncryptionCookieHelper.Add(KPic, Pic, 300);
            }
            else
            {
                EncryptionCookieHelper.Add(KName, Name);
                EncryptionCookieHelper.Add(KEmail, Email);
                EncryptionCookieHelper.Add(KUrl, Url);
                EncryptionCookieHelper.Add(KPic, Pic);
            }
        }

        #endregion
    }
}
