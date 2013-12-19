namespace YangKai.BlogEngine.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovePostUselessProperties : DbMigration
    {
        public override void Up()
        {
            Sql("update posts set CreateUser=a.UserName from Users a where posts.PubAdmin_UserId=a.UserId and posts.PubAdmin_UserId is not null");
            Sql("update posts set LastEditUser=a.UserName from Users a where posts.EditAdmin_UserId=a.UserId and posts.EditAdmin_UserId is not null");
            Sql("update posts set CreateIp=PubIp where PubIp is not null");
            Sql("update posts set CreateAddress=PubAddress where PubAddress is not null");
            Sql("update posts set LastEditIp=EditIp where EditIp is not null");
            Sql("update posts set LastEditAddress=EditAddress where EditAddress is not null");

            Sql("ALTER TABLE [dbo].[Posts] DROP CONSTRAINT [FK_dbo.P_文章_dbo.C_用户_PubAdminId]");
            Sql("ALTER TABLE [dbo].[Posts] DROP CONSTRAINT [FK_dbo.P_文章_dbo.C_用户_EditAdminId]");
            Sql("DROP INDEX [IX_EditAdminId] ON [dbo].[Posts]");

            DropForeignKey("dbo.Posts", "EditAdmin_UserId", "dbo.Users");
            DropForeignKey("dbo.Posts", "PubAdmin_UserId", "dbo.Users");
            DropIndex("dbo.Posts", new[] { "EditAdmin_UserId" });
            DropIndex("dbo.Posts", new[] { "PubAdmin_UserId" });
            DropColumn("dbo.Posts", "PubIp");
            DropColumn("dbo.Posts", "PubAddress");
            DropColumn("dbo.Posts", "EditDate");
            DropColumn("dbo.Posts", "EditIp");
            DropColumn("dbo.Posts", "EditAddress");
            DropColumn("dbo.Posts", "PageCount");
            DropColumn("dbo.Posts", "EditAdmin_UserId");
            DropColumn("dbo.Posts", "PubAdmin_UserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Posts", "PubAdmin_UserId", c => c.Guid());
            AddColumn("dbo.Posts", "EditAdmin_UserId", c => c.Guid());
            AddColumn("dbo.Posts", "PageCount", c => c.Int(nullable: false));
            AddColumn("dbo.Posts", "EditAddress", c => c.String());
            AddColumn("dbo.Posts", "EditIp", c => c.String());
            AddColumn("dbo.Posts", "EditDate", c => c.DateTime());
            AddColumn("dbo.Posts", "PubAddress", c => c.String());
            AddColumn("dbo.Posts", "PubIp", c => c.String());
            CreateIndex("dbo.Posts", "PubAdmin_UserId");
            CreateIndex("dbo.Posts", "EditAdmin_UserId");
            AddForeignKey("dbo.Posts", "PubAdmin_UserId", "dbo.Users", "UserId");
            AddForeignKey("dbo.Posts", "EditAdmin_UserId", "dbo.Users", "UserId");
        }
    }
}
