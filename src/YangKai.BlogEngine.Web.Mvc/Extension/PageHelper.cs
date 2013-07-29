using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Http.OData.Query;

namespace YangKai.BlogEngine.Web.Mvc.Extension
{
    public class PageHelper
    {
        private const string LINK_HEADER_TEMPLATE = "<{0}>; rel=\"{1}\"";

        public static void SetLinkHeader<T>(IQueryable<T> data, ODataQueryOptions options, HttpRequestMessage request)
        {
            if (options.Top == null) return;

            var skip = options.Skip == null ? 0 : options.Skip.Value;

            var pagesize = options.Top.Value;
            int pageindex = skip / pagesize + 1;
            var totalPage = data.Count() / pagesize + (data.Count() % pagesize > 0 ? 1 : 0);

            var url = request.RequestUri.AbsoluteUri;

            var linkHeader = new List<string>();
            if (pageindex != 1)
            {
                var link = GetPagedUrl(url, pagesize, 1);
                linkHeader.Add(string.Format(LINK_HEADER_TEMPLATE, link, "first"));
            }
            if (pageindex > 1)
            {
                var link = GetPagedUrl(url, pagesize, pageindex - 1);
                linkHeader.Add(string.Format(LINK_HEADER_TEMPLATE, link, "prev"));
            }
            if (pageindex < totalPage)
            {
                var link = GetPagedUrl(url, pagesize, pageindex + 1);
                linkHeader.Add(string.Format(LINK_HEADER_TEMPLATE, link, "next"));
            }
            if (pageindex != totalPage)
            {
                var link = GetPagedUrl(url, pagesize, totalPage);
                linkHeader.Add(string.Format(LINK_HEADER_TEMPLATE, link, "last"));
            }
            HttpContext.Current.Response.AppendHeader("Link", string.Join(",", linkHeader));
        }

        private static string GetPagedUrl(string url, int pagesize, int pageindex)
        {
            var top = pagesize;
            var skip = pagesize * (pageindex - 1);

            var regex = new Regex("top=[0-9]{1,}");
            if (regex.IsMatch(url))
            {
                url = regex.Replace(url, "top=" + top);
            }

            regex = new Regex("skip=[0-9]{1,}");
            if (regex.IsMatch(url))
            {
                url = regex.Replace(url, "skip=" + skip);
            }
            else
            {
                url += "&$skip=" + skip;
            }

            return url;
        }
    }
}