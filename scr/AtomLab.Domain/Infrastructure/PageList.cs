using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AtomLab.Domain.Infrastructure
{
    /// <summary>
    /// 分页数据集
    /// </summary>
    public class PageList<T>
    {
        /// <summary>
        /// 总数
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// 当前页数据集
        /// </summary>
        public List<T> DataList { get; set; }
    }
}