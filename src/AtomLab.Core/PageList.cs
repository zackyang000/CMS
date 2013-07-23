using System.Collections.Generic;

namespace AtomLab.Core
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
            get { return TotalCount / PageSize + (TotalCount % PageSize == 0 ? 0 : 1); }
        }

        /// <summary>
        /// 当前页数据集
        /// </summary>
        public List<T> DataList { get; set; }
    }
}