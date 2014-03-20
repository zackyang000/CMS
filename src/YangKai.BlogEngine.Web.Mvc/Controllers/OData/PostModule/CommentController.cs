using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.OData;
using System.Web.Http.OData.Query;
using YangKai.BlogEngine.Common;
using YangKai.BlogEngine.Domain;
using YangKai.BlogEngine.Service;

namespace YangKai.BlogEngine.Web.Mvc.Controllers.OData
{
    public class CommentController : EntityController<Comment>
    {
        [Queryable(AllowedQueryOptions = AllowedQueryOptions.All, PageSize = 100, MaxExpansionDepth = 5)]
        public override IQueryable<Comment> Get()
        {
            return base.Get();
        }

        protected override Comment CreateEntity(Comment entity)
        {
            if (!Current.IsAdmin)
            {
                Current.User = new WebUser
                {
                    UserName = entity.Author,
                    Email = entity.Email,
                };
            }
            entity.Ip = Request.Headers.From;
            entity.Avatar = Proxy.Security().GetAvater(Current.User);
            entity.Post = Proxy.Repository<Post>().Get(entity.Post.PostId);
            base.CreateEntity(entity);
            Task.Factory.StartNew(() => Rss.Current.BuildComment());
            return entity;
        }

        public override void Remove([FromODataUri] Guid key, ODataActionParameters parameters)
        {
            base.Remove(key, parameters);

            var entity = Proxy.Repository<Comment>().Get(key).Post;
            entity.ReplyCount = entity.Comments.Count(p => !p.IsDeleted);
            Proxy.Repository<Post>().Update(entity);
        }

        [HttpPost]
        public override void Recover([FromODataUri] Guid key, ODataActionParameters parameters)
        {
            base.Recover(key, parameters);

            var entity = Proxy.Repository<Comment>().Get(key).Post;
            entity.ReplyCount = entity.Comments.Count(p => !p.IsDeleted);
            Proxy.Repository<Post>().Update(entity);
        }
    }
}