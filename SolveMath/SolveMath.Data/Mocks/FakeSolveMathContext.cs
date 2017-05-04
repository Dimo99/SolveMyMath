using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using SolveMath.Data.Interfaces;
using SolveMath.Models.Entities;

namespace SolveMath.Data.Mocks
{
    public class FakeSolveMathContext : ISolveMathContext
    {
        public FakeSolveMathContext()
        {
            this.Categories = new FakeDbSet<Category>();
			this.ForumComments = new FakeDbSet<ForumComment>();
			this.Replies  = new FakeDbSet<Reply>();
			this.Roles = new FakeDbSet<IdentityRole>();
			this.Users = new FakeDbSet<ApplicationUser>();
            this.Topics = new FakeDbSet<Topic>();
            this.Tags = new FakeDbSet<Tag>();
			this.Posts = new FakeDbSet<Post>();
			this.Comments = new FakeDbSet<Comment>();
        }

        public int SaveChanges()
        {
            return 0;
        }

        public DbSet<Topic> Topics { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Reply> Replies { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<ForumComment> ForumComments { get; set; }
        public IDbSet<ApplicationUser> Users { get; set; }
        public IDbSet<IdentityRole> Roles { get; set; }
    }
}