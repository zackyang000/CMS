using System;
using System.IO;
using System.Web;
using AtomLab.Utility;
using YangKai.BlogEngine.Common;

namespace YangKai.BlogEngine.Web.Mvc.Areas.Admin.Common
{
    public class SaveRemoteFile
    {
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