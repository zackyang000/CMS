using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using YangKai.BlogEngine.Modules.PostModule.Objects;

namespace YangKai.BlogEngine.Infrastructure.ModelConfiguration.Post
{
    public class ChannelConfiguration : EntityTypeConfiguration<Channel>
    {
        public ChannelConfiguration()
        {
            HasKey(p => p.ChannelId);
            Property(p => p.ChannelId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(p => p.Name).IsRequired().HasMaxLength(100);
            Property(p => p.Url).IsRequired().HasMaxLength(100);
            Property(p => p.Description).IsRequired().HasMaxLength(1000);
            Property(p => p.StyleConfigurePath).IsRequired().HasMaxLength(100);
            Property(p => p.IsDefault).IsRequired();
            Property(p => p.OrderId).IsRequired();
            Property(p => p.CreateDate).IsRequired();
            Property(p => p.IsDeleted).IsRequired();
        }
    }
}
