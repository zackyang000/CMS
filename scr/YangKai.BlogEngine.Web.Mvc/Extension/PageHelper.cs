using System.Collections.Generic;
using System.Web;
using AtomLab.Domain.Infrastructure;

namespace YangKai.BlogEngine.Web.Mvc.Extension
{
    public class PageHelper
    {
        private const string LINK_HEADER_TEMPLATE = "<{0}>; rel=\"{1}\"";

        public static void  SetLinkHeader<T>(PageList<T> pageData, string baseUrl, int page)
        {
            var linkHeader = new List<string>();
            if (page != 1)
            {
                var first = GetPagedUrl(baseUrl, 1);
                linkHeader.Add(string.Format(LINK_HEADER_TEMPLATE, first, "first"));
            }
            if (page > 1)
            {
                var prev = GetPagedUrl(baseUrl, page - 1);
                linkHeader.Add(string.Format(LINK_HEADER_TEMPLATE, prev, "prev"));
            }
            if (page < pageData.PageCount)
            {
                var next = GetPagedUrl(baseUrl, page + 1);
                linkHeader.Add(string.Format(LINK_HEADER_TEMPLATE, next, "next"));
            }
            if (page != pageData.PageCount)
            {
                var last = GetPagedUrl(baseUrl, pageData.PageCount);
                linkHeader.Add(string.Format(LINK_HEADER_TEMPLATE, last, "last"));
            }

            HttpContext.Current.Response.AppendHeader("Link", string.Join(",", linkHeader));
        }

        private static string GetPagedUrl(string baseUrl, int page)
        {
            var connector = baseUrl.Contains("?") ? "&" : "?";
            return baseUrl + connector + "page=" + page;
        }
    }
}