using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using YangKai.BlogEngine.Domain;

namespace YangKai.BlogEngine.Infrastructure.Mapping.BoardModule
{
    public class BoardConfiguration : EntityTypeConfiguration<Board>
    {
        public BoardConfiguration()
        {
            HasKey(p => p.BoardId);
            Property(p => p.BoardId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }
}
