using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using YangKai.BlogEngine.Modules.PostModule.Objects;

namespace YangKai.BlogEngine.Infrastructure.ModelConfiguration.Post
{
    public class CommentConfiguration : EntityTypeConfiguration<Comment>
    {
        public CommentConfiguration()
        {
            ToTable("P_评论");
            HasKey(p => p.CommentId);
            Property(p => p.CommentId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(p => p.Content).IsRequired().HasMaxLength(1000);
            Property(p => p.Author).IsRequired().HasMaxLength(100);
            Property(p => p.Email).IsRequired().HasMaxLength(100);
            Property(p => p.Url).IsRequired().HasMaxLength(100);
            Property(p => p.Ip).IsRequired().HasMaxLength(100);
            Property(p => p.Address).IsRequired().HasMaxLength(100);
            Property(p => p.Pic).IsRequired().HasMaxLength(200);
            Property(p => p.PublicMode).IsRequired().HasMaxLength(100);
            Property(p => p.IsAdmin).IsRequired();
            Property(p => p.CreateDate).IsRequired();
            Property(p => p.IsDeleted).IsRequired();
            Property(p => p.ParentId);
        }
    }
}
