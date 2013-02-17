using AtomLab.Domain.Infrastructure;
using YangKai.BlogEngine.Modules.PostModule.Objects;

namespace YangKai.BlogEngine.Modules.PostModule.Repositories
{
    public class ChannelRepository : GuidRepository<Channel>
    {
        public ChannelRepository(IUnitOfWork context)
            : base(context)
        {
        }
    }
}