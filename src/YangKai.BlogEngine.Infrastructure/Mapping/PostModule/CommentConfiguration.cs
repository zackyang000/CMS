using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using YangKai.BlogEngine.Domain;

namespace YangKai.BlogEngine.Infrastructure.Mapping.PostModule
{
    public class CommentConfiguration : EntityTypeConfiguration<Comment>
    {
        public CommentConfiguration()
        {
            HasKey(p => p.CommentId);
            Property(p => p.CommentId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }
}
