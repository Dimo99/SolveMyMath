using System;
using System.Collections.Generic;

namespace SolveMath.Models.Entities
{
    public class Post
    {
        public int Id { get; set; }
        public DateTime? PublishDate { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}