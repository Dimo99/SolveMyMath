using System.Collections.Generic;
using System.ComponentModel;

namespace SolveMath.Models.ViewModels
{
    public class TopicEditViewModel
    {
        public int Id { get; set; }
        [DisplayName("Съдържание")]
        public string Content { get; set; }
        [DisplayName("Заглавие")]
        public string Title { get; set; }
        public IEnumerable<string> CategoryNames { get; set; }
        public string CategoryName { get; set; }
        public IEnumerable<string> TagNames { get; set; }
    }
}
