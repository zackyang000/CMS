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
            var orderBy = new OrderByExpression<Board, DateTime>(p => p.CreateDate, OrderMode.DESC);
            return GetAll(count, orderBy).ToList();
        }

        public List<Board> GetRecent(int count)
        {
            var orderBy = new OrderByExpression<Board, DateTime>(p => p.CreateDate, OrderMode.DESC);
            return GetAll(count, p => !p.IsDeleted, orderBy).ToList();
        }

        public new int Count()
        {
            return Count(p => !p.IsDeleted);
        }
    }
}