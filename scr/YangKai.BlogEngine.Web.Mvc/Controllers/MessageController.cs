using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using AtomLab.Utility;
using YangKai.BlogEngine.Common;
using YangKai.BlogEngine.Modules.BoardModule.Events;
using YangKai.BlogEngine.ServiceProxy;
using YangKai.BlogEngine.Web.Mvc.Filters;
using YangKai.BlogEngine.Web.Mvc.Models;

namespace YangKai.BlogEngine.Web.Mvc.Controllers
{
    public class MessageController : ApiController
    {
        public IEnumerable<BoardViewModel> Get(string action=null)
        {
            if (action == "recent")
            {
                return QueryFactory.Instance.Board.GetRecent().ToBoardViewModels();
            }

            var data = QueryFactory.Instance.Board.FindAll(Int32.MaxValue);
            if (!WebMasterCookie.IsLogin)
            {
                data = data.Where(p => !p.IsDeleted).ToList();
            }
            return data.ToBoardViewModels();
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
            var entity = viewModel.ToBoardEntity();
            entity.Ip = HttpContext.Current.Request.UserHostAddress;
            entity.Address = IpLocator.GetIpLocation(entity.Ip);
            
            CommandFactory.Instance.Create(entity);

            return entity.ToBoardViewModel();
        }

        // 删除留言
        public object Delete(Guid id)
        {
            CommandFactory.Instance.Run(new BoardDeleteEvent() {BoardId = id});
            return true;
        }

        // 恢复留言
        public object Renew(Guid id)
        {
            CommandFactory.Instance.Run(new BoardRenewEvent() {BoardId = id});
            return true;
        }
    }
}