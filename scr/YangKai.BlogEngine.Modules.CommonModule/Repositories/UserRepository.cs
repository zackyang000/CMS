using System;
using System.Linq.Expressions;
using AtomLab.Domain.Infrastructure;
using YangKai.BlogEngine.Modules.CommonModule.Objects;

namespace YangKai.BlogEngine.Modules.CommonModule.Repositories
{
    public class UserRepository : GuidRepository<User>
    {
        public UserRepository(IUnitOfWork context)
            : base(context)
        {
        }

        public new User Get(Expression<Func<User, bool>> specExpr)
        {
            return base.Get(specExpr);
        }

        public bool LoginValidate(string name, string pwd)
        {
            return Exist(p => p.LoginName == name && p.Password == pwd);
        }
    }
}