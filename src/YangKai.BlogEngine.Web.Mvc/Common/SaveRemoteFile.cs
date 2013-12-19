using System;
using System.IO;
using System.Web;
using AtomLab.Utility;
using YangKai.BlogEngine.Common;

namespace YangKai.BlogEngine.Web.Mvc.Areas.Admin.Common
{
    public class SaveRemoteFile
    {
        public static string SaveContentPic(string content, string folder)
        {
            try
            {
                folder = string.Format("{0}.{1}", DateTime.Now.ToString("yyyy.MM.dd"), folder);
                folder = Path.Combine(Config.Path.REMOTE_PICTURE_FOLDER, folder);
                var cdata = SaveRemoteFileHelper.SavePic(content, Config.Path.PHYSICAL_ROOT_PATH, folder);
                return cdata.Content;
            }
            catch (Exception)
            {
                return content;
            }
        }
    }
}