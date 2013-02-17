using System;
using AtomLab.Domain;

namespace YangKai.BlogEngine.Modules.PostModule.Objects
{
    /// <summary>
    /// 文章分页信息.
    /// </summary>
    public class Page : Entity<Guid>
    {
        #region constructor

        public Page()
            : base(Guid.NewGuid())
        {
            Title = string.Empty;
            Content = string.Empty;
            OrderId = 1;
        }

        #endregion

        #region property

        /// <summary>
        /// 主键.
        /// </summary>
        public Guid PageId { get; private set; }

        /// <summary>
        /// 分特标题.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 分页内容.
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 该页排列顺序,默认为1.
        /// </summary>
        public int OrderId { get; set; }

        #endregion

        #region handler

        #endregion
    }
}