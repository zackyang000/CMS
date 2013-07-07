namespace YangKai.BlogEngine.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UsedEnglishTableNameRestPart2 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.P_文章_分类", newName: "PostCategories");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.PostCategories", newName: "P_文章_分类");
        }
    }
}
