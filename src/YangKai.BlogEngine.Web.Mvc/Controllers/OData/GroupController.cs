using System;
using YangKai.BlogEngine.Domain;
using YangKai.BlogEngine.Service;

namespace YangKai.BlogEngine.Web.Mvc.Controllers.OData
{
    public class GroupController : EntityController<Group>
    {
        protected override Group CreateEntity(Group entity)
        {
            if (Proxy.Repository<Group>().Exist(p => !p.IsDeleted
                                                     && p.Url == entity.Url
                                                     && p.Channel.ChannelId == entity.Channel.ChannelId
                                                     && p.GroupId != entity.GroupId))
            {
                throw new Exception("Group has been exist.");
            }
            entity.Channel = Proxy.Repository<Channel>().Get(entity.Channel.ChannelId);
            Proxy.Repository<Group>().Add(entity);
            return entity;
        }

        protected override Group UpdateEntity(Guid key, Group update)
        {
            if (Proxy.Repository<Group>().Exist(p => !p.IsDeleted
                                                     && p.Url == update.Url
                                                     && p.Channel.ChannelId == update.Channel.ChannelId
                                                     && p.GroupId != update.GroupId))
            {
                throw new Exception("Group has been exist.");
            }

            return Proxy.Repository<Group>().Update(update);
        }
    }
}