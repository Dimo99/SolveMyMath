using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using SolveMath.Models.Entities;

namespace SolveMath.Data
{
    public class SolveMathContext : IdentityDbContext<ApplicationUser>
    {
        public SolveMathContext()
            : base("SolveMath")
        {
        }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Reply> Replies { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<ForumComment> ForumComments { get; set; }
        public static SolveMathContext Create()
        {
            return new SolveMathContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasMany(x => x.SubCategories).WithMany().Map(c =>
            {
                c.MapLeftKey("CategoryId");
                c.MapRightKey("SubCategoryId");
                c.ToTable("CategoriesSubCategories");
            });
            modelBuilder.Entity<Topic>().HasMany(t => t.UpVotedUsers).WithMany().Map(t =>
            {
                t.MapLeftKey("UpVotedUserId");
                t.MapRightKey("TopicId");
                t.ToTable("TopicsUpVotedUsers");
            });
            modelBuilder.Entity<Topic>().HasMany(t => t.DownVotedUsers).WithMany().Map(t =>
            {
                t.MapLeftKey("DownVotedUserId");
                t.MapRightKey("TopicId");
                t.ToTable("TopicsDownVotedUsers");
            });
            modelBuilder.Entity<Reply>().HasMany(r => r.DownVotedUsers).WithMany().Map(r =>
            {
                r.MapLeftKey("DownVotedUserId");
                r.MapRightKey("ReplyId");
                r.ToTable("RepliesDownVotedUsers");
            });
            modelBuilder.Entity<Reply>().HasMany(r => r.UpVotedUsers).WithMany().Map(r =>
            {
                r.MapLeftKey("UpVotedUserId");
                r.MapRightKey("ReplyId");
                r.ToTable("RepliesUpVotedUsers");
            });
            modelBuilder.Entity<ForumComment>().HasMany(f => f.UpVotedUsers).WithMany().Map(r =>
            {
                r.MapLeftKey("UpVotedUserId");
                r.MapRightKey("ForumCommentId");
                r.ToTable("ForumCommentsUpVotedUsers");
            });
            modelBuilder.Entity<ForumComment>().HasMany(f => f.DownVotedUsers).WithMany().Map(f =>
            {
                f.MapLeftKey("DownVotedUserId");
                f.MapRightKey("ForumCommentId");
                f.ToTable("ForumCommentsDownVotedUsers");
            });
            base.OnModelCreating(modelBuilder);
        }
    }
}