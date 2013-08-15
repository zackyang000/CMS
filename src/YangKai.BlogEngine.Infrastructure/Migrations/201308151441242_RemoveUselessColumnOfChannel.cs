using System.Data.Entity.Migrations.Model;

namespace YangKai.BlogEngine.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveUselessColumnOfChannel : DbMigration
    {
        public override void Up()
        {
            //DropForeignKey("dbo.Groups", "ChannelId", "dbo.Channels");
            //DropIndex("dbo.Groups", new[] { "ChannelId" });
            RenameColumn(table: "dbo.Groups", name: "ChannelId", newName: "Channel_ChannelId");
            AddForeignKey("dbo.Groups", "Channel_ChannelId", "dbo.Channels", "ChannelId");
            CreateIndex("dbo.Groups", "Channel_ChannelId");
            DropColumn("dbo.Channels", "StyleConfigurePath");
            DropColumn("dbo.Channels", "BannerUrl");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Channels", "BannerUrl", c => c.String());
            AddColumn("dbo.Channels", "StyleConfigurePath", c => c.String());
            DropIndex("dbo.Groups", new[] { "Channel_ChannelId" });
            DropForeignKey("dbo.Groups", "Channel_ChannelId", "dbo.Channels");
            RenameColumn(table: "dbo.Groups", name: "Channel_ChannelId", newName: "ChannelId");
            //CreateIndex("dbo.Groups", "ChannelId");
            //AddForeignKey("dbo.Groups", "ChannelId", "dbo.Channels", "ChannelId", cascadeDelete: true);
        }
    }
}
