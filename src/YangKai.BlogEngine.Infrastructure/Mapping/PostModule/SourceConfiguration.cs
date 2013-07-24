using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using YangKai.BlogEngine.Domain;

namespace YangKai.BlogEngine.Infrastructure.Mapping.PostModule
{
    public class SourceConfiguration : EntityTypeConfiguration<Source>
    {
        public SourceConfiguration()
        {
            HasKey(p => p.SourceId);
            Property(p => p.SourceId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }
}
