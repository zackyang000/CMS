using System;
using AtomLab.Domain;

namespace YangKai.BlogEngine.Modules.PostModule.Objects
{
    /// <summary>
    /// 文章标签信息.
    /// </summary>
    public class Tag : Entity<Guid>
    {
        #region constructor

        public Tag()
            : base(Guid.NewGuid())
        {
            Name = string.Empty;
        }

        #endregion

        #region property

        /// <summary>
        /// 主键.
        /// </summary>
        public Guid TagId { get; private set; }

        /// <summary>
        /// 文章标签.
        /// </summary>
        public string Name { get; set; }

        #endregion

        #region relation schema

        /// <summary>
        /// 所属文章外键.
        /// </summary>
        public Guid PostId { get; set; }

        /// <summary>
        /// 所属文章.
        /// </summary>
        public virtual PostModule.Objects.Post Post { get; set; }

        #endregion

        public void Handle(EntityCreatedEvent<Comment> evnt)
        {
            var post = GetEntity<Post, Guid>(evnt.EntityCreated.PostId);
            post.ReplyCount++;
        }
    }
}