using YangKai.BlogEngine.Domain;


namespace YangKai.BlogEngine.Web.Mvc.Controllers
{
    public class CommentController : EntityController<Comment>
    {
        protected override Comment CreateEntity(Comment entity)
        {
            Current.User = new WebUser()
            {
                UserName = entity.Author,
                Email = entity.Email,
            };
            return base.CreateEntity(entity);
        }

//        // É¾³ýÆÀÂÛ
//        public object Delete(Guid id)
//        {
//            var entity = Proxy.Repository<Comment>().Get(id);
//            entity.IsDeleted = true;
//            Proxy.Repository<Comment>().Update(entity);
//            return true;
//        }
//
//        // »Ö¸´ÆÀÂÛ
//        public object Renew(Guid id)
//        {
//            var entity = Proxy.Repository<Comment>().Get(id);
//            entity.IsDeleted = false;
//            Proxy.Repository<Comment>().Update(entity);
//            return true;
//        }
    }
}