using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using YangKai.BlogEngine.Modules.CommonModule.Objects;

namespace YangKai.BlogEngine.Infrastructure.ModelConfiguration.Common
{
    public class LogConfiguration : EntityTypeConfiguration<Log>
    {
        public LogConfiguration()
        {
            ToTable("C_日志");
            HasKey(p => p.LogId);
            Property(p => p.LogId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(p => p.AppName).IsRequired().HasMaxLength(100);
            Property(p => p.ModuleName).IsRequired().HasMaxLength(100);
            Property(p => p.ActionType).IsRequired().HasMaxLength(100);
            Property(p => p.BusinessId);
            Property(p => p.Description).IsRequired().HasMaxLength(1000);
            Property(p => p.User).IsRequired().HasMaxLength(100);
            Property(p => p.Ip).HasMaxLength(100);
            Property(p => p.Address).HasMaxLength(100);
            Property(p => p.Os).HasMaxLength(100);
            Property(p => p.MachineName).HasMaxLength(100);
            Property(p => p.MacAddress).HasMaxLength(100);
            Property(p => p.CreateDate).IsRequired();
        }
    }
}
