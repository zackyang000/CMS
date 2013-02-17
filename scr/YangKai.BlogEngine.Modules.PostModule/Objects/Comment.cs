using System;
using AtomLab.Domain;
using YangKai.BlogEngine.Modules.PostModule.Commands;

namespace YangKai.BlogEngine.Modules.PostModule.Objects
{
    /// <summary>
    /// 评论信息.
    /// </summary>
    public class Comment : Entity<Guid>,
                           IEventHandler<EntityCreatingEvent<Comment>>,
                           IEntityEventHandler<CommentDeleteEvent>,
                           IEntityEventHandler<CommentRenewEvent>
    {
        #region constructor

        public Comment()
            : base(Guid.NewGuid())
        {
            Content = string.Empty;
            Author = string.Empty;
            Email = string.Empty;
            Url = string.Empty;
            Ip = string.Empty;
            Address = string.Empty;
            Pic = string.Empty;
            PublicMode = string.Empty;
            IsAdmin = false;
            CreateDate = DateTime.Now;
            IsDeleted = false;
            ParentId = null;
        }

        #endregion

        #region property

        /// <summary>
        /// 主键.
        /// </summary>
        public Guid CommentId { get; private set; }

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
        /// 评论创建时间.
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// 是否已被删除.
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// 父评论Id.若为null则为顶级评论.
        /// </summary>
        public Guid? ParentId { get; set; }

        #endregion

        #region relation schema

        /// <summary>
        /// 所属文章外键.
        /// </summary>
        public Guid PostId { get; set; }

        /// <summary>
        /// 所属文章.
        /// </summary>
        public virtual Post Post { get; set; }

        #endregion

        #region handler



        #region IEventHandler<EntityCreatingEvent<Comment>> Members

        public void Handle(EntityCreatingEvent<Comment> e)
        {
            CreatingCheck(e.EntityCreating);
            CreatingFix(e.EntityCreating);
        }

        private void CreatingCheck(Comment entity)
        {
            const int commentContentMin = 0;
            const int commentContentMax = 300;
            const string msgGuestNameRequired = "请输入昵称.";
            const string msgCommentContentMin = "请输入评论内容.";
            const string msgCommentContentMax = "评论内容最大允许300个字符,该提交超过系统限制.";

            if (!entity.Author.HasValue())
            {
                throw new DomainException(msgGuestNameRequired);
            }
            if (!entity.Content.HasValue())
            {
                throw new DomainException(msgCommentContentMin);
            }
            if (!entity.Content.LengthValidate(commentContentMin, commentContentMax))
            {
                throw new DomainException(msgCommentContentMax);
            }
        }

        private void CreatingFix(Comment entity)
        {
            entity.Content = entity.Content.Replace("\n", "<br />");
            entity.Url = entity.Url ?? string.Empty;
            entity.Url = entity.Url.ToLower();
            entity.Email = entity.Email ?? string.Empty;
            entity.Email = entity.Email.ToLower();
            var hasHead = entity.Url.Contains("http://")
                          || entity.Url.Contains("https://");
            if (entity.Url.HasValue() && !hasHead)
            {
                entity.Url = string.Format("http://{0}", entity.Url);
            }
        }

        #endregion

        #region IEntityEventHandler<CommentDeleteEvent> Members

        public object GetEntityId(CommentDeleteEvent e)
        {
            return e.CommentId;
        }

        public void Handle(CommentDeleteEvent e)
        {
            this.IsDeleted = true;
        }

        #endregion

        #region IEntityEventHandler<CommentRenewEvent> Members

        public object GetEntityId(CommentRenewEvent e)
        {
            return e.CommentId;
        }

        public void Handle(CommentRenewEvent e)
        {
            this.IsDeleted = false;
        }

        #endregion

        #endregion
    }
}
