using System.Collections.Generic;
using System.Web;
using AtomLab.Domain.Infrastructure;

namespace YangKai.BlogEngine.Web.Mvc.Extension
{
    public class PageHelper
    {
        private const string LINK_HEADER_TEMPLATE = "<{0}>; rel=\"{1}\"";

        public static void SetLinkHeader<T>(PageList<T> pageData, string baseUrl, int page, Dictionary<string, object> param=null)
        {
            var linkHeader = new List<string>();
            if (page != 1)
            {
                var link = GetParamsUrl(baseUrl, param);
                link = GetPagedUrl(link, 1);
                linkHeader.Add(string.Format(LINK_HEADER_TEMPLATE, link, "first"));
            }
            if (page > 1)
            {
                var link = GetParamsUrl(baseUrl, param);
                link = GetPagedUrl(link, page - 1);
                linkHeader.Add(string.Format(LINK_HEADER_TEMPLATE, link, "prev"));
            }
            if (page < pageData.PageCount)
            {
                var link = GetParamsUrl(baseUrl, param);
                link = GetPagedUrl(link, page + 1);
                linkHeader.Add(string.Format(LINK_HEADER_TEMPLATE, link, "next"));
            }
            if (page != pageData.PageCount)
            {
                var link = GetParamsUrl(baseUrl, param);
                link = GetPagedUrl(link, pageData.PageCount);
                linkHeader.Add(string.Format(LINK_HEADER_TEMPLATE, link, "last"));
            }

            HttpContext.Current.Response.AppendHeader("Link", string.Join(",", linkHeader));
        }

        private static string GetParamsUrl(string baseUrl, Dictionary<string, object> param)
        {
            foreach (var item in param)
            {
                if (item.Value!=null &&string.IsNullOrWhiteSpace(item.Value.ToString())) continue;
                var connector = baseUrl.Contains("?") ? "&" : "?";
                baseUrl = baseUrl + connector + item.Key + "=" + item.Value;
            }
            return baseUrl;
        }

        private static string GetPagedUrl(string baseUrl, int page)
        {
            var connector = baseUrl.Contains("?") ? "&" : "?";
            return baseUrl + connector + "page=" + page;
        }
    }
}