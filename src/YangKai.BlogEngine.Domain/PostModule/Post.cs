using System;
using System.Collections.Generic;
using AtomLab.Core;

namespace YangKai.BlogEngine.Domain
{
    /// <summary>
    /// 文章信息.
    /// </summary>
    public class Post : Entity
    {
        public Post()
        {
            PostStatus = (int)PostStatusEnum.Publish;
            CommentStatus = (int)CommentStatusEnum.Open;
            GradePoint = 5;
            PageCount = 1;
        }

        /// <summary>
        /// 主键.
        /// </summary>
        public Guid PostId { get; set; }

        /// <summary>
        /// 用于http访问的固定URL地址,若为空则使用PostId访问.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 文章标题.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 文章正文.
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 文章摘要.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 作者评级分数.
        /// 仅允许为0~10的整数.(默认为5)
        /// </summary>
        public int GradePoint { get; set; }

        /// <summary>
        /// 文章当前状态值.
        /// </summary>
        public int PostStatus { get; set; }

        /// <summary>
        /// 文章当前评论状态值.
        /// </summary>
        public int CommentStatus { get; set; }

        /// <summary>
        /// 文章密码.
        /// 可为文章设定一个密码,凭这个密码才能对文章进行编辑.
        /// 为空或null表示不需要密码.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 文章的发布日期.发布日期大于当前日期的文章将不予显示.
        /// </summary>
        public DateTime PubDate { get; set; }

        /// <summary>
        /// 文章发布者IP地址.
        /// </summary>
        public string PubIp { get; set; }

        /// <summary>
        /// 文章发布者地理位置.
        /// </summary>
        public string PubAddress { get; set; }

        /// <summary>
        /// 文章的最后一次编辑时间.
        /// </summary>
        public DateTime? EditDate { get; set; }

        /// <summary>
        /// 文章最后一次编辑者IP地址.
        /// </summary>
        public string EditIp { get; set; }

        /// <summary>
        /// 文章最后一次编辑者地理位置.
        /// </summary>
        public string EditAddress { get; set; }

        /// <summary>
        /// 文章页数.默认为1,即不分页.
        /// </summary>
        public int PageCount { get; set; }

        /// <summary>
        /// 文章的浏览次数.
        /// </summary>
        public int ViewCount { get; set; }

        /// <summary>
        /// 文章的评论次数.
        /// </summary>
        public int ReplyCount { get; set; }

        /// <summary>
        /// 文章所属标签,包含0,1或多个标签.
        /// </summary>
        public virtual List<Tag> Tags { get; set; }

        /// <summary>
        /// 文章所有评论.
        /// </summary>
        public virtual List<Comment> Comments { get; set; }

        /// <summary>
        /// 文章的二维码信息,可能不存在.
        /// </summary>
        public virtual QrCode QrCode { get; set; }

        /// <summary>
        /// 文章转载信息,可能不存在.
        /// </summary>
        public virtual Source Source { get; set; }

        /// <summary>
        /// 文章缩略图,可能不存在.
        /// </summary>
        public string Thumbnail { get; set; }

        /// <summary>
        /// 发布者信息.
        /// </summary>
        public virtual User PubAdmin { get; set; }

        /// <summary>
        /// 最后编辑者信息,可能为空.
        /// </summary>
        public virtual User EditAdmin { get; set; }

        /// <summary>
        /// 文章分组信息外键.
        /// </summary>
        public Guid GroupId { get; set; }

        /// <summary>
        /// 文章分组信息.
        /// </summary>
        public virtual Group Group { get; set; }
    }

    /// <summary>
    /// 文章当前状态
    /// </summary>
    public enum PostStatusEnum
    {
        /// <summary>
        /// 已发表
        /// </summary>
        Publish = 1,

        /// <summary>
        /// 待审核
        /// </summary>
        Pending = 2,

        /// <summary>
        /// 草稿
        /// </summary>
        Draft = 4,

        /// <summary>
        /// 私人内容(不会被公开)
        /// </summary>
        Private = 8,

        /// <summary>
        /// 已删除(回收站)
        /// </summary>
        Trash = 16,
    }

    /// <summary>
    /// 文章当前评论状态
    /// </summary>
    public enum CommentStatusEnum
    {
        /// <summary>
        /// 允许添加评论
        /// </summary>
        Open = 1,

        /// <summary>
        /// 不允许添加评论
        /// </summary>
        Closed = 2,

        /// <summary>
        /// 评论不可见并不允许添加评论
        /// </summary>
        NonVisible = 4,
    }
}