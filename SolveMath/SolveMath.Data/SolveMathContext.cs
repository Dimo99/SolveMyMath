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
            base.OnModelCreating(modelBuilder);
        }
    }
}