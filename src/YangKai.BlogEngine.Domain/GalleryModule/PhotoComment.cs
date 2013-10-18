using System;
using AtomLab.Core;

namespace YangKai.BlogEngine.Domain
{
    public class PhotoComment : Entity
    {
        public Guid PhotoCommentId { get; set; }

        /// <summary>
        /// 评论内容.
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 发布者.
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// 发布者电子邮件.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 发布者Ip地址.
        /// </summary>
        public string Ip { get; set; }

        /// <summary>
        /// 发布者物理位置.
        /// </summary>
        public string Address { get; set; }

        public virtual Photo Photo { get; set; }
    }
}