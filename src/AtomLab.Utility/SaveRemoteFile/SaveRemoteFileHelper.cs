using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.IO;
using System.Net;

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
        public const string IMG_REGEX =
            @"((http|https|ftp|rtsp|mms):(\/\/|\\\\){1}([\w\-\//]+[.]){1,}([\w]{1,3})(([^.])+[.]{1}(gif|jpg|jpeg|jpe|bmp|png)))";

        /// <summary>
        /// 保存图片.
        /// </summary>
        /// <param name="htmlContent">HTML内容</param>
        /// <param name="picWaterMarkFile">水印图片相对路径</param>
        /// <param name="physicalRootPath">图片保存物理根路径</param>
        /// <param name="folder">文件夹名</param>
        /// <returns></returns>
        public static ContentData SavePic(string htmlContent, string physicalRootPath, 
                                          string folder, string picWaterMarkFile)
        {
            var cData = new ContentData
                {
                    Content = string.Empty,
                    FileList = new List<string>()
                };
            var physicalPath = Path.Combine(physicalRootPath, folder);

            var client = new WebClient();

            //使用浏览器代理配置
            client.Proxy = WebRequest.GetSystemWebProxy();
            client.Proxy.Credentials = CredentialCache.DefaultCredentials;

            var matchs = new Regex(IMG_REGEX, RegexOptions.IgnoreCase).Matches(htmlContent);

            if (matchs.Count > 0)
            {
                if (!Directory.Exists(physicalPath))
                {
                    Directory.CreateDirectory(physicalPath);
                }
            }

            foreach (Match match in matchs)
            {
                var remoteUrl = match.Groups[1].Value;
                if (remoteUrl.Contains("woshinidezhu.com")) continue;

                var extension = Path.GetExtension(remoteUrl);
                var filename = Guid.NewGuid() + extension;
                var physicalAddress = Path.Combine(physicalPath, filename);
                var url = Path.Combine(folder, filename);
                client.DownloadFile(remoteUrl, physicalAddress);
                if (!string.IsNullOrEmpty(picWaterMarkFile))
                {
                    ImageHelper.CreateWeaterPicture(physicalAddress, physicalAddress, picWaterMarkFile, extension);
                }
                htmlContent = htmlContent.Replace(remoteUrl, url);

                cData.FileList.Add(url);
            }
            client.Dispose();
            cData.Content = htmlContent;
            return cData;
        }

        /// <summary>
        /// 保存图片.
        /// </summary>
        /// <param name="htmlContent">HTML内容</param>
        /// <param name="physicalRootPath">图片保存物理根路径</param>
        /// <param name="folder">文件夹名</param>
        /// <returns></returns>
        public static ContentData SavePic(string htmlContent, string physicalRootPath, string folder)
        {
            return SavePic(htmlContent, physicalRootPath, folder, string.Empty);
        }
    }
}