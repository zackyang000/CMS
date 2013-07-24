using System;
using System.Collections.Generic;
using AtomLab.Core;

namespace YangKai.BlogEngine.Domain
{
    /// <summary>
    /// 分类信息.
    /// </summary>
    public class Category : Entity
    {
        /// <summary>
        /// 主键.
        /// </summary>
        public Guid CategoryId { get; set; }

        /// <summary>
        /// 分类名称.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 用于显示分类信息的URL地址.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 分类描述.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 所包含的文章列表.
        /// </summary>
        public List<Post> Posts { get; set; }

        /// <summary>
        /// 所属分组信息.
        /// </summary>
        public Group Group { get; set; }
    }
}