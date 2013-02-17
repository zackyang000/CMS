using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using YangKai.BlogEngine.Modules.PostModule.Objects;

namespace YangKai.BlogEngine.Infrastructure.ModelConfiguration.Post
{
    public class CategoryConfiguration : EntityTypeConfiguration<Category>
    {
        public CategoryConfiguration()
        {
            ToTable("P_分类");
            HasKey(p => p.CategoryId);
            Property(p => p.CategoryId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(p => p.Name).IsRequired().HasMaxLength(100);
            Property(p => p.Url).IsRequired().HasMaxLength(100);
            Property(p => p.Description).IsRequired().HasMaxLength(1000);
            Property(p => p.OrderId).IsRequired();
            Property(p => p.CreateDate).IsRequired();
            Property(p => p.IsDeleted).IsRequired();
        }
    }
}
