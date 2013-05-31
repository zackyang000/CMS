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
        public PageList(int pageSize)
        {
            PageSize = pageSize;
        }

        /// <summary>
        /// 总条数
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// 每页条数
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 总页数
        /// </summary>
        public int PageCount
        {
            get { return TotalCount/PageSize + 1; }
        }

        /// <summary>
        /// 当前页数据集
        /// </summary>
        public IList<T> DataList { get; set; }
    }
}