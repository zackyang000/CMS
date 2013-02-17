using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace YangKai.BlogEngine.Infrastructure.ModelConfiguration.Board
{
    public class BoardConfiguration : EntityTypeConfiguration<Modules.BoardModule.Objects.Board>
    {
        public BoardConfiguration()
        {
            ToTable("B_留言");
            HasKey(p => p.BoardId);
            Property(p => p.BoardId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(p => p.Content).IsRequired().HasMaxLength(1000);
            Property(p => p.Author).IsRequired().HasMaxLength(100);
            Property(p => p.Ip).IsRequired().HasMaxLength(100);
            Property(p => p.Address).IsRequired().HasMaxLength(100);
            Property(p => p.CreateDate).IsRequired();
            Property(p => p.IsDeleted).IsRequired();
        }
    }
}
