using System;
using AtomLab.Core;

namespace YangKai.BlogEngine.Domain
{
    /// <summary>
    /// 文章转载信息.
    /// </summary>
    public class Source : Entity
    {
        /// <summary>
        /// 主键.
        /// </summary>
        public Guid SourceId { get; set; }

        /// <summary>
        /// 转载标题.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 转载URL.
        /// </summary>
        public string Url { get; set; }
    }
}