using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using AtomLab.Utility;

namespace YangKai.BlogEngine.Common
{
    public class SaveRemoteFile
    {
        /// <summary>
        /// 是否添加水印
        /// </summary>
        private static readonly bool IsAddWaterMark = false;
        /// <summary>
        /// 水印图片相对路径
        /// </summary>
        private static readonly string PicWaterMarkFile = string.Empty;
        /// <summary>
        /// 图片保存物理根路径
        /// </summary>
        private static readonly string FileSavePath = HttpContext.Current.Server.MapPath("/");
        /// <summary>
        /// 图片保存Web相对路径
        /// </summary>
        private static readonly string PicSaveWebPath = "/upload/offsite/";

        public static string SaveContentPic(string content, string fileHeaderName)
        {
            ContentData cdata = SaveRemoteFileHelper.SavePic(content, IsAddWaterMark, PicWaterMarkFile, FileSavePath, PicSaveWebPath, fileHeaderName);
            return cdata.Content;
        }
    }
}