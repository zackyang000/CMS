using System;
using AtomLab.Domain;
using YangKai.BlogEngine.Modules.BoardModule.Events;

namespace YangKai.BlogEngine.Modules.BoardModule.Objects
{
    /// <summary>
    /// 留言信息.
    /// </summary>
    public class Board : Entity<Guid>,
                                IEventHandler<EntityCreatingEvent<Board>>,
                                IEntityEventHandler<BoardDeleteEvent>,
                                IEntityEventHandler<BoardRenewEvent>
    {
        #region constructor

        public Board()
            : base(Guid.NewGuid())
        {
            Content = string.Empty;
            Author = string.Empty;
            Ip = string.Empty;
            Address = string.Empty;
            CreateDate = DateTime.Now;
            IsDeleted = false;
        }

        #endregion

        #region property

        /// <summary>
        /// 主键.
        /// </summary>
        public Guid BoardId { get; private set; }

        /// <summary>
        /// 留言内容.
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 发布者名称.
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// 发布者Ip地址.
        /// </summary>
        public string Ip { get; set; }

        /// <summary>
        /// 发布者物理位置.
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 留言创建时间.
        /// </summary>
        public DateTime CreateDate { get; private set; }

        /// <summary>
        /// 是否被删除.
        /// </summary>
        public bool IsDeleted { get; set; }

        #endregion

        #region handler

        #region IEventHandler<EntityCreatingEvent<Board>> Members

        public void Handle(EntityCreatingEvent<Board> e)
        {
            CreatingCheck(e.EntityCreating);
            CreatingFix(e.EntityCreating);
        }

        private void CreatingCheck(Board entity)
        {
            const int msgContentMinLen = 0;
            const int msgContentMaxLen = 300;
            const string msgGuestNameRequiredTip = "请输入昵称.";
            const string msgContentMinLenTip = "请输入留言内容.";
            const string msgContentMaxLenTip = "留言内容最大允许300个字符,该提交超过系统限制.";

            if (!entity.Author.HasValue())
            {
                throw new DomainException(msgGuestNameRequiredTip);
            }
            if (!entity.Content.HasValue())
            {
                throw new DomainException(msgContentMinLenTip);
            }
            if (!entity.Content.LengthValidate(msgContentMinLen, msgContentMaxLen))
            {
                throw new DomainException(msgContentMaxLenTip);
            }
        }

        private void CreatingFix(Board entity)
        {
            entity.Content = entity.Content.Replace("\n", "<br />");
        }

        #endregion

        #region IEntityEventHandler<BoardDeleteCommand> Members

        public object GetEntityId(BoardDeleteEvent e)
        {
            return e.BoardId;
        }

        public void Handle(BoardDeleteEvent e)
        {
            IsDeleted = true;
        }

        #endregion

        #region IEntityEventHandler<BoardRenewCommand> Members

        public object GetEntityId(BoardRenewEvent e)
        {
            return e.BoardId;
        }

        public void Handle(BoardRenewEvent e)
        {
            IsDeleted = false;
        }

        #endregion

        #endregion
    }
}
