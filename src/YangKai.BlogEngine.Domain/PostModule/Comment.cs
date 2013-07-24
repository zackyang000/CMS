using System;
using AtomLab.Core;

namespace YangKai.BlogEngine.Domain
{
    /// <summary>
    /// 评论信息.
    /// </summary>
    public class Comment : Entity
    {
        /// <summary>
        /// 主键.
        /// </summary>
        public Guid CommentId { get; set; }

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

        /// <summary>
        /// 发布者头像.
        /// </summary>
        public string Pic { get; set; }

        /// <summary>
        /// 发布者发布消息方式.
        /// </summary>
        public string PublicMode { get; set; }

        /// <summary>
        /// 是否是管理员.
        /// </summary>
        public bool IsAdmin { get; set; }

        /// <summary>
        /// 父评论Id.若为null则为顶级评论.
        /// </summary>
        public Guid? ParentId { get; set; }

        /// <summary>
        /// 所属文章外键.
        /// </summary>
        public Guid PostId { get; set; }

        /// <summary>
        /// 所属文章.
        /// </summary>
        public virtual Post Post { get; set; }
    }
}
