using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AtomLab.Domain.Infrastructure;
using YangKai.BlogEngine.Modules.CommonModule.Objects;

namespace YangKai.BlogEngine.Modules.CommonModule.Repositories
{
    public class LogRepository : Repository<Log, Guid>
    {
        public LogRepository(IUnitOfWork context)
            : base(context)
        {
         
        }
    }
}