using System.Data.Entity.Migrations;
using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SolveMath.Models.Entities;

namespace SolveMath.Data.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<SolveMathContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(SolveMathContext context)
        {
            if (!context.Roles.Any(role => role.Name == "User"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole("User");
                manager.Create(role);
            }
            if (!context.Roles.Any(role => role.Name == "Moderator"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole("Moderator");
                manager.Create(role);
            }
            if (!context.Roles.Any(role => role.Name == "Admin"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole("Admin");
                manager.Create(role);
            }
            if (!context.Categories.Any(x => x.Name == "Геометрия"))
            {
                context.Categories.Add(new Category() { Name = "Геометрия" });
                context.SaveChanges();
            }
            if (!context.Categories.Any(x => x.Name == "Стереомертрия"))
            {
                Category category = new Category()
                {
                    Name = "Стереомертрия"
                };
                context.Categories.Add(category);
                var parent = context.Categories.First(x => x.Name == "Геометрия");
                parent.SubCategories.Add(category);
                context.SaveChanges();
            }
            if (!context.Categories.Any(x => x.Name == "Планиметрия"))
            {
                Category category = new Category()
                {
                    Name = "Планиметрия"
                };
                context.Categories.Add(category);
                var parent = context.Categories.First(x => x.Name == "Геометрия");
                parent.SubCategories.Add(category);
                context.SaveChanges();
            }
            if (!context.Categories.Any(x => x.Name == "Алгебра"))
            {
                context.Categories.Add(new Category() { Name = "Алгебра" });
                context.SaveChanges();
            }
            if (!context.Categories.Any(x => x.Name == "Алгебра - 1кл."))
            {
                Category category = new Category()
                {
                    Name = "Алгебра - 1кл."
                };
                context.Categories.Add(category);
                var parent = context.Categories.First(x => x.Name == "Алгебра");
                parent.SubCategories.Add(category);
                context.SaveChanges();
            }
            if (!context.Categories.Any(x => x.Name == "Алгебра - 2кл."))
            {
                Category category = new Category()
                {
                    Name = "Алгебра - 2кл."
                };
                context.Categories.Add(category);
                var parent = context.Categories.First(x => x.Name == "Алгебра");
                parent.SubCategories.Add(category);
                context.SaveChanges();
            }
        }
    }
}
