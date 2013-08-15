using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Http.OData.Query;
using System.Web.Mvc;
using AtomLab.Utility;
using YangKai.BlogEngine.Common;
using YangKai.BlogEngine.Domain;
using YangKai.BlogEngine.Service;
using YangKai.BlogEngine.Web.Mvc.Filters;

namespace YangKai.BlogEngine.Web.Mvc.Controllers
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

            var entitiy = Proxy.Repository<Channel>().Get(key);
            entitiy.Name = update.Name;
            entitiy.Url = update.Url;
            entitiy.Description = update.Description;
            entitiy.IsDeleted = update.IsDeleted;
            Proxy.Repository<Channel>().Update(entitiy);
            return entitiy;
        }
    }
}