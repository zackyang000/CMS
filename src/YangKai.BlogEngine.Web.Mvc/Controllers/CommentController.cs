using YangKai.BlogEngine.Domain;


namespace YangKai.BlogEngine.Web.Mvc.Controllers
{
    public class CommentController : EntityController<Comment>
    {
        protected override Comment CreateEntity(Comment entity)
        {
            if (!Current.IsAdmin)
            {
                Current.User = new WebUser()
                {
                    UserName = entity.Author,
                    Email = entity.Email,
                };
            }

            return base.CreateEntity(entity);
        }
    }
}