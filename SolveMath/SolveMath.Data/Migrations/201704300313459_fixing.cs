namespace SolveMath.Data.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class fixing : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Replies", "Parent_Id", "dbo.Replies");
            DropIndex("dbo.Replies", new[] { "Parent_Id" });
            CreateTable(
                "dbo.ForumComments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Content = c.String(),
                        UpVotes = c.Int(nullable: false),
                        DownVotes = c.Int(nullable: false),
                        PublishDate = c.DateTime(),
                        Author_Id = c.String(maxLength: 128),
                        Parent_Id = c.Int(),
                        Topic_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Author_Id)
                .ForeignKey("dbo.Replies", t => t.Parent_Id)
                .ForeignKey("dbo.Topics", t => t.Topic_Id)
                .Index(t => t.Author_Id)
                .Index(t => t.Parent_Id)
                .Index(t => t.Topic_Id);
            
            DropColumn("dbo.Replies", "Parent_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Replies", "Parent_Id", c => c.Int());
            DropForeignKey("dbo.ForumComments", "Topic_Id", "dbo.Topics");
            DropForeignKey("dbo.ForumComments", "Parent_Id", "dbo.Replies");
            DropForeignKey("dbo.ForumComments", "Author_Id", "dbo.AspNetUsers");
            DropIndex("dbo.ForumComments", new[] { "Topic_Id" });
            DropIndex("dbo.ForumComments", new[] { "Parent_Id" });
            DropIndex("dbo.ForumComments", new[] { "Author_Id" });
            DropTable("dbo.ForumComments");
            CreateIndex("dbo.Replies", "Parent_Id");
            AddForeignKey("dbo.Replies", "Parent_Id", "dbo.Replies", "Id");
        }
    }
}
