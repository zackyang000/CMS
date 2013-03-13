using System;
using AtomLab.Utility;

namespace YangKai.BlogEngine.Common
{
    public class WebMasterCookie
    {
        #region [Private Const]

        private const string KId = "uid";
        private const string KName = "username";

        #endregion

        #region [Properties]

        public Guid Id { get; private set; }
        public string Name { get; private set; }

        #endregion

        #region [Constructor]

        private WebMasterCookie()
        {
            string uid = EncryptionCookieHelper.Load(KId);
            Id = string.IsNullOrEmpty(uid) ? Guid.Empty : new Guid(uid);
            Name = EncryptionCookieHelper.Load(KName);
        }

        private WebMasterCookie(Guid id, string name, bool isRemember)
        {
            Id = id;
            Name = name;
            Save(isRemember);
        }

        #endregion

        #region [Public Methods]

        /// <summary>
        /// 保存cookies初始化类
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="isRemember"></param>
        /// <returns></returns>
        public static WebMasterCookie Save(Guid id, string name, bool isRemember)
        {
            return new WebMasterCookie(id,name,isRemember);
        }

        /// <summary>
        /// 读取cookies初始化类
        /// </summary>
        /// <returns></returns>
        public static WebMasterCookie Load()
        {
            return new WebMasterCookie();
        }

        /// <summary>
        /// 移除cookies
        /// </summary>
        public static void Remove()
        {
            EncryptionCookieHelper.Remove(KId);
            EncryptionCookieHelper.Remove(KName);
        }

        /// <summary>
        /// 移除cookies
        /// </summary>
        public void Clear()
        {
            Id = Guid.Empty;
            Name = string.Empty;
            EncryptionCookieHelper.Remove(KId);
            EncryptionCookieHelper.Remove(KName);
        }

        public static bool IsLogin
        {
            get { return Load().Id != Guid.Empty; }
        }

        #endregion

        #region [Private Methods]

        private void Save(bool isRemember)
        {
            if (isRemember)
            {
                EncryptionCookieHelper.Add(KId, Id.ToString(), 300);
                EncryptionCookieHelper.Add(KName, Name, 300);
            }
            else
            {
                EncryptionCookieHelper.Add(KId, Id.ToString());
                EncryptionCookieHelper.Add(KName, Name);
            }
        }

        #endregion
    }
}
