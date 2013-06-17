using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AtomLab.Domain;
using AtomLab.Domain.Infrastructure;
using YangKai.BlogEngine.Modules.BoardModule.Objects;

namespace YangKai.BlogEngine.Modules.BoardModule.Repositories
{
    public class BoardRepository : Repository<Board,Guid>
    {
        public BoardRepository(IUnitOfWork context)
            : base(context)
        {
        }
    }
}