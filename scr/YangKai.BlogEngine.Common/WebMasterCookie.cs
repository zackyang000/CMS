using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
            string uid = Cookie.Load(WebMasterCookie.KId);
            Id = string.IsNullOrEmpty(uid) ? Guid.Empty : new Guid(uid);
            Name = Cookie.Load(WebMasterCookie.KName);
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
            Cookie.Remove(WebMasterCookie.KId);
            Cookie.Remove(WebMasterCookie.KName);
        }

        /// <summary>
        /// 移除cookies
        /// </summary>
        public void Clear()
        {
            Id = Guid.Empty;
            Name = string.Empty;
            Cookie.Remove(WebMasterCookie.KId);
            Cookie.Remove(WebMasterCookie.KName);
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
                Cookie.Add(WebMasterCookie.KId, Id.ToString(), 300);
                Cookie.Add(WebMasterCookie.KName, Name, 300);
            }
            else
            {
                Cookie.Add(WebMasterCookie.KId, Id.ToString());
                Cookie.Add(WebMasterCookie.KName, Name);
            }
        }

        #endregion
    }
}
