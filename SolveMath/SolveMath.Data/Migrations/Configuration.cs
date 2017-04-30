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
            if (!context.Categories.Any(x => x.Name == "���������"))
            {
                context.Categories.Add(new Category() { Name = "���������" });
                context.SaveChanges();
            }
            if (!context.Categories.Any(x => x.Name == "�������������"))
            {
                Category category = new Category()
                {
                    Name = "�������������"
                };
                context.Categories.Add(category);
                var parent = context.Categories.First(x => x.Name == "���������");
                parent.SubCategories.Add(category);
                context.SaveChanges();
            }
            if (!context.Categories.Any(x => x.Name == "�����������"))
            {
                Category category = new Category()
                {
                    Name = "�����������"
                };
                context.Categories.Add(category);
                var parent = context.Categories.First(x => x.Name == "���������");
                parent.SubCategories.Add(category);
                context.SaveChanges();
            }
            if (!context.Categories.Any(x => x.Name == "�������"))
            {
                context.Categories.Add(new Category() { Name = "�������" });
                context.SaveChanges();
            }
            if (!context.Categories.Any(x => x.Name == "������� - 1��."))
            {
                Category category = new Category()
                {
                    Name = "������� - 1��."
                };
                context.Categories.Add(category);
                var parent = context.Categories.First(x => x.Name == "�������");
                parent.SubCategories.Add(category);
                context.SaveChanges();
            }
            if (!context.Categories.Any(x => x.Name == "������� - 2��."))
            {
                Category category = new Category()
                {
                    Name = "������� - 2��."
                };
                context.Categories.Add(category);
                var parent = context.Categories.First(x => x.Name == "�������");
                parent.SubCategories.Add(category);
                context.SaveChanges();
            }
        }
    }
}
