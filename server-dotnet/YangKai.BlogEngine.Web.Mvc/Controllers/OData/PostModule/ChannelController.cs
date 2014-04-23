using System;
using YangKai.BlogEngine.Domain;
using YangKai.BlogEngine.Service;

namespace YangKai.BlogEngine.Web.Mvc.Controllers.OData
{
    public class ChannelController : EntityController<Channel>
    {
        protected override Channel CreateEntity(Channel entity)
        {
            if (Proxy.Repository<Channel>().Exist(p => !p.IsDeleted && p.Url == entity.Url && p.ChannelId != entity.ChannelId))
            {
                throw new Exception("Channel has been exist.");
            }

            Proxy.Repository<Channel>().Add(entity);
            return entity;
        }

        protected override Channel UpdateEntity(Guid key, Channel update)
        {
            if (Proxy.Repository<Channel>().Exist(p => !p.IsDeleted && p.Url == update.Url && p.ChannelId != update.ChannelId))
            {
                throw new Exception("Channel has been exist.");
            }

            return Proxy.Repository<Channel>().Update(update);
        }
    }
}