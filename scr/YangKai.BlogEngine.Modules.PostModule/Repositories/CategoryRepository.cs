using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AtomLab.Domain.Infrastructure;
using YangKai.BlogEngine.Modules.PostModule.Objects;

namespace YangKai.BlogEngine.Modules.PostModule.Repositories
{
    public class CategoryRepository : GuidRepository<Category>
    {
        public CategoryRepository(IUnitOfWork context)
            : base(context)
        {

        }
    }
}