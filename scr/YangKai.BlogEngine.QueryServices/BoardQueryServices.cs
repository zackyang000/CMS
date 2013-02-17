using System.Collections.Generic;
using AtomLab.Domain;
using YangKai.BlogEngine.IQueryServices;
using YangKai.BlogEngine.Modules.BoardModule.Objects;
using YangKai.BlogEngine.Modules.BoardModule.Repositories;

namespace YangKai.BlogEngine.QueryServices
{
   public class BoardQueryServices : IBoardQueryServices
   {
       readonly BoardRepository _boardRepository = InstanceLocator.Current.GetInstance<BoardRepository>();
       
       public IList<Board> FindAll(int count)
       {
           return _boardRepository.GetAll(count);
       }

       public int Count()
       {
           return _boardRepository.Count();
       }
    }
}
