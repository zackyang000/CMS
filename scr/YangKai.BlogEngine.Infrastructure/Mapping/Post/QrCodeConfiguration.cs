using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using YangKai.BlogEngine.Modules.PostModule.Objects;

namespace YangKai.BlogEngine.Infrastructure.ModelConfiguration.Post
{
    public class QrCodeConfiguration : EntityTypeConfiguration<QrCode>
    {
        public QrCodeConfiguration()
        {
            ToTable("P_二维码信息");
            HasKey(p => p.QrCodeId);
            Property(p => p.QrCodeId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(p => p.Content).IsRequired().HasMaxLength(500);
            Property(p => p.Url).IsRequired().HasMaxLength(500);
        }
    }
}
