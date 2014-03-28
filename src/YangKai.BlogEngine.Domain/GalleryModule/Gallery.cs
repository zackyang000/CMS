using System;
using System.Collections.Generic;
using AtomLab.Core;

namespace YangKai.BlogEngine.Domain
{
    /// <summary>
    /// 分类信息.
    /// </summary>
    public class Gallery : Entity
    {
        public Guid GalleryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Cover { get; set; }
        public bool IsHidden { get; set; }

        public virtual List<Photo> Photos { get; set; } 
    }
}