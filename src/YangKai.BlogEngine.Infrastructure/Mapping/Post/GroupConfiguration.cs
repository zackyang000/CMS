using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using YangKai.BlogEngine.Modules.PostModule.Objects;

namespace YangKai.BlogEngine.Infrastructure.ModelConfiguration.Post
{
    public class GroupConfiguration : EntityTypeConfiguration<Group>
    {
        public GroupConfiguration()
        {
            HasKey(p => p.GroupId);
            Property(p => p.GroupId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(p => p.Name).IsRequired().HasMaxLength(100);
            Property(p => p.Url).IsRequired().HasMaxLength(100);
            Property(p => p.Description).IsRequired().HasMaxLength(1000);
            Property(p => p.OrderId).IsRequired();
            Property(p => p.CreateDate).IsRequired();
            Property(p => p.IsDeleted).IsRequired();
        }
    }
}
