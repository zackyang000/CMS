using System;
using System.Collections.Generic;
using AtomLab.Core;

namespace YangKai.BlogEngine.Domain
{
    /// <summary>
    /// 分类信息.
    /// </summary>
    public class Photo : Entity
    {
        public Guid PhotoId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Path { get; set; }

        public virtual Gallery Gallery { get; set; } 
        public virtual List<PhotoComment> Comment { get; set; } 
    }
}