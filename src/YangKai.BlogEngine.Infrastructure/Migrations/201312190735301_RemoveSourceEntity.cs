namespace YangKai.BlogEngine.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveSourceEntity : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Posts", "Source_SourceId", "dbo.Sources");
            DropIndex("dbo.Posts", new[] { "Source_SourceId" });
            AddColumn("dbo.Posts", "Source", c => c.String());

            Sql("ALTER TABLE [dbo].[Posts] DROP CONSTRAINT [FK_dbo.P_文章_dbo.P_转载信息_Source_SourceId]");
            Sql("update posts set [Source]=a.Url from Sources a where posts.Source_SourceId=a.SourceId");

            DropColumn("dbo.Posts", "Source_SourceId");
            DropTable("dbo.Sources");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Sources",
                c => new
                    {
                        SourceId = c.Guid(nullable: false),
                        Title = c.String(),
                        Url = c.String(),
                        CreateUser = c.String(),
                        CreateIp = c.String(),
                        CreateAddress = c.String(),
                        CreateDate = c.DateTime(nullable: false),
                        LastEditIp = c.String(),
                        LastEditAddress = c.String(),
                        LastEditUser = c.String(),
                        LastEditDate = c.DateTime(),
                        OrderId = c.Int(),
                        IsDeleted = c.Boolean(nullable: false),
                        Remark = c.String(),
                    })
                .PrimaryKey(t => t.SourceId);
            
            AddColumn("dbo.Posts", "Source_SourceId", c => c.Guid());
            DropColumn("dbo.Posts", "Source");
            CreateIndex("dbo.Posts", "Source_SourceId");
            AddForeignKey("dbo.Posts", "Source_SourceId", "dbo.Sources", "SourceId");
        }
    }
}
