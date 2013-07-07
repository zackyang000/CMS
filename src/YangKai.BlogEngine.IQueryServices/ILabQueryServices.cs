//===========================================================
// Copyright @ 2010 YangKai. All Rights Reserved.
// Framework: 4.0
// Author: 杨凯
// Email: yangkai-13896222@sohu.com
// QQ: 83448327
// CreateTime: 7/25/2012 11:37:26 PM
//===========================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YangKai.BlogEngine.Modules.PostModule.Objects;

namespace YangKai.BlogEngine.IQueryServices
{
    public interface ILabQueryServices
    {
        /// <summary>
        /// 读取累计访问人数
        /// </summary>
        int TotalVisitorCount();

        /// <summary>
        /// 读取今日访问人数
        /// </summary>
        int TodayVisitorCount();

        /// <summary>
        /// 得到统计信息
        /// </summary>
        /// <returns></returns>
        IDictionary<string, string> GetRefstatInfo();

        /// <summary>
        /// 更新访问统计图
        /// </summary>
        void UpdateRefStatPicture();

        /// <summary>
        /// 导出文章为excel
        /// </summary>
        /// <param name="filePath"></param>
        void ExportPosts(string filePath,IList<Post> data);
    }
}
