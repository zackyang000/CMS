using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using YangKai.BlogEngine.Modules.PostModule.Objects;

namespace YangKai.BlogEngine.Infrastructure.ModelConfiguration.Post
{
    public class PageConfiguration : EntityTypeConfiguration<Page>
    {
        public PageConfiguration()
        {
            HasKey(p => p.PageId);
            Property(p => p.PageId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(p => p.Title).IsRequired().HasMaxLength(100);
            Property(p => p.Content).IsRequired();
            Property(p => p.OrderId).IsRequired();
        }
    }
}
