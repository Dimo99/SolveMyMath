namespace SolveMath.Data.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class improvingCategory : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Categories", "Category_Id", "dbo.Categories");
            DropIndex("dbo.Categories", new[] { "Category_Id" });
            CreateTable(
                "dbo.CategoriesSubCategories",
                c => new
                    {
                        CategoryId = c.Int(nullable: false),
                        SubCategoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.CategoryId, t.SubCategoryId })
                .ForeignKey("dbo.Categories", t => t.CategoryId)
                .ForeignKey("dbo.Categories", t => t.SubCategoryId)
                .Index(t => t.CategoryId)
                .Index(t => t.SubCategoryId);
            
            AddColumn("dbo.Categories", "LevelInHierarchy", c => c.Int(nullable: false));
            DropColumn("dbo.Categories", "Category_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Categories", "Category_Id", c => c.Int());
            DropForeignKey("dbo.CategoriesSubCategories", "SubCategoryId", "dbo.Categories");
            DropForeignKey("dbo.CategoriesSubCategories", "CategoryId", "dbo.Categories");
            DropIndex("dbo.CategoriesSubCategories", new[] { "SubCategoryId" });
            DropIndex("dbo.CategoriesSubCategories", new[] { "CategoryId" });
            DropColumn("dbo.Categories", "LevelInHierarchy");
            DropTable("dbo.CategoriesSubCategories");
            CreateIndex("dbo.Categories", "Category_Id");
            AddForeignKey("dbo.Categories", "Category_Id", "dbo.Categories", "Id");
        }
    }
}
