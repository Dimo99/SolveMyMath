using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;

namespace SolveMath.Models.Entities
{
    public class Topic
    {
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
    }
}