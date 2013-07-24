using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using YangKai.BlogEngine.Domain;

namespace YangKai.BlogEngine.Infrastructure.Mapping.PostModule
{
    public class ChannelConfiguration : EntityTypeConfiguration<Channel>
    {
        public ChannelConfiguration()
        {
            HasKey(p => p.ChannelId);
            Property(p => p.ChannelId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }
}
