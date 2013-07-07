using System;
using System.Collections.Generic;
using AtomLab.Domain;

namespace YangKai.BlogEngine.Modules.PostModule.Objects
{
    /// <summary>
    /// 分组信息.
    /// </summary>
    public class Group : Entity<Guid>
    {
        #region constructor

        public Group()
            : base(Guid.NewGuid())
        {
            Name = string.Empty;
            Url = string.Empty;
            OrderId = 255;
            CreateDate = DateTime.Now;
            IsDeleted = false;
        }

        #endregion

        #region property

        /// <summary>
        /// 主键.
        /// </summary>
        public Guid GroupId { get; private set; }

        /// <summary>
        /// 分组名称.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 用于显示分组信息的URL地址.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 分组描述.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 分组排列顺序,默认为255.
        /// </summary>
        public int OrderId { get; set; }

        /// <summary>
        /// 分组创建时间.
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// 是否已被删除.(禁用)
        /// </summary>
        public bool IsDeleted { get; set; }

        #endregion

        #region relation schema

        /// <summary>
        /// 分组所有分类信息列表.
        /// </summary>
        public virtual List<Category> Categorys { get; set; }

        /// <summary>
        /// 分组所有文章信息列表.
        /// </summary>
        public virtual List<Post> Posts { get; set; }

        /// <summary>
        /// 分组所属频道信息外键.
        /// </summary>
        public Guid ChannelId { get; set; }

        /// <summary>
        /// 分组所属频道信息.
        /// </summary>
        public virtual Channel Channel { get; set; }

        #endregion
    }
}