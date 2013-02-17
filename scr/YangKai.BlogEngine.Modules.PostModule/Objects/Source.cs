using System;
using AtomLab.Domain;

namespace YangKai.BlogEngine.Modules.PostModule.Objects
{
    /// <summary>
    /// 文章转载信息.
    /// </summary>
    public class Source : Entity<Guid>
    {
        #region constructor

        public Source()
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
        public Guid SourceId { get; private set; }

        /// <summary>
        /// 转载标题.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 转载URL.
        /// </summary>
        public string Url { get; set; }

        #endregion
    }
}