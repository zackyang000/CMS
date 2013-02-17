namespace YangKai.BlogEngine.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveUserProfile : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.UserProfile");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.UserProfile",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.UserId);
            
        }
    }
}
