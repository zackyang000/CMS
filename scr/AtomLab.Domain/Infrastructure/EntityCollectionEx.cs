//===========================================================
// Copyright @ 2010 YangKai. All Rights Reserved.
// Framework: 4.0
// Author: 杨凯
// Email: yangkai-13896222@sohu.com
// QQ: 83448327
// CreateTime: 8/1/2012 10:19:16 PM
//===========================================================

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Objects.DataClasses;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace AtomLab.Domain.Infrastructure
{
    public abstract class EntityCollectionEx<TEntity, TEntityId> : EntityCollection<TEntity, TEntityId>
        where TEntity : class, IEntity<TEntityId>
    {
        protected EntityCollectionEx(IUnitOfWork context)
            : base(context)
        {
        }

        #region Find method

        /// <summary>
        /// 查询实体,若不存在则返回默认实体
        /// </summary>
        /// <param name="key">主键</param>
        /// <returns></returns>
        protected virtual TEntity Find<TEntity>(TEntityId key)
            where TEntity : class
        {
            return _context.Set<TEntity>().Find(key);
        }

        /// <summary>
        /// 查询实体,若不存在则返回默认实体
        /// </summary>
        /// <param name="specExpr">查询条件</param>
        /// <returns></returns>
        protected virtual TEntity Find<TEntity>(Expression<Func<TEntity, bool>> specExpr)
            where TEntity : class
        {
            return _context.Set<TEntity>().Where(specExpr).FirstOrDefault();
        }

        #endregion

        #region FindAll method

        /// <summary>
        /// 查询实体列表
        /// </summary>
        /// <param name="specExpr">查询条件</param>
        /// <returns></returns>
        protected virtual IQueryable<TEntity> FindAll<TEntity>(Expression<Func<TEntity, bool>> specExpr = null)
            where TEntity : class
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();

            if (specExpr != null)
                query = query.Where(specExpr);

            return query;
        }

        /// <summary>
        /// 查询实体列表
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="specExpr">查询条件</param>
        /// <param name="orderByExpression">排序条件</param>
        /// <returns></returns>
        protected virtual IQueryable<TEntity> FindAll<TEntity, TResult>(
            Expression<Func<TEntity, bool>> specExpr = null,
            OrderByExpression<TEntity, TResult> orderByExpression = null)
            where TEntity : class
        {
            var query = FindAll<TEntity>();

            if (specExpr != null)
                query = query.Where(specExpr);

            if (orderByExpression != null)
                query = orderByExpression.OrdaerMode == OrderMode.ASC
                            ? query.OrderBy(orderByExpression.OrderByField)
                            : query.OrderByDescending(orderByExpression.OrderByField);

            return query;
        }

        /// <summary>
        /// 查询实体列表
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="topNum">实体数量</param>
        /// <param name="specExpr">查询条件</param>
        /// <param name="orderByExpression">排序条件</param>
        /// <returns></returns>
        protected virtual IQueryable<TEntity> FindAll<TEntity, TResult>(int topNum,
                                                                        Expression<Func<TEntity, bool>> specExpr = null,
                                                                        OrderByExpression<TEntity, TResult>
                                                                            orderByExpression = null)
            where TEntity : class
        {
            return FindAll(specExpr, orderByExpression).Take(topNum);
        }

        /// <summary>
        /// 查询实体列表
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="orderByExpression">排序条件</param>
        /// <returns></returns>
        protected virtual IQueryable<TEntity> FindAll<TEntity, TResult>(
            OrderByExpression<TEntity, TResult> orderByExpression = null) where TEntity : class
        {
            return FindAll(null, orderByExpression);
        }

        /// <summary>
        /// 查询实体列表
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="topNum">实体数量</param>
        /// <param name="orderByExpression">排序条件</param>
        /// <returns></returns>
        protected virtual IQueryable<TEntity> FindAll<TEntity, TResult>(int topNum,
                                                                        OrderByExpression<TEntity, TResult>
                                                                            orderByExpression)
            where TEntity : class
        {
            return FindAll(null, orderByExpression).Take(topNum);
        }

        /// <summary>
        /// 查询实体列表
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页数量</param>
        /// <param name="specExpr">查询条件</param>
        /// <param name="orderByExpression">排序条件</param>
        /// <returns></returns>
        protected virtual PageList<TEntity> FindAll<TEntity, TResult>(int pageIndex, int pageSize,
                                                                      Expression<Func<TEntity, bool>> specExpr = null,
                                                                      OrderByExpression<TEntity, TResult>
                                                                          orderByExpression
                                                                          = null)
            where TEntity : class
        {
            var query = FindAll(specExpr, orderByExpression);

            return new PageList<TEntity>
                       {
                           TotalCount = query.Count(),
                           DataList = query.Skip((pageIndex - 1)*pageSize).Take(pageSize).ToList()
                       };
        }

        #endregion

        #region Count method

        /// <summary>
        /// 根据条件查询实体数量
        /// </summary>
        /// <param name="specExpr">查询条件</param>
        /// <returns></returns>
        protected virtual int Count<TEntity>(Expression<Func<TEntity, bool>> specExpr = null)
            where TEntity : class
        {
            return FindAll(specExpr).Count();
        }

        #endregion

        #region Exist method

        /// <summary>
        /// 判断实体是否存在
        /// </summary>
        /// <param name="specExpr">查询条件</param>
        /// <returns></returns>
        protected virtual bool Exist<TEntity>(Expression<Func<TEntity, bool>> specExpr = null)
            where TEntity : class
        {
            return Count(specExpr) > 0;
        }

        #endregion
    }
}