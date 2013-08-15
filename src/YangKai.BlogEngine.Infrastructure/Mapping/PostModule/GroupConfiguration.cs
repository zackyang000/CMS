using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using YangKai.BlogEngine.Domain;

namespace YangKai.BlogEngine.Infrastructure.Mapping.PostModule
{
    public class GroupConfiguration : EntityTypeConfiguration<Group>
    {
        public GroupConfiguration()
        {
            HasKey(p => p.GroupId);
        }
    }
}
