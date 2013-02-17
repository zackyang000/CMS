using AtomLab.Domain.Infrastructure;
using YangKai.BlogEngine.Modules.PostModule.Objects;

namespace YangKai.BlogEngine.Modules.PostModule.Repositories
{
    public class GroupRepository : GuidRepository<Group>
    {
        public GroupRepository(IUnitOfWork context)
            : base(context)
        {
        }
    }
}