namespace YangKai.BlogEngine.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UsedEnglishTableNameRestPart : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.C_用户", newName: "Users");
            RenameTable(name: "dbo.C_日志", newName: "Logs");
            RenameTable(name: "dbo.B_留言", newName: "Boards");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Boards", newName: "B_留言");
            RenameTable(name: "dbo.Logs", newName: "C_日志");
            RenameTable(name: "dbo.Users", newName: "C_用户");
        }
    }
}
