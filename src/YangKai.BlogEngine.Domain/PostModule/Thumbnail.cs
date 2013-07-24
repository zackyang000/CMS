using System;
using AtomLab.Core;

namespace YangKai.BlogEngine.Domain
{
    /// <summary>
    /// 文章缩略图信息.
    /// </summary>
    public class Thumbnail : Entity
    {
        /// <summary>
        /// 主键.
        /// </summary>
        public Guid ThumbnailId { get; set; }

        /// <summary>
        /// 缩略图标题.主要用于a标签的title属性.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 缩略图文件名.
        /// </summary>
        public string Url { get; set; }
    }
}