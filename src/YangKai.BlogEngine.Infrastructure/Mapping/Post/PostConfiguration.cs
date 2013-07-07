using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace YangKai.BlogEngine.Infrastructure.ModelConfiguration.Post
{
    public class PostConfiguration : EntityTypeConfiguration<Modules.PostModule.Objects.Post>
    {
        public PostConfiguration()
        {
            HasKey(p => p.PostId);
            Property(p => p.PostId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(p => p.Url).IsRequired().HasMaxLength(500);       
            Property(p => p.Title).IsRequired().HasMaxLength(500);       
            Property(p => p.Description).IsRequired();       
            Property(p => p.GradePoint).IsRequired();       
            Property(p => p.PostStatus).IsRequired();       
            Property(p => p.CommentStatus).IsRequired();       
            Property(p => p.Password).HasMaxLength(100);       
            Property(p => p.PubDate).IsRequired();       
            Property(p => p.PubIp).IsRequired().HasMaxLength(100);       
            Property(p => p.PubAddress).IsRequired().HasMaxLength(100);       
            Property(p => p.EditDate);       
            Property(p => p.EditIp).HasMaxLength(100);       
            Property(p => p.EditAddress).HasMaxLength(100);
            Property(p => p.PageCount).IsRequired();
            Property(p => p.ViewCount).IsRequired();
            Property(p => p.ReplyCount).IsRequired();
            Property(p => p.CreateDate).IsRequired();
        }
    }
}
