namespace YangKai.BlogEngine.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveQrCodeEntity : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Posts", "QrCode_QrCodeId", "dbo.QrCodes");
            DropIndex("dbo.Posts", new[] { "QrCode_QrCodeId" });
            AddColumn("dbo.Posts", "QrCode", c => c.String());

            Sql("ALTER TABLE [dbo].[Posts] DROP CONSTRAINT [FK_dbo.P_文章_dbo.P_二维码信息_QrCode_QrCodeId]");
            Sql("update posts set [QrCode]='/upload/qrcode/'+a.Url from QrCodes a where posts.QrCode_QrCodeId=a.QrCodeId");

            DropColumn("dbo.Posts", "QrCode_QrCodeId");
            DropTable("dbo.QrCodes");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.QrCodes",
                c => new
                    {
                        QrCodeId = c.Guid(nullable: false),
                        Content = c.String(),
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
                .PrimaryKey(t => t.QrCodeId);
            
            AddColumn("dbo.Posts", "QrCode_QrCodeId", c => c.Guid());
            DropColumn("dbo.Posts", "QrCode");
            CreateIndex("dbo.Posts", "QrCode_QrCodeId");
            AddForeignKey("dbo.Posts", "QrCode_QrCodeId", "dbo.QrCodes", "QrCodeId");
        }
    }
}
