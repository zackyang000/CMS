namespace YangKai.BlogEngine.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveThumbnailEntity : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Posts", "Thumbnail_ThumbnailId", "dbo.Thumbnails");
            DropForeignKey("dbo.Posts", "P_缩略图信息_Thumbnail_ThumbnailId", "dbo.Thumbnails");
            DropIndex("dbo.Posts", new[] { "Thumbnail_ThumbnailId" });
            AddColumn("dbo.Posts", "Thumbnail", c => c.String());

            Sql("ALTER TABLE [Posts] DROP CONSTRAINT [FK_dbo.P_文章_dbo.P_缩略图信息_Thumbnail_ThumbnailId]");
            Sql("update posts set [Thumbnail]='/upload/thumbnail/'+a.Url from Thumbnails a where posts.Thumbnail_ThumbnailId=a.ThumbnailId");

            DropColumn("dbo.Posts", "Thumbnail_ThumbnailId");
            DropTable("dbo.Thumbnails");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Thumbnails",
                c => new
                    {
                        ThumbnailId = c.Guid(nullable: false),
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
                .PrimaryKey(t => t.ThumbnailId);
            
            AddColumn("dbo.Posts", "Thumbnail_ThumbnailId", c => c.Guid());
            DropColumn("dbo.Posts", "Thumbnail");
            CreateIndex("dbo.Posts", "Thumbnail_ThumbnailId");
            AddForeignKey("dbo.Posts", "Thumbnail_ThumbnailId", "dbo.Thumbnails", "ThumbnailId");
        }
    }
}
