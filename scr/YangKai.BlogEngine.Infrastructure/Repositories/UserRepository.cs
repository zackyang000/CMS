using System;
using System.Linq.Expressions;
using AtomLab.Domain.Infrastructure;
using YangKai.BlogEngine.Modules.CommonModule.Objects;

namespace YangKai.BlogEngine.Modules.CommonModule.Repositories
{
    public class UserRepository : Repository<User, Guid>
    {
        public UserRepository(IUnitOfWork context)
            : base(context)
        {
        }
    }
}