namespace YangKai.BlogEngine.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Channel_BannerUrl : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.P_频道", "BannerUrl", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.P_频道", "BannerUrl");
        }
    }
}
