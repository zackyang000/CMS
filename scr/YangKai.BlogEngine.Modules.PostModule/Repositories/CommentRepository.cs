using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
            var orderBy = new OrderByExpression<Comment, DateTime>(p => p.CreateDate);
            return GetAll(p => p.PostId == postId, orderBy).ToList();
        }

        public IList<Comment> GetRecent(int count, string channelUrl)
        {
            Expression<Func<Comment, bool>> specExpr = p => p.Post.Group.Channel.Url == channelUrl
                                                            && !p.IsDeleted
                                                            && p.Post.PostStatus == (int) PostStatusEnum.Publish
                                                            && !p.Post.Group.IsDeleted
                                                            && !p.Post.Group.Channel.IsDeleted;
            var orderBy = new OrderByExpression<Comment, DateTime>(p => p.CreateDate, OrderMode.DESC);
            return GetAll(count, specExpr, orderBy).ToList();
        }

        public new int Count()
        {
            return Count(p => !p.IsDeleted);
        }
    }
}