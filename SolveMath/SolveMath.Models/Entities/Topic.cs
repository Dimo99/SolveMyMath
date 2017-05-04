using System;
using System.Collections.Generic;

namespace SolveMath.Models.Entities
{
    public class Topic
    {
        public Topic()
        {
            Tags = new HashSet<Tag>();
            Replies = new HashSet<Reply>();
            UpVotedUsers = new HashSet<ApplicationUser>();
            DownVotedUsers = new HashSet<ApplicationUser>();
        }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime? PublishDate { get; set; }
        public virtual ApplicationUser Author { get; set; }
        public virtual Category Category { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }
        public virtual ICollection<Reply> Replies { get; set; }
        public int UpVotes { get; set; }
        public int DownVotes { get; set; }
        public virtual ICollection<ApplicationUser> UpVotedUsers { get; set; }
        public virtual ICollection<ApplicationUser> DownVotedUsers { get; set; }
    }
}