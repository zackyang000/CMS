namespace YangKai.BlogEngine.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddQrCode : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.P_二维码信息",
                c => new
                    {
                        QrCodeId = c.Guid(nullable: false, identity: true),
                        Content = c.String(nullable: false, maxLength: 500),
                        Url = c.String(nullable: false, maxLength: 500),
                    })
                .PrimaryKey(t => t.QrCodeId);
            
            AddColumn("dbo.P_文章", "QrCode_QrCodeId", c => c.Guid());
            AddForeignKey("dbo.P_文章", "QrCode_QrCodeId", "dbo.P_二维码信息", "QrCodeId");
            CreateIndex("dbo.P_文章", "QrCode_QrCodeId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.P_文章", new[] { "QrCode_QrCodeId" });
            DropForeignKey("dbo.P_文章", "QrCode_QrCodeId", "dbo.P_二维码信息");
            DropColumn("dbo.P_文章", "QrCode_QrCodeId");
            DropTable("dbo.P_二维码信息");
        }
    }
}
