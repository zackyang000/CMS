using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using YangKai.BlogEngine.Domain;

namespace YangKai.BlogEngine.Infrastructure.Mapping.Common
{
    public class LogConfiguration : EntityTypeConfiguration<Log>
    {
        public LogConfiguration()
        {
            HasKey(p => p.LogId);
            Property(p => p.LogId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }
}
