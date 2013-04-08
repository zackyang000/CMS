using System;
using System.Collections.Generic;
using YangKai.BlogEngine.Modules.BoardModule.Objects;

namespace YangKai.BlogEngine.IQueryServices
{
    public interface IBoardQueryServices
    {
        IList<Board> FindAll(int count);
        int Count();
        IList<Board> GetRecent();
    }
}