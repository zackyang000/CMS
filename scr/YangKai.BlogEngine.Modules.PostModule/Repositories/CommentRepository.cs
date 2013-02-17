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

        public new int Count()
        {
            return Count(p => !p.IsDeleted);
        }
    }
}