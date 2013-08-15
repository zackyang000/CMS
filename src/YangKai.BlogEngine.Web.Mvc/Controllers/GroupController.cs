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
    public class GroupController : EntityController<Group>
    {
        protected override Group CreateEntity(Group entity)
        {
            if (Proxy.Repository<Group>().Exist(p => !p.IsDeleted && p.Url == entity.Url && p.GroupId != entity.GroupId))
            {
                throw new Exception("Group has been exist.");
            }
            entity.Channel = Proxy.Repository<Channel>().Get(entity.Channel.ChannelId);
            Proxy.Repository<Group>().Add(entity);
            return entity;
        }

        protected override Group UpdateEntity(Guid key, Group update)
        {
            if (Proxy.Repository<Group>().Exist(p => !p.IsDeleted && p.Url == update.Url && p.GroupId != update.GroupId))
            {
                throw new Exception("Group has been exist.");
            }

            var entitiy = Proxy.Repository<Group>().Get(key);
            entitiy.Name = update.Name;
            entitiy.Url = update.Url;
            entitiy.Description = update.Description;
            entitiy.IsDeleted = update.IsDeleted;
            Proxy.Repository<Group>().Update(entitiy);
            return entitiy;
        }
    }
}