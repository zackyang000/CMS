using System;
using System.Collections.Generic;
using AtomLab.Core;

namespace YangKai.BlogEngine.Domain
{
    /// <summary>
    /// 分组信息.
    /// </summary>
    public class Group : Entity
    {
        /// <summary>
        /// 主键.
        /// </summary>
        public Guid GroupId { get; set; }

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
        /// 分组所有分类信息列表.
        /// </summary>
        public virtual List<Category> Categorys { get; set; }

        /// <summary>
        /// 分组所有文章信息列表.
        /// </summary>
        public virtual List<Post> Posts { get; set; }

        /// <summary>
        /// 分组所属频道信息.
        /// </summary>
        public virtual Channel Channel { get; set; }
    }
}