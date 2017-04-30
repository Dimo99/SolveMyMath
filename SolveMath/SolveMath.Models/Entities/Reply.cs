using System;
using System.Collections.Generic;

namespace SolveMath.Models.Entities
{
    public class Reply
    {
        public Reply()
        {
            ForumComments = new HashSet<ForumComment>();
        }
        public int Id { get; set; }
        public string Content { get; set; }
        public int UpVotes { get; set; }
        public int DownVotes { get; set; }
        public DateTime? PublishDate { get; set; }
        public virtual ApplicationUser Author { get; set; }
        public virtual Topic Topic { get; set; }
        public virtual ICollection<ForumComment> ForumComments { get; set; }
    }
}