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
    public class GalleryController : EntityController<Gallery>
    {
        [Queryable(AllowedQueryOptions = AllowedQueryOptions.All, PageSize = 1000, MaxExpansionDepth = 5)]
        public override IQueryable<Gallery> Get()
        {
            return Proxy.Repository<Gallery>().GetAll();
        }
    }
}