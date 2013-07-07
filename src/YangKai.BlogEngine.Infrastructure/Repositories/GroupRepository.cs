using System;
using AtomLab.Domain.Infrastructure;
using YangKai.BlogEngine.Modules.PostModule.Objects;

namespace YangKai.BlogEngine.Modules.PostModule.Repositories
{
    public class GroupRepository : Repository<Group, Guid>
    {
        public GroupRepository(IUnitOfWork context)
            : base(context)
        {
        }
    }
}