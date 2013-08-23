using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using YangKai.BlogEngine.Domain;

namespace YangKai.BlogEngine.Infrastructure.Mapping.PostModule
{
    public class TagConfiguration : EntityTypeConfiguration<Tag>
    {
        public TagConfiguration()
        {
            HasKey(p => p.TagId);
        }
    }
}
