using System;
using AtomLab.Core;

namespace YangKai.BlogEngine.Domain
{
    /// <summary>
    /// 文章标签信息.
    /// </summary>
    public class Tag : Entity
    {
        /// <summary>
        /// 主键.
        /// </summary>
        public Guid TagId { get; set; }

        /// <summary>
        /// 文章标签.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 所属文章.
        /// </summary>
        public virtual Post Post { get; set; }
    }
}