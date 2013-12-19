namespace YangKai.BlogEngine.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveCategory : DbMigration
    {
        public override void Up()
        {
            //删除Category前将原Categories转移至Tags
            Sql("insert Tags select newid(),[Name],a.Post_PostId,[CreateUser],[CreateDate],[LastEditUser],[LastEditDate],[OrderId],[IsDeleted],[Remark],[CreateIp],[CreateAddress],[LastEditIp],[LastEditAddress] FROM PostCategories a inner join [Categories] b on a.Category_CategoryId=b.CategoryId where name<>'早期数据' and name<>'默认' and name<>'default'");

            DropForeignKey("dbo.Categories", "Group_GroupId", "dbo.Groups");
            DropForeignKey("dbo.PostCategories", "Post_PostId", "dbo.Posts");
            DropForeignKey("dbo.PostCategories", "Category_CategoryId", "dbo.Categories");
            DropIndex("dbo.Categories", new[] { "Group_GroupId" });
            DropIndex("dbo.PostCategories", new[] { "Post_PostId" });
            DropIndex("dbo.PostCategories", new[] { "Category_CategoryId" });
            DropTable("dbo.PostCategories");
            DropTable("dbo.Categories");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.PostCategories",
                c => new
                    {
                        Post_PostId = c.Guid(nullable: false),
                        Category_CategoryId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.Post_PostId, t.Category_CategoryId });
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryId = c.Guid(nullable: false),
                        Name = c.String(),
                        Url = c.String(),
                        Description = c.String(),
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
                        Group_GroupId = c.Guid(),
                    })
                .PrimaryKey(t => t.CategoryId);
            
            CreateIndex("dbo.PostCategories", "Category_CategoryId");
            CreateIndex("dbo.PostCategories", "Post_PostId");
            CreateIndex("dbo.Categories", "Group_GroupId");
            AddForeignKey("dbo.PostCategories", "Category_CategoryId", "dbo.Categories", "CategoryId", cascadeDelete: true);
            AddForeignKey("dbo.PostCategories", "Post_PostId", "dbo.Posts", "PostId", cascadeDelete: true);
            AddForeignKey("dbo.Categories", "Group_GroupId", "dbo.Groups", "GroupId");
        }
    }
}
