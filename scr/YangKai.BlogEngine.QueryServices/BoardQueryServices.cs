using System;
using System.Collections.Generic;
using System.Linq;
using AtomLab.Domain;
using AtomLab.Domain.Infrastructure;
using YangKai.BlogEngine.IQueryServices;
using YangKai.BlogEngine.Modules.BoardModule.Objects;

namespace YangKai.BlogEngine.QueryServices
{
   public class BoardQueryServices : IBoardQueryServices
   {
       readonly GuidRepository<Board> _boardRepository = InstanceLocator.Current.GetInstance<GuidRepository<Board>>();
       
       public IList<Board> FindAll(int count)
       {
           var orderBy = new OrderByExpression<Board, DateTime>(p => p.CreateDate, OrderMode.DESC);
           return _boardRepository.GetAll(count, orderBy).ToList();
       }

          public IList<Board> GetRecent()
       {
           var orderBy = new OrderByExpression<Board, DateTime>(p => p.CreateDate, OrderMode.DESC);
           return _boardRepository.GetAll(10, p => !p.IsDeleted, orderBy).ToList();
       }

       public int Count()
       {
           return _boardRepository.Count(p => !p.IsDeleted);
       }
    }
}
