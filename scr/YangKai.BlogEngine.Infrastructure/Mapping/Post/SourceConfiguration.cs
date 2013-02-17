using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using YangKai.BlogEngine.Modules.PostModule.Objects;

namespace YangKai.BlogEngine.Infrastructure.ModelConfiguration.Post
{
    public class SourceConfiguration : EntityTypeConfiguration<Source>
    {
        public SourceConfiguration()
        {
            ToTable("P_转载信息");
            HasKey(p => p.SourceId);
            Property(p => p.SourceId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(p => p.Title).IsRequired().HasMaxLength(500);
            Property(p => p.Url).IsRequired().HasMaxLength(500);
        }
    }
}
