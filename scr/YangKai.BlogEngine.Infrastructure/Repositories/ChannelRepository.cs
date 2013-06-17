using System;
using AtomLab.Domain.Infrastructure;
using YangKai.BlogEngine.Modules.PostModule.Objects;

namespace YangKai.BlogEngine.Modules.PostModule.Repositories
{
    public class ChannelRepository : Repository<Channel, Guid>
    {
        public ChannelRepository(IUnitOfWork context)
            : base(context)
        {
        }
    }
}