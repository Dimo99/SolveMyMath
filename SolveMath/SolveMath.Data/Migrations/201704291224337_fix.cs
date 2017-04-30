namespace SolveMath.Data.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class fix : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Categories", "LevelInHierarchy");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Categories", "LevelInHierarchy", c => c.Int(nullable: false));
        }
    }
}
