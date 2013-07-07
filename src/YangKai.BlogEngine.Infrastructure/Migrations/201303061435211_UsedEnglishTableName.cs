namespace YangKai.BlogEngine.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UsedEnglishTableName : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.P_频道", newName: "Channels");
            RenameTable(name: "dbo.P_分组", newName: "Groups");
            RenameTable(name: "dbo.P_分类", newName: "Categories");
            RenameTable(name: "dbo.P_文章", newName: "Posts");
            RenameTable(name: "dbo.P_标签", newName: "Tags");
            RenameTable(name: "dbo.P_评论", newName: "Comments");
            RenameTable(name: "dbo.P_分页", newName: "Pages");
            RenameTable(name: "dbo.P_二维码信息", newName: "QrCodes");
            RenameTable(name: "dbo.P_转载信息", newName: "Sources");
            RenameTable(name: "dbo.P_缩略图信息", newName: "Thumbnails");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Thumbnails", newName: "P_缩略图信息");
            RenameTable(name: "dbo.Sources", newName: "P_转载信息");
            RenameTable(name: "dbo.QrCodes", newName: "P_二维码信息");
            RenameTable(name: "dbo.Pages", newName: "P_分页");
            RenameTable(name: "dbo.Comments", newName: "P_评论");
            RenameTable(name: "dbo.Tags", newName: "P_标签");
            RenameTable(name: "dbo.Posts", newName: "P_文章");
            RenameTable(name: "dbo.Categories", newName: "P_分类");
            RenameTable(name: "dbo.Groups", newName: "P_分组");
            RenameTable(name: "dbo.Channels", newName: "P_频道");
        }
    }
}
