using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AtomLab.Domain.Infrastructure;
using YangKai.BlogEngine.Model;
using YangKai.BlogEngine.Modules.PostModule.Objects;

namespace YangKai.BlogEngine.Modules.PostModule.Repositories
{
    public class CategoryRepository : GuidRepository<Category>
    {
        private readonly PostRepository _postRepository;

        public CategoryRepository(IUnitOfWork context)
            : base(context)
        {
            _postRepository = new PostRepository(context);
        }

        public IDictionary<Category,int> StatGroupByCategory(string groupUrl)
        {
            var articles =_postRepository.GetAll(p => p.PostStatus == (int) PostStatusEnum.Publish && p.Group.Url == groupUrl).ToList();
             
            var result = new Dictionary<Category, int>();

            foreach (var article in articles)
            {
                foreach (var category in article.Categorys)
                {
                    if (result.Keys.Contains(category))
                    {
                        result[category]++;
                    }
                    else
                    {
                        result.Add(category, 1);
                    }
                }
            }

            return result;
        }
    }
}