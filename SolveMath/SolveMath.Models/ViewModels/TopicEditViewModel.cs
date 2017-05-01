using System.Collections.Generic;

namespace SolveMath.Models.ViewModels
{
    public class TopicEditViewModel
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string Title { get; set; }
        public IEnumerable<string> CategoryNames { get; set; }
        public string CategoryName { get; set; }
        public IEnumerable<string> TagNames { get; set; }
    }
}
