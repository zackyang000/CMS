using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using YangKai.BlogEngine.Modules.PostModule.Objects;

namespace YangKai.BlogEngine.Infrastructure.ModelConfiguration.Post
{
    public class ThumbnailConfiguration : EntityTypeConfiguration<Thumbnail>
    {
        public ThumbnailConfiguration()
        {
            HasKey(p => p.ThumbnailId);
            Property(p => p.ThumbnailId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(p => p.Title).IsRequired().HasMaxLength(500);
            Property(p => p.Url).IsRequired().HasMaxLength(500);
        }
    }
}
