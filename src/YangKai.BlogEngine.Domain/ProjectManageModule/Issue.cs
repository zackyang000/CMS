using System;
using System.Collections.Generic;
using AtomLab.Core;

namespace YangKai.BlogEngine.Domain
{
    /// <summary>
    /// 分类信息.
    /// </summary>
    public class Issue : Entity
    {
        /// <summary>
        /// 主键.
        /// </summary>
        public Guid IssueId { get; set; }

        public string Project { get; set; }
        public string Author { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Result { get; set; }
        public string Statu { get; set; }
    }
}