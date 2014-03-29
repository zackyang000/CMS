using System;
using System.Threading.Tasks;
using YangKai.BlogEngine.Domain;
using YangKai.BlogEngine.Service;

namespace YangKai.BlogEngine.Web.Mvc.Controllers.OData
{
    public class IssueController : EntityController<Issue>
    {
        protected override Issue UpdateEntity(Guid key, Issue update)
        {
            var data = base.UpdateEntity(key, update);
            Task.Factory.StartNew(() => Rss.Current.BuildIssue());
            return data;
        }
    }
}