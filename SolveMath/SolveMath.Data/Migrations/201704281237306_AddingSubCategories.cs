namespace SolveMath.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingSubCategories : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Categories", "Category_Id", c => c.Int());
            CreateIndex("dbo.Categories", "Category_Id");
            AddForeignKey("dbo.Categories", "Category_Id", "dbo.Categories", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Categories", "Category_Id", "dbo.Categories");
            DropIndex("dbo.Categories", new[] { "Category_Id" });
            DropColumn("dbo.Categories", "Category_Id");
        }
    }
}
