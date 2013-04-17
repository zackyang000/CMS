using System;
using System.IO;
using System.Web;
using AtomLab.Utility;

namespace YangKai.BlogEngine.Common
{
    public class SaveRemoteFile
    {
        /// <summary>
        /// 水印图片路径
        /// </summary>
        private static readonly string PicWaterMarkFile = HttpContext.Current.Server.MapPath("/water.png");

        public static string SaveContentPic(string content, string title)
        {
            var folder = string.Format("{0}.{1}", DateTime.Now.ToString("yyyy.MM.dd"), title);
            var root = Config.Path.REMOTE_PICTURE_FOLDER;
            if (root.Substring(0, 1) == "/") root = root.Substring(1);
            folder = Path.Combine(root, folder);
            var cdata = SaveRemoteFileHelper.SavePic(content, Config.Path.PHYSICAL_ROOT_PATH, folder);
            return cdata.Content;
        }
    }
}