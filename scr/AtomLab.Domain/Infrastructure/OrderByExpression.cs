using System;
using System.Linq.Expressions;

namespace AtomLab.Domain.Infrastructure
{
    public class OrderByExpression<TEntity, TResult>
    {
        public Expression<Func<TEntity, TResult>> OrderByField { get; set; }
        public OrderMode OrdaerMode { get; set; }

        public OrderByExpression(Expression<Func<TEntity, TResult>> orderByField, OrderMode orderMode = OrderMode.ASC)
        {
            this.OrderByField = orderByField;
            this.OrdaerMode = orderMode;
        }
    }

    /// <summary>
    /// 排序方式
    /// </summary>
    public enum OrderMode
    {
        /// <summary>
        /// 正序
        /// </summary>
        ASC,

        /// <summary>
        /// 倒序
        /// </summary>
        DESC
    }
}