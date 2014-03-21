using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.OData;
using System.Web.Http.OData.Query;
using AtomLab.Utility;
using Gma.QrCodeNet.Encoding;
using Gma.QrCodeNet.Encoding.Windows.Render;
using YangKai.BlogEngine.Common;
using YangKai.BlogEngine.Domain;
using YangKai.BlogEngine.Service;
namespace YangKai.BlogEngine.Web.Mvc.Controllers.OData
{
    public class BoardController : EntityController<Board>
    {
        [Queryable(AllowedQueryOptions = AllowedQueryOptions.All, PageSize = 100, MaxExpansionDepth = 5)]
        public override IQueryable<Board> Get()
        {
            return base.Get();
        }

        protected override Board CreateEntity(Board entity)
        {
            entity.Ip = HttpContext.Current.Request.UserHostAddress;
            entity.Avatar = GravatarHelper.GetImage(entity.Email);
            return base.CreateEntity(entity);
        }
    }
}