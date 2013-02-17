using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AtomLab.Domain;
using AtomLab.Domain.Infrastructure;
using YangKai.BlogEngine.Modules.BoardModule.Objects;

namespace YangKai.BlogEngine.Modules.BoardModule.Repositories
{
    public class BoardRepository : GuidRepository<Board>
    {
        public BoardRepository(IUnitOfWork context)
            : base(context)
        {
        }

        public new List<Board> GetAll(int count)
        {
            Expression<Func<Board, bool>> specExpr = p => !p.IsDeleted;
            var orderBy = new OrderByExpression<Board, DateTime>(p => p.CreateDate, OrderMode.DESC);
            return GetAll(count, specExpr, orderBy).ToList();
        }

        public new int Count()
        {
            return Count(p => !p.IsDeleted);
        }
    }
}