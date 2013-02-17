using System;
using AtomLab.Domain;

namespace YangKai.BlogEngine.Modules.PostModule.Objects
{
    /// <summary>
    /// 文章缩略图信息.
    /// </summary>
    public class Thumbnail : Entity<Guid>
    {
        #region constructor

        public Thumbnail()
            : base(Guid.NewGuid())
        {
            Title = string.Empty;
            Url = string.Empty;
        }

        #endregion

        #region property

        /// <summary>
        /// 主键.
        /// </summary>
        public Guid ThumbnailId { get; private set; }

        /// <summary>
        /// 缩略图标题.主要用于a标签的title属性.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 缩略图文件名.
        /// </summary>
        public string Url { get; set; }

        #endregion
    }
}