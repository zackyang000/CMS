using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using YangKai.BlogEngine.Domain;

namespace YangKai.BlogEngine.Infrastructure.Mapping.PostModule
{
    public class PostConfiguration : EntityTypeConfiguration<Post>
    {
        public PostConfiguration()
        {
            HasKey(p => p.PostId);
        }
    }
}
