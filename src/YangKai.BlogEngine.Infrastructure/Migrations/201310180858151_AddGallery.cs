namespace YangKai.BlogEngine.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddGallery : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Galleries",
                c => new
                    {
                        GalleryId = c.Guid(nullable: false),
                        Name = c.String(),
                        Description = c.String(),
                        CoverPath = c.String(),
                        CreateUser = c.String(),
                        CreateDate = c.DateTime(nullable: false),
                        LastEditUser = c.String(),
                        LastEditDate = c.DateTime(),
                        OrderId = c.Int(),
                        IsDeleted = c.Boolean(nullable: false),
                        Remark = c.String(),
                    })
                .PrimaryKey(t => t.GalleryId);
            
            CreateTable(
                "dbo.Photos",
                c => new
                    {
                        PhotoId = c.Guid(nullable: false),
                        Name = c.String(),
                        Description = c.String(),
                        Path = c.String(),
                        CreateUser = c.String(),
                        CreateDate = c.DateTime(nullable: false),
                        LastEditUser = c.String(),
                        LastEditDate = c.DateTime(),
                        OrderId = c.Int(),
                        IsDeleted = c.Boolean(nullable: false),
                        Remark = c.String(),
                        Gallery_GalleryId = c.Guid(),
                    })
                .PrimaryKey(t => t.PhotoId)
                .ForeignKey("dbo.Galleries", t => t.Gallery_GalleryId)
                .Index(t => t.Gallery_GalleryId);
            
            CreateTable(
                "dbo.PhotoComments",
                c => new
                    {
                        PhotoCommentId = c.Guid(nullable: false),
                        Content = c.String(),
                        Author = c.String(),
                        Email = c.String(),
                        Ip = c.String(),
                        Address = c.String(),
                        CreateUser = c.String(),
                        CreateDate = c.DateTime(nullable: false),
                        LastEditUser = c.String(),
                        LastEditDate = c.DateTime(),
                        OrderId = c.Int(),
                        IsDeleted = c.Boolean(nullable: false),
                        Remark = c.String(),
                        Photo_PhotoId = c.Guid(),
                    })
                .PrimaryKey(t => t.PhotoCommentId)
                .ForeignKey("dbo.Photos", t => t.Photo_PhotoId)
                .Index(t => t.Photo_PhotoId);
            
            AlterColumn("dbo.Users", "UserId", c => c.Guid(nullable: false));
            AlterColumn("dbo.Logs", "LogId", c => c.Guid(nullable: false));
        }
        
        public override void Down()
        {
            DropIndex("dbo.PhotoComments", new[] { "Photo_PhotoId" });
            DropIndex("dbo.Photos", new[] { "Gallery_GalleryId" });
            DropForeignKey("dbo.PhotoComments", "Photo_PhotoId", "dbo.Photos");
            DropForeignKey("dbo.Photos", "Gallery_GalleryId", "dbo.Galleries");
            AlterColumn("dbo.Logs", "LogId", c => c.Guid(nullable: false, identity: true));
            AlterColumn("dbo.Users", "UserId", c => c.Guid(nullable: false, identity: true));
            DropTable("dbo.PhotoComments");
            DropTable("dbo.Photos");
            DropTable("dbo.Galleries");
        }
    }
}
