using System;
using System.Collections.Generic;
using System.Linq;
using AtomLab.Domain.Infrastructure;
using YangKai.BlogEngine.Modules.PostModule.Objects;

namespace YangKai.BlogEngine.Modules.PostModule.Repositories
{
    public class CommentRepository : GuidRepository<Comment>
    {
        public CommentRepository(IUnitOfWork context)
            : base(context)
        {
        }

        public new IList<Comment> GetAll(Guid postId)
        {
            var orderBy = new OrderByExpression<Comment, DateTime>(p => p.CreateDate, OrderMode.DESC);
            return GetAll(p => p.PostId == postId, orderBy).ToList();
        }

        public IList<Comment> GetRecent(int count)
        {
            var orderBy = new OrderByExpression<Comment, DateTime>(p => p.CreateDate, OrderMode.DESC);
            return GetAll(count, p => !p.IsDeleted, orderBy).ToList();
        }

        public new int Count()
        {
            return Count(p => !p.IsDeleted);
        }
    }
}