using System;
using YangKai.BlogEngine.Domain;
using YangKai.BlogEngine.Service;

namespace YangKai.BlogEngine.Web.Mvc.Controllers.OData
{
    public class CategoryController : EntityController<Category>
    {
        protected override Category CreateEntity(Category entity)
        {
            if (Proxy.Repository<Category>().Exist(p => !p.IsDeleted
                                                        && p.Url == entity.Url
                                                        && p.CategoryId != entity.CategoryId
                                                        && p.Group.GroupId == entity.Group.GroupId
                ))
            {
                throw new Exception("Category has been exist.");
            }
            entity.Group = Proxy.Repository<Group>().Get(entity.Group.GroupId);
            Proxy.Repository<Category>().Add(entity);
            return entity;
        }

        protected override Category UpdateEntity(Guid key, Category update)
        {
            if (Proxy.Repository<Category>().Exist(p => !p.IsDeleted
                                                        && p.Url == update.Url
                                                        && p.Group.GroupId == update.Group.GroupId
                                                        && p.CategoryId != update.CategoryId))
            {
                throw new Exception("Category has been exist.");
            }

            var entitiy = Proxy.Repository<Category>().Get(key);
            entitiy.Name = update.Name;
            entitiy.Url = update.Url;
            entitiy.Description = update.Description;
            entitiy.IsDeleted = update.IsDeleted;
            Proxy.Repository<Category>().Update(entitiy);
            return entitiy;
        }
    }
}