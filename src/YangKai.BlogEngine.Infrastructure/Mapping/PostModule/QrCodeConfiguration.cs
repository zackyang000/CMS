using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using YangKai.BlogEngine.Domain;

namespace YangKai.BlogEngine.Infrastructure.Mapping.PostModule
{
    public class QrCodeConfiguration : EntityTypeConfiguration<QrCode>
    {
        public QrCodeConfiguration()
        {
            HasKey(p => p.QrCodeId);
        }
    }
}
