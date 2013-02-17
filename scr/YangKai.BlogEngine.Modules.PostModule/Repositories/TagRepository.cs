using System.Collections.Generic;
using System.Linq;
using AtomLab.Domain.Infrastructure;
using YangKai.BlogEngine.Model;
using YangKai.BlogEngine.Modules.PostModule.Objects;

namespace YangKai.BlogEngine.Modules.PostModule.Repositories
{
    public class TagRepository : GuidRepository<Tag>
    {
        public TagRepository(IUnitOfWork context)
            : base(context)
        {

        }

        public new IList<Tag> GetAll()
        {
            return base.GetAll().ToList();
        }

        public TagsDictionary GetTagsFrequency(string groupUrl)
        {
            var query = from a in base.GetAll(p => p.Post.Group.Url == groupUrl)
                        //where a.Modules.Post.DomainObjects.Post.IsDel == false
                        //where a.Modules.Post.DomainObjects.Post.IsAudit == true
                        //where a.Modules.Post.DomainObjects.Post.IsDraft == false 如果包含这3行,自身这篇文章如被删则都无法查询到
                        group a by new { a.Name }
                            into g
                            orderby g.Count() descending
                            select new { TagName = g.Key.Name, count = g.Count() };
            var result = query.ToDictionary(k => k.TagName, v => v.count);
           return new TagsDictionary() { Data = result };
        }
    }
}