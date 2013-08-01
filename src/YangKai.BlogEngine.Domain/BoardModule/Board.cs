using System;
using AtomLab.Core;

namespace YangKai.BlogEngine.Domain
{
    /// <summary>
    /// 留言信息.
    /// </summary>
    public class Board : Entity
    {
        #region property

        /// <summary>
        /// 主键.
        /// </summary>
        public Guid BoardId { get; set; }

        /// <summary>
        /// 留言内容.
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 发布者名称.
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// 发布者电子邮件.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 发布者站点.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 发布者Ip地址.
        /// </summary>
        public string Ip { get; set; }

        /// <summary>
        /// 发布者物理位置.
        /// </summary>
        public string Address { get; set; }

        #endregion
    }
}
