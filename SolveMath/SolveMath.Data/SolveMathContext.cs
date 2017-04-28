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
        public static SolveMathContext Create()
        {
            return new SolveMathContext();
        }
    }
}