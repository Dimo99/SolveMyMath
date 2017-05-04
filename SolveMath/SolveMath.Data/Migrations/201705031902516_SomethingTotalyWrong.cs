namespace SolveMath.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SomethingTotalyWrong : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ForumCommentsDownVotedUsers",
                c => new
                    {
                        DownVotedUserId = c.Int(nullable: false),
                        ForumCommentId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.DownVotedUserId, t.ForumCommentId })
                .ForeignKey("dbo.ForumComments", t => t.DownVotedUserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.ForumCommentId, cascadeDelete: true)
                .Index(t => t.DownVotedUserId)
                .Index(t => t.ForumCommentId);
            
            CreateTable(
                "dbo.RepliesDownVotedUsers",
                c => new
                    {
                        DownVotedUserId = c.Int(nullable: false),
                        ReplyId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.DownVotedUserId, t.ReplyId })
                .ForeignKey("dbo.Replies", t => t.DownVotedUserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.ReplyId, cascadeDelete: true)
                .Index(t => t.DownVotedUserId)
                .Index(t => t.ReplyId);
            
            CreateTable(
                "dbo.RepliesUpVotedUsers",
                c => new
                    {
                        UpVotedUserId = c.Int(nullable: false),
                        ReplyId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UpVotedUserId, t.ReplyId })
                .ForeignKey("dbo.Replies", t => t.UpVotedUserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.ReplyId, cascadeDelete: true)
                .Index(t => t.UpVotedUserId)
                .Index(t => t.ReplyId);
            
            CreateTable(
                "dbo.ForumCommentsUpVotedUsers",
                c => new
                    {
                        UpVotedUserId = c.Int(nullable: false),
                        ForumCommentId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UpVotedUserId, t.ForumCommentId })
                .ForeignKey("dbo.ForumComments", t => t.UpVotedUserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.ForumCommentId, cascadeDelete: true)
                .Index(t => t.UpVotedUserId)
                .Index(t => t.ForumCommentId);
            
            CreateTable(
                "dbo.TopicsDownVotedUsers",
                c => new
                    {
                        DownVotedUserId = c.Int(nullable: false),
                        TopicId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.DownVotedUserId, t.TopicId })
                .ForeignKey("dbo.Topics", t => t.DownVotedUserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.TopicId, cascadeDelete: true)
                .Index(t => t.DownVotedUserId)
                .Index(t => t.TopicId);
            
            CreateTable(
                "dbo.TopicsUpVotedUsers",
                c => new
                    {
                        UpVotedUserId = c.Int(nullable: false),
                        TopicId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UpVotedUserId, t.TopicId })
                .ForeignKey("dbo.Topics", t => t.UpVotedUserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.TopicId, cascadeDelete: true)
                .Index(t => t.UpVotedUserId)
                .Index(t => t.TopicId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TopicsUpVotedUsers", "TopicId", "dbo.AspNetUsers");
            DropForeignKey("dbo.TopicsUpVotedUsers", "UpVotedUserId", "dbo.Topics");
            DropForeignKey("dbo.TopicsDownVotedUsers", "TopicId", "dbo.AspNetUsers");
            DropForeignKey("dbo.TopicsDownVotedUsers", "DownVotedUserId", "dbo.Topics");
            DropForeignKey("dbo.ForumCommentsUpVotedUsers", "ForumCommentId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ForumCommentsUpVotedUsers", "UpVotedUserId", "dbo.ForumComments");
            DropForeignKey("dbo.RepliesUpVotedUsers", "ReplyId", "dbo.AspNetUsers");
            DropForeignKey("dbo.RepliesUpVotedUsers", "UpVotedUserId", "dbo.Replies");
            DropForeignKey("dbo.RepliesDownVotedUsers", "ReplyId", "dbo.AspNetUsers");
            DropForeignKey("dbo.RepliesDownVotedUsers", "DownVotedUserId", "dbo.Replies");
            DropForeignKey("dbo.ForumCommentsDownVotedUsers", "ForumCommentId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ForumCommentsDownVotedUsers", "DownVotedUserId", "dbo.ForumComments");
            DropIndex("dbo.TopicsUpVotedUsers", new[] { "TopicId" });
            DropIndex("dbo.TopicsUpVotedUsers", new[] { "UpVotedUserId" });
            DropIndex("dbo.TopicsDownVotedUsers", new[] { "TopicId" });
            DropIndex("dbo.TopicsDownVotedUsers", new[] { "DownVotedUserId" });
            DropIndex("dbo.ForumCommentsUpVotedUsers", new[] { "ForumCommentId" });
            DropIndex("dbo.ForumCommentsUpVotedUsers", new[] { "UpVotedUserId" });
            DropIndex("dbo.RepliesUpVotedUsers", new[] { "ReplyId" });
            DropIndex("dbo.RepliesUpVotedUsers", new[] { "UpVotedUserId" });
            DropIndex("dbo.RepliesDownVotedUsers", new[] { "ReplyId" });
            DropIndex("dbo.RepliesDownVotedUsers", new[] { "DownVotedUserId" });
            DropIndex("dbo.ForumCommentsDownVotedUsers", new[] { "ForumCommentId" });
            DropIndex("dbo.ForumCommentsDownVotedUsers", new[] { "DownVotedUserId" });
            DropTable("dbo.TopicsUpVotedUsers");
            DropTable("dbo.TopicsDownVotedUsers");
            DropTable("dbo.ForumCommentsUpVotedUsers");
            DropTable("dbo.RepliesUpVotedUsers");
            DropTable("dbo.RepliesDownVotedUsers");
            DropTable("dbo.ForumCommentsDownVotedUsers");
        }
    }
}
