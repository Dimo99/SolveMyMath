using Microsoft.AspNet.Identity.EntityFramework;
using SolveMath.Models;
using SolveMath.Models.Entities;

namespace SolveMath.Data
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class SolveMathContext : IdentityDbContext<ApplicationUser>
    {
        public SolveMathContext()
            : base("name=SolveMathContext")
        {
        }

        public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Reply> Replies { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Reply> Answers { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public static SolveMathContext Create()
        {
            return new SolveMathContext();
        }
    }
}