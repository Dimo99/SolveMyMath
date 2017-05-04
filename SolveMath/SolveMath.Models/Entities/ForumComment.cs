using System;
using System.Collections.Generic;

namespace SolveMath.Models.Entities
{
    public class ForumComment
    {
        public ForumComment()
        {
            UpVotedUsers = new HashSet<ApplicationUser>();
            DownVotedUsers = new HashSet<ApplicationUser>();
        }
        public int Id { get; set; }
        public string Content { get; set; }
        public int UpVotes { get; set; }
        public int DownVotes { get; set; }
        public DateTime? PublishDate { get; set; }
        public virtual ApplicationUser Author { get; set; }
        public virtual Topic Topic { get; set; }
        public virtual Reply Parent { get; set; }
        public virtual ICollection<ApplicationUser> UpVotedUsers { get; set; }
        public virtual ICollection<ApplicationUser> DownVotedUsers { get; set; }
    }
}