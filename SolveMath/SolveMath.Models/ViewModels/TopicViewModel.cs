using System;
using System.Collections.Generic;
using SolveMath.Models.Entities;

namespace SolveMath.Models.ViewModels
{
    public class TopicViewModel
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public ApplicationUser Author { get; set; }
        public int Score { get; set; }
        public DateTime PublishDate { get; set; }
        public Category Category { get; set; }
        public ICollection<Reply> Replies { get; set; }
        public ICollection<Tag> Tags { get; set; }
        public int Id { get; set; }
    }
}
