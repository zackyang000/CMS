using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace AtomLab.Core
{
    public class Repository<TEntity, TEntityId>
        where TEntity : class
    {
        protected readonly DbContext _context;

        public Repository(DbContext context)
        {
            _context = context;
        }

        public TEntity Add(TEntity T)
        {
            _context.Set<TEntity>().Add(T);
            _context.SaveChanges();
            return T;
        }

        public void Add(IList<TEntity> list)
        {
            foreach (var entity in list)
            {
                _context.Set<TEntity>().Add(entity);
            }
            _context.SaveChanges();
        }

        public TEntity Update(TEntity T)
        {
            _context.SaveChanges();
           return T;
        }

        public void Update(IList<TEntity> list)
        {
            _context.SaveChanges();
        }

        public TEntity Remove(TEntity T)
        {
            if (T == null)
            {
                return null;
            }
            _context.Set<TEntity>().Remove(T);
           _context.SaveChanges();
            return T;
        }

        public void Remove(IList<TEntity> list)
        {
            while (list.Count>0)
            {
                _context.Set<TEntity>().Remove(list[0]);
            }
            _context.SaveChanges();
        }

        #region Get method

        /// <summary>
        /// 查询实体,若不存在则返回默认实体
        /// </summary>
        /// <param name="key">主键</param>
        public virtual TEntity Get(TEntityId key)
        {
            return _context.Set<TEntity>().Find(key);
        }

        /// <summary>
        /// 查询实体,若不存在则返回默认实体
        /// </summary>
        /// <param name="specExpr">查询条件</param>
        public virtual TEntity Get(Expression<Func<TEntity, bool>> specExpr)
        {
            return _context.Set<TEntity>().Where(specExpr).FirstOrDefault();
        }

        #endregion

        #region GetAll method

        /// <summary>
        /// 查询实体列表.
        /// </summary>
        public virtual IQueryable<TEntity> GetAll()
        {
            return _context.Set<TEntity>();
        }

        /// <summary>
        /// 查询实体列表.
        /// </summary>
        /// <param name="topNum">实体数量</param>
        public virtual IQueryable<TEntity> GetAll(int topNum)
        {
            return GetAll().Take(topNum);
        }

        /// <summary>
        /// 查询实体列表.
        /// </summary>
        /// <param name="specExpr">查询条件</param>
        public virtual IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> specExpr)
        {
            return GetAll().Where(specExpr);
        }

        /// <summary>
        /// 查询实体列表.
        /// </summary>
        /// <param name="orderByExpression">排序条件</param>
        public virtual IQueryable<TEntity> GetAll<TResult>(OrderByExpression<TEntity, TResult> orderByExpression)
        {
            var query = GetAll();
            query = orderByExpression.OrdaerMode == OrderMode.ASC
                        ? query.OrderBy(orderByExpression.OrderByField)
                        : query.OrderByDescending(orderByExpression.OrderByField);
            return query;
        }

        /// <summary>
        /// 查询实体列表.
        /// </summary>
        /// <param name="specExpr">查询条件</param>
        /// <param name="orderByExpression">排序条件</param>
        public virtual IQueryable<TEntity> GetAll<TResult>(Expression<Func<TEntity, bool>> specExpr,
                                                              OrderByExpression<TEntity, TResult> orderByExpression)
        {
            var query = GetAll(specExpr);
            query = orderByExpression.OrdaerMode == OrderMode.ASC
                        ? query.OrderBy(orderByExpression.OrderByField)
                        : query.OrderByDescending(orderByExpression.OrderByField);
            return query;
        }

        /// <summary>
        /// 查询实体列表.
        /// </summary>
        /// <param name="topNum">实体数量</param>
        /// <param name="specExpr">查询条件</param>
        public virtual IQueryable<TEntity> GetAll(int topNum, Expression<Func<TEntity, bool>> specExpr)
        {
            return GetAll(specExpr).Take(topNum);
        }

        /// <summary>
        /// 查询实体列表.
        /// </summary>
        /// <param name="topNum">实体数量</param>
        /// <param name="orderByExpression">排序条件</param>
        public virtual IQueryable<TEntity> GetAll<TResult>(int topNum,
                                                              OrderByExpression<TEntity, TResult> orderByExpression)
        {
            return GetAll(orderByExpression).Take(topNum);
        }

        /// <summary>
        /// 查询实体列表.
        /// </summary>
        /// <param name="topNum">实体数量</param>
        /// <param name="specExpr">查询条件</param>
        /// <param name="orderByExpression">排序条件</param>
        public virtual IQueryable<TEntity> GetAll<TResult>(int topNum, Expression<Func<TEntity, bool>> specExpr,
                                                              OrderByExpression<TEntity, TResult> orderByExpression)
        {
            return GetAll(specExpr, orderByExpression).Take(topNum);
        }

        #endregion

        #region GetRandom Random method

        /// <summary>
        /// 随机获取实体列表.
        /// </summary>
        /// <param name="topNum">实体数量</param>
        public virtual IQueryable<TEntity> GetRandom(int topNum)
        {
            return GetAll(topNum).OrderBy(x => new Guid());
        }

        /// <summary>
        /// 随机获取实体列表.
        /// </summary>
        /// <param name="specExpr">查询条件</param>
        public virtual IQueryable<TEntity> GetRandom(Expression<Func<TEntity, bool>> specExpr)
        {
            return GetAll(specExpr).OrderBy(x => new Guid());
        }

        /// <summary>
        /// 随机获取实体列表.
        /// </summary>
        /// <param name="topNum">实体数量</param>
        /// <param name="specExpr">查询条件</param>
        public virtual IQueryable<TEntity> GetRandom(int topNum, Expression<Func<TEntity, bool>> specExpr)
        {
            return GetAll(specExpr).OrderBy(x => new Guid()).Take(topNum);
        }

        #endregion

        #region GetPage PageList method

        /// <summary>
        /// 查询实体列表
        /// </summary>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页数量</param>
        /// <param name="orderByExpression">排序条件</param>
        public virtual PageList<TEntity> GetPage<TResult>(int pageIndex, int pageSize,
                                                             OrderByExpression<TEntity, TResult> orderByExpression)
        {
            var query = GetAll(orderByExpression);

            return new PageList<TEntity>(pageSize)
                {
                    TotalCount = query.Count(),
                    DataList = query.Skip((pageIndex - 1)*pageSize).Take(pageSize).ToList()
                };
        }

        /// <summary>
        /// 查询实体列表
        /// </summary>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页数量</param>
        /// <param name="specExpr">查询条件</param>
        /// <param name="orderByExpression">排序条件</param>
        public virtual PageList<TEntity> GetPage<TResult>(int pageIndex, int pageSize,
                                                             Expression<Func<TEntity, bool>> specExpr,
                                                             OrderByExpression<TEntity, TResult> orderByExpression)
        {
            var query = GetAll(specExpr, orderByExpression);

            return new PageList<TEntity>(pageSize)
                {
                    TotalCount = query.Count(),
                    DataList = query.Skip((pageIndex - 1)*pageSize).Take(pageSize).ToList()
                };
        }

        #endregion

        #region Count method

        /// <summary>
        /// 查询实体数目.
        /// </summary>
        /// <returns></returns>
        public virtual int Count()
        {
            return GetAll().Count();
        }

        /// <summary>
        /// 查询实体数目.
        /// </summary>
        /// <param name="specExpr">查询条件</param>
        /// <returns></returns>
        public virtual int Count(Expression<Func<TEntity, bool>> specExpr)
        {
            return GetAll(specExpr).Count();
        }

        #endregion

        #region Exist method

        /// <summary>
        /// 判断实体是否存在.
        /// </summary>
        /// <returns></returns>
        public virtual bool Exist()
        {
            return Count() > 0;
        }

        /// <summary>
        /// 判断实体是否存在.
        /// </summary>
        /// <param name="specExpr">查询条件</param>
        /// <returns></returns>
        public virtual bool Exist(Expression<Func<TEntity, bool>> specExpr)
        {
            return Count(specExpr) > 0;
        }

        #endregion
    }
}