using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using AtomLab.Utility;
using YangKai.BlogEngine.Common;
using YangKai.BlogEngine.Modules.PostModule.Commands;
using YangKai.BlogEngine.Modules.PostModule.Objects;
using YangKai.BlogEngine.ServiceProxy;
using YangKai.BlogEngine.Web.Mvc.Filters;
using YangKai.BlogEngine.Web.Mvc.Models;

namespace YangKai.BlogEngine.Web.Mvc.Controllers
{
    public class CommentController : ApiController
    {
        public IList<CommentViewModel> Get(Guid PostId)
        {
            var comments = Query.Comment.GetAll(p=>p.PostId==PostId);
            if (!WebMasterCookie.IsLogin)
            {
                comments = comments.Where(p => !p.IsDeleted);
            }
            return comments.OrderBy(p=>p.CreateDate).ToList().ToViewModels();
        }

        [UserAuthorize]
        public object Post(Guid id,CommentViewModel entity, string action)
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

        public CommentViewModel Put(CommentViewModel viewModel)
        {
            var entity = viewModel.ToEntity();
            entity.Ip = HttpContext.Current.Request.UserHostAddress;
            entity.Address = IpLocator.GetIpLocation(entity.Ip);
            entity.IsAdmin = WebMasterCookie.IsLogin;

            Command.Instance.Create(entity);
            WebGuestCookie.Save(entity.Author, entity.Email, entity.Url, true);

            return entity.ToViewModel();
        }

        // É¾³ýÆÀÂÛ
        public object Delete(Guid id)
        {
            Command.Instance.Run(new CommentDeleteEvent() { CommentId = id });
            return true;
        }

        // »Ö¸´ÆÀÂÛ
        public object Renew(Guid id)
        {
            Command.Instance.Run(new CommentRenewEvent() { CommentId = id });
            return true;
        }
    }
}