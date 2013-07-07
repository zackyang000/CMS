using System;
using System.Collections.Generic;
using System.Linq;
using AtomLab.Domain.Infrastructure;
using YangKai.BlogEngine.Modules.PostModule.Objects;

namespace YangKai.BlogEngine.Modules.PostModule.Repositories
{
    public class TagRepository : Repository<Tag, Guid>
    {
        public TagRepository(IUnitOfWork context)
            : base(context)
        {

        }
    }
}