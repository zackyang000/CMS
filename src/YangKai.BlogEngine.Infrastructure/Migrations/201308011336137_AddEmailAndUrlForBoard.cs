namespace YangKai.BlogEngine.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddEmailAndUrlForBoard : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Boards", "Email", c => c.String());
            AddColumn("dbo.Boards", "Url", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Boards", "Url");
            DropColumn("dbo.Boards", "Email");
        }
    }
}
