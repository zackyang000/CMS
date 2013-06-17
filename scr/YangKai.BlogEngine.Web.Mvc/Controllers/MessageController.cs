using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using AtomLab.Domain.Infrastructure;
using AtomLab.Utility;
using YangKai.BlogEngine.Common;
using YangKai.BlogEngine.Modules.BoardModule.Events;
using YangKai.BlogEngine.Modules.BoardModule.Objects;
using YangKai.BlogEngine.ServiceProxy;
using YangKai.BlogEngine.Web.Mvc.Filters;
using YangKai.BlogEngine.Web.Mvc.Models;

namespace YangKai.BlogEngine.Web.Mvc.Controllers
{
    public class MessageController : ApiController
    {
        public IEnumerable<BoardViewModel> Get(string action=null)
        {
            var orderBy = new OrderByExpression<Board, DateTime>(p => p.CreateDate, OrderMode.DESC);

            if (action == "recent")
            {
                return Query.Message.GetAll(10, p => !p.IsDeleted, orderBy).ToList().ToViewModels();
            }

            var data = Query.Message.GetAll(Int32.MaxValue, orderBy);
            if (!WebMasterCookie.IsLogin)
            {
                data = data.Where(p => !p.IsDeleted);
            }
            return data.ToList().ToViewModels();
        }

        [UserAuthorize]
        public object Post(Guid id, BoardViewModel entity, string action)
        {
            switch (action)
            {
                case "delete":
                    return Delete(id);
                case "renew":
                    return Renew(id);
            }
            throw new HttpResponseException(HttpStatusCode.NotFound);
        }

        public BoardViewModel Put(BoardViewModel viewModel)
        {
            var entity = viewModel.ToEntity();
            entity.Ip = HttpContext.Current.Request.UserHostAddress;
            entity.Address = IpLocator.GetIpLocation(entity.Ip);
            
            Command.Instance.Create(entity);
            WebGuestCookie.Save(entity.Author);

            return entity.ToViewModel();
        }

        // 删除留言
        public object Delete(Guid id)
        {
            Command.Instance.Run(new BoardDeleteEvent() {BoardId = id});
            return true;
        }

        // 恢复留言
        public object Renew(Guid id)
        {
            Command.Instance.Run(new BoardRenewEvent() {BoardId = id});
            return true;
        }
    }
}