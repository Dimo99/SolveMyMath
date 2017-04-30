using System;
using System.Collections.Generic;

namespace SolveMath.Models.Entities
{
    public class Comment
    {
        public Comment()
        {
            Answers = new HashSet<Comment>();
        }
        public int Id { get; set; }
        public DateTime? PublishDate { get; set; }
        public virtual ApplicationUser Author { get; set; }
        public virtual Post Post { get; set; }
        public virtual ICollection<Comment> Answers { get; set; }
        public virtual Comment Parent { get; set; }
        public string Content { get; set; }
        public int UpVotes { get; set; }
        public int DownVotes { get; set; }
    }
}