using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
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
        public IEnumerable<BoardViewModel> Get()
        {
            var data = QueryFactory.Instance.Board.FindAll(Int32.MaxValue);
            if (!WebMasterCookie.IsLogin)
            {
                data = data.Where(p => !p.IsDeleted).ToList();
            }
            return data.ToBoardViewModels();
        }
    }
}