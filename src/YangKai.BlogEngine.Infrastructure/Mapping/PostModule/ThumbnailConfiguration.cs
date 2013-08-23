using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using YangKai.BlogEngine.Domain;

namespace YangKai.BlogEngine.Infrastructure.Mapping.PostModule
{
    public class ThumbnailConfiguration : EntityTypeConfiguration<Thumbnail>
    {
        public ThumbnailConfiguration()
        {
            HasKey(p => p.ThumbnailId);
        }
    }
}
