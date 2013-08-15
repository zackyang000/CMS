using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using YangKai.BlogEngine.Domain;

namespace YangKai.BlogEngine.Infrastructure.Mapping.PostModule
{
    public class CategoryConfiguration : EntityTypeConfiguration<Category>
    {
        public CategoryConfiguration()
        {
            HasKey(p => p.CategoryId);
        }
    }
}
