using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using SolveMath.Models.Entities;

namespace SolveMath.Data.Interfaces
{
    public interface ISolveMathContext
    {
        DbSet<Topic> Topics { get; set; }
        DbSet<Category> Categories { get; set; }
        DbSet<Reply> Replies { get; set; }
        DbSet<Tag> Tags { get; set; }
        DbSet<Post> Posts { get; set; }
        DbSet<Comment> Comments { get; set; }
        DbSet<ForumComment> ForumComments { get; set; }
        IDbSet<ApplicationUser> Users { get; set; }
        IDbSet<IdentityRole> Roles { get; set; }
        int SaveChanges();
    }
}
