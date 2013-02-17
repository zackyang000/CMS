//===========================================================
// Copyright @ 2010 YangKai. All Rights Reserved.
// Framework: 4.0
// Author: 杨凯
// Email: yangkai-13896222@sohu.com
// QQ: 83448327
// CreateTime: 2/1/2011 10:26:30 PM
// 用法:ContentData cdata = SaveRemoteFileHelper.SavePic(content, IsAddWaterMark, PicWaterMarkFile, FileSavePath, PicSaveWebPath, fileHeaderName)
//===========================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Text.RegularExpressions;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Net;
using System.Xml;
using System.Web;
using System.Configuration;

namespace AtomLab.Utility
{
    public class ContentData
    {
        /// <summary>
        /// 处理后的内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 内容包含图片列表
        /// </summary>
        public List<string> FileList { get; set; }
    }

    public class SaveRemoteFileHelper
    {
        /// <summary>
        /// 保存图片.
        /// </summary>
        /// <param name="htmlContent">HTML内容</param>
        /// <param name="autoMark">是否添加水印</param>
        /// <param name="picWaterMarkFile">水印图片相对路径</param>
        /// <param name="fileSavePath">图片保存物理根路径</param>
        /// <param name="picSaveWebPath">图片保存Web相对路径</param>
        /// <param name="fileHeaderName">文件名前缀</param>
        /// <returns></returns>
        public static ContentData SavePic(string htmlContent, bool autoMark, 
            string picWaterMarkFile, string fileSavePath, string picSaveWebPath, string fileHeaderName)
        {
            ContentData cData = new ContentData();
            cData.Content = string.Empty;
            cData.FileList = new List<string>();

            string FileSubPath = string.Format("{0}.{1}.{2}.{3}/", DateTime.Now.ToString("yyyy"), DateTime.Now.ToString("MM"), DateTime.Now.ToString("dd"), fileHeaderName);
            picSaveWebPath += FileSubPath;
            string SaveFullPath = fileSavePath + picSaveWebPath.Replace("/", "\\");

            WebClient client = new WebClient();
            MatchCollection matchs = new Regex(@"((http|https|ftp|rtsp|mms):(\/\/|\\\\){1}([\w\-\//]+[.]){1,}([\w]{1,3})(([^.])+[.]{1}(gif|jpg|jpeg|jpe|bmp|png)))", RegexOptions.IgnoreCase).Matches(htmlContent);

            if (matchs.Count > 0)
            {
                if (!Directory.Exists(SaveFullPath))
                {
                    Directory.CreateDirectory(SaveFullPath);
                }
            }

            //准备处理要下载的文件
            int num = 0;
            foreach (Match match in matchs)
            {
                num++;
                string fileurl = match.Groups[1].Value;//.ToLower();

                if (fileurl.Contains("woshinidezhu.com")) { continue; }

                string fileextname = Path.GetExtension(fileurl);//.ToLower();

                string newfilename = Guid.NewGuid().ToString() + fileextname;

                if (autoMark)
                {
                    string tfilename = Guid.NewGuid().ToString() + "t" + fileextname;
                    //下载文件
                    client.DownloadFile(fileurl, SaveFullPath + tfilename);
                    ImageHelper.CreateWeaterPicture(SaveFullPath + tfilename, SaveFullPath + newfilename, HttpContext.Current.Server.MapPath(picWaterMarkFile), fileextname);
                }
                else
                {
                    //下载文件
                    client.DownloadFile(fileurl, SaveFullPath + newfilename);
                }

                htmlContent = htmlContent.Replace(fileurl,  picSaveWebPath + newfilename);

                //添加到集合
                cData.FileList.Add( picSaveWebPath + newfilename);
            }
            client.Dispose();

            cData.Content = htmlContent;

            return cData;
        }

        /// <summary>
        /// 检查是否为合法的上传文件
        /// </summary>
        /// <param name="_fileExt"></param>
        /// <returns></returns>
        private static bool CheckFileExt(string _fileExt)
        {
            string[] allowExt = new string[] { ".gif", ".jpg", ".jpeg", ".png", ".swf" };
            for (int i = 0; i < allowExt.Length; i++)
            {
                if (allowExt[i] == _fileExt) { return true; }
            }
            return false;

        }
    }
}
