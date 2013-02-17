namespace YangKai.BlogEngine.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.C_用户",
                c => new
                    {
                        UserId = c.Guid(nullable: false, identity: true),
                        UserName = c.String(nullable: false, maxLength: 100),
                        LoginName = c.String(nullable: false, maxLength: 100),
                        Password = c.String(nullable: false, maxLength: 100),
                        Email = c.String(nullable: false, maxLength: 100),
                        IsDeleted = c.Boolean(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.C_日志",
                c => new
                    {
                        LogId = c.Guid(nullable: false, identity: true),
                        AppName = c.String(nullable: false, maxLength: 100),
                        ModuleName = c.String(nullable: false, maxLength: 100),
                        ActionType = c.String(nullable: false, maxLength: 100),
                        BusinessId = c.Guid(),
                        Description = c.String(nullable: false, maxLength: 1000),
                        User = c.String(nullable: false, maxLength: 100),
                        Ip = c.String(maxLength: 100),
                        Address = c.String(maxLength: 100),
                        Os = c.String(maxLength: 100),
                        MachineName = c.String(maxLength: 100),
                        WindowsName = c.String(),
                        MacAddress = c.String(maxLength: 100),
                        Browser = c.String(),
                        BrowserVersion = c.String(),
                        CreateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.LogId);
            
            CreateTable(
                "dbo.P_频道",
                c => new
                    {
                        ChannelId = c.Guid(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        Url = c.String(nullable: false, maxLength: 100),
                        Description = c.String(nullable: false, maxLength: 1000),
                        StyleConfigurePath = c.String(nullable: false, maxLength: 100),
                        IsDefault = c.Boolean(nullable: false),
                        OrderId = c.Int(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ChannelId);
            
            CreateTable(
                "dbo.P_分组",
                c => new
                    {
                        GroupId = c.Guid(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        Url = c.String(nullable: false, maxLength: 100),
                        Description = c.String(nullable: false, maxLength: 1000),
                        OrderId = c.Int(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        ChannelId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.GroupId)
                .ForeignKey("dbo.P_频道", t => t.ChannelId, cascadeDelete: true)
                .Index(t => t.ChannelId);
            
            CreateTable(
                "dbo.P_分类",
                c => new
                    {
                        CategoryId = c.Guid(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        Url = c.String(nullable: false, maxLength: 100),
                        Description = c.String(nullable: false, maxLength: 1000),
                        OrderId = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        Group_GroupId = c.Guid(),
                    })
                .PrimaryKey(t => t.CategoryId)
                .ForeignKey("dbo.P_分组", t => t.Group_GroupId)
                .Index(t => t.Group_GroupId);
            
            CreateTable(
                "dbo.P_文章",
                c => new
                    {
                        PostId = c.Guid(nullable: false, identity: true),
                        Url = c.String(nullable: false, maxLength: 500),
                        Title = c.String(nullable: false, maxLength: 500),
                        Description = c.String(nullable: false),
                        GradePoint = c.Int(nullable: false),
                        PostStatus = c.Int(nullable: false),
                        CommentStatus = c.Int(nullable: false),
                        Password = c.String(maxLength: 100),
                        PubDate = c.DateTime(nullable: false),
                        PubIp = c.String(nullable: false, maxLength: 100),
                        PubAddress = c.String(nullable: false, maxLength: 100),
                        EditDate = c.DateTime(),
                        EditIp = c.String(maxLength: 100),
                        EditAddress = c.String(maxLength: 100),
                        PageCount = c.Int(nullable: false),
                        ViewCount = c.Int(nullable: false),
                        ReplyCount = c.Int(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        PubAdminId = c.Guid(nullable: false),
                        EditAdminId = c.Guid(),
                        GroupId = c.Guid(nullable: false),
                        Source_SourceId = c.Guid(),
                        Thumbnail_ThumbnailId = c.Guid(),
                    })
                .PrimaryKey(t => t.PostId)
                .ForeignKey("dbo.P_转载信息", t => t.Source_SourceId)
                .ForeignKey("dbo.P_缩略图信息", t => t.Thumbnail_ThumbnailId)
                .ForeignKey("dbo.C_用户", t => t.PubAdminId, cascadeDelete: true)
                .ForeignKey("dbo.C_用户", t => t.EditAdminId)
                .ForeignKey("dbo.P_分组", t => t.GroupId, cascadeDelete: true)
                .Index(t => t.Source_SourceId)
                .Index(t => t.Thumbnail_ThumbnailId)
                .Index(t => t.PubAdminId)
                .Index(t => t.EditAdminId)
                .Index(t => t.GroupId);
            
            CreateTable(
                "dbo.P_标签",
                c => new
                    {
                        TagId = c.Guid(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        PostId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.TagId)
                .ForeignKey("dbo.P_文章", t => t.PostId, cascadeDelete: true)
                .Index(t => t.PostId);
            
            CreateTable(
                "dbo.P_评论",
                c => new
                    {
                        CommentId = c.Guid(nullable: false, identity: true),
                        Content = c.String(nullable: false, maxLength: 1000),
                        Author = c.String(nullable: false, maxLength: 100),
                        Email = c.String(nullable: false, maxLength: 100),
                        Url = c.String(nullable: false, maxLength: 100),
                        Ip = c.String(nullable: false, maxLength: 100),
                        Address = c.String(nullable: false, maxLength: 100),
                        Pic = c.String(nullable: false, maxLength: 200),
                        PublicMode = c.String(nullable: false, maxLength: 100),
                        IsAdmin = c.Boolean(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        ParentId = c.Guid(),
                        PostId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.CommentId)
                .ForeignKey("dbo.P_文章", t => t.PostId, cascadeDelete: true)
                .Index(t => t.PostId);
            
            CreateTable(
                "dbo.P_分页",
                c => new
                    {
                        PageId = c.Guid(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 100),
                        Content = c.String(nullable: false),
                        OrderId = c.Int(nullable: false),
                        Post_PostId = c.Guid(),
                    })
                .PrimaryKey(t => t.PageId)
                .ForeignKey("dbo.P_文章", t => t.Post_PostId)
                .Index(t => t.Post_PostId);
            
            CreateTable(
                "dbo.P_转载信息",
                c => new
                    {
                        SourceId = c.Guid(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 500),
                        Url = c.String(nullable: false, maxLength: 500),
                    })
                .PrimaryKey(t => t.SourceId);
            
            CreateTable(
                "dbo.P_缩略图信息",
                c => new
                    {
                        ThumbnailId = c.Guid(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 500),
                        Url = c.String(nullable: false, maxLength: 500),
                    })
                .PrimaryKey(t => t.ThumbnailId);
            
            CreateTable(
                "dbo.B_留言",
                c => new
                    {
                        BoardId = c.Guid(nullable: false, identity: true),
                        Content = c.String(nullable: false, maxLength: 1000),
                        Author = c.String(nullable: false, maxLength: 100),
                        Ip = c.String(nullable: false, maxLength: 100),
                        Address = c.String(nullable: false, maxLength: 100),
                        CreateDate = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.BoardId);
            
            CreateTable(
                "dbo.P_文章_分类",
                c => new
                    {
                        Post_PostId = c.Guid(nullable: false),
                        Category_CategoryId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.Post_PostId, t.Category_CategoryId })
                .ForeignKey("dbo.P_文章", t => t.Post_PostId, cascadeDelete: true)
                .ForeignKey("dbo.P_分类", t => t.Category_CategoryId, cascadeDelete: true)
                .Index(t => t.Post_PostId)
                .Index(t => t.Category_CategoryId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.P_文章_分类", new[] { "Category_CategoryId" });
            DropIndex("dbo.P_文章_分类", new[] { "Post_PostId" });
            DropIndex("dbo.P_分页", new[] { "Post_PostId" });
            DropIndex("dbo.P_评论", new[] { "PostId" });
            DropIndex("dbo.P_标签", new[] { "PostId" });
            DropIndex("dbo.P_文章", new[] { "GroupId" });
            DropIndex("dbo.P_文章", new[] { "EditAdminId" });
            DropIndex("dbo.P_文章", new[] { "PubAdminId" });
            DropIndex("dbo.P_文章", new[] { "Thumbnail_ThumbnailId" });
            DropIndex("dbo.P_文章", new[] { "Source_SourceId" });
            DropIndex("dbo.P_分类", new[] { "Group_GroupId" });
            DropIndex("dbo.P_分组", new[] { "ChannelId" });
            DropForeignKey("dbo.P_文章_分类", "Category_CategoryId", "dbo.P_分类");
            DropForeignKey("dbo.P_文章_分类", "Post_PostId", "dbo.P_文章");
            DropForeignKey("dbo.P_分页", "Post_PostId", "dbo.P_文章");
            DropForeignKey("dbo.P_评论", "PostId", "dbo.P_文章");
            DropForeignKey("dbo.P_标签", "PostId", "dbo.P_文章");
            DropForeignKey("dbo.P_文章", "GroupId", "dbo.P_分组");
            DropForeignKey("dbo.P_文章", "EditAdminId", "dbo.C_用户");
            DropForeignKey("dbo.P_文章", "PubAdminId", "dbo.C_用户");
            DropForeignKey("dbo.P_文章", "Thumbnail_ThumbnailId", "dbo.P_缩略图信息");
            DropForeignKey("dbo.P_文章", "Source_SourceId", "dbo.P_转载信息");
            DropForeignKey("dbo.P_分类", "Group_GroupId", "dbo.P_分组");
            DropForeignKey("dbo.P_分组", "ChannelId", "dbo.P_频道");
            DropTable("dbo.P_文章_分类");
            DropTable("dbo.B_留言");
            DropTable("dbo.P_缩略图信息");
            DropTable("dbo.P_转载信息");
            DropTable("dbo.P_分页");
            DropTable("dbo.P_评论");
            DropTable("dbo.P_标签");
            DropTable("dbo.P_文章");
            DropTable("dbo.P_分类");
            DropTable("dbo.P_分组");
            DropTable("dbo.P_频道");
            DropTable("dbo.C_日志");
            DropTable("dbo.C_用户");
        }
    }
}
