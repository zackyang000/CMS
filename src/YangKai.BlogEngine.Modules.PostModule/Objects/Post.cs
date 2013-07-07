using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using AtomLab.Domain;
using YangKai.BlogEngine.Modules.CommonModule.Objects;
using YangKai.BlogEngine.Modules.PostModule.Commands;

namespace YangKai.BlogEngine.Modules.PostModule.Objects
{
    /// <summary>
    /// 文章信息.
    /// </summary>
    public class Post : Entity<Guid>,
                        IEventHandler<EntityCreatingEvent<Post>>,
                        IEventHandler<EntityCreatedEvent<Comment>>,
                        IEntityEventHandler<PostDeleteEvent>,
                        IEntityEventHandler<PostRenewEvent>,
                        IEntityEventHandler<PostBrowseEvent>,
                        IEntityEventHandler<CommentDeleteEvent>,
                        IEntityEventHandler<CommentRenewEvent>
    {
        #region constructor

        public Post()
            : base(Guid.NewGuid())
        {
            Url = string.Empty;
            Title = string.Empty;
            Description = string.Empty;
            PostStatus = (int)PostStatusEnum.Publish;
            CommentStatus = (int)CommentStatusEnum.Open;
            Password = null;
            GradePoint = 5;
            CreateDate = DateTime.Now;
            PubDate = DateTime.Now;
            PubIp = string.Empty;
            PubAddress = string.Empty;
            EditDate = null;
            EditIp = null;
            EditAddress = null;
            PageCount = 1;
            ViewCount = 0;
            ReplyCount = 0;
            Categorys = new List<Category>();
            Tags = new List<Tag>();
        }

        #endregion

        #region property

        /// <summary>
        /// 主键.
        /// </summary>
        public Guid PostId { get; private set; }

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
        /// 文章的创建时间,即第一次保存时间.与发布日期可能不一致.
        /// </summary>
        public DateTime CreateDate { get; set; }

        #endregion

        #region relation schema

        /// <summary>
        /// 文章所属分类,至少包含1个分类.
        /// </summary>
        public virtual List<Category> Categorys { get; set; }

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
        /// 文章的转载信息,可能不存在.
        /// </summary>
        public virtual Source Source { get; set; }

        /// <summary>
        /// 文章的缩略图信息,可能不存在.
        /// </summary>
        public virtual Thumbnail Thumbnail { get; set; }

        /// <summary>
        /// 发布者信息外键.
        /// </summary>
        [ForeignKey("PubAdmin")]
        public Guid PubAdminId { get; set; }

        /// <summary>
        /// 发布者信息.
        /// </summary>
        public virtual User PubAdmin { get; set; }

        /// <summary>
        /// 最后编辑者信息外键,可能为空.
        /// </summary>
        [ForeignKey("EditAdmin")]
        public Guid? EditAdminId { get; set; }

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

        #endregion

        #region handler

        #region IEventHandler<EntityCreatingEvent<Post>> Members

        public void Handle(EntityCreatingEvent<Post> e)
        {
            var entity = e.EntityCreating;
            entity.Title = entity.Title.Trim();
            entity.Url = entity.Url.Trim().ToLower().Replace(" ", "-");
        }

        #endregion

        #region IEventHandler<EntityCreatedEvent<Comment>> Members

        public void Handle(EntityCreatedEvent<Comment> evnt)
        {
            var post = GetEntity<Post, Guid>(evnt.EntityCreated.PostId);
            post.ReplyCount++;
        }

        #endregion

        #region IEntityEventHandler<PostDeleteEvent> Members

        public object GetEntityId(PostDeleteEvent e)
        {
            return e.PostId;
        }

        public void Handle(PostDeleteEvent e)
        {
            this.PostStatus = (int)PostStatusEnum.Trash;
        }

        #endregion

        #region IEntityEventHandler<PostRenewEvent> Members

        public object GetEntityId(PostRenewEvent e)
        {
            return e.PostId;
        }

        public void Handle(PostRenewEvent e)
        {
            this.PostStatus = (int)PostStatusEnum.Publish;
        }

        #endregion

        #region IEntityEventHandler<PostBrowseEvent> Members

        public object GetEntityId(PostBrowseEvent e)
        {
            return e.PostId;
        }

        public void Handle(PostBrowseEvent e)
        {
            this.ViewCount++;
        }

        #endregion

        #region IEntityEventHandler<CommentDeleteEvent> Members

        public object GetEntityId(CommentDeleteEvent e)
        {
            var comment = GetEntity<Comment, Guid>(e.CommentId);
            return comment.PostId;
        }

        public void Handle(CommentDeleteEvent e)
        {
            this.ReplyCount--;
        }

        #endregion

        #region IEntityEventHandler<CommentRenewEvent> Members

        public object GetEntityId(CommentRenewEvent e)
        {
            var comment = GetEntity<Comment, Guid>(e.CommentId);
            return comment.PostId;
        }

        public void Handle(CommentRenewEvent e)
        {
            this.ReplyCount++;
        }

        #endregion

        #endregion
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