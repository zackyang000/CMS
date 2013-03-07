using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using YangKai.BlogEngine.Modules.CommonModule.Objects;

namespace YangKai.BlogEngine.Infrastructure.ModelConfiguration.Common
{
    public class UserConfiguration : EntityTypeConfiguration<User>
    {
        public UserConfiguration()
        {
            HasKey(p => p.UserId);
            Property(p => p.UserId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(p => p.UserName).IsRequired().HasMaxLength(100);
            Property(p => p.LoginName).IsRequired().HasMaxLength(100);
            Property(p => p.Password).IsRequired().HasMaxLength(100);
            Property(p => p.Email).IsRequired().HasMaxLength(100);
            Property(p => p.CreateDate).IsRequired();
            Property(p => p.IsDeleted).IsRequired();
        }
    }
}
