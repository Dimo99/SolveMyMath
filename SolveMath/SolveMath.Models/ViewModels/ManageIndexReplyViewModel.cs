using System.Collections.Generic;

namespace SolveMath.Models.ViewModels
{
    public class ManageIndexReplyViewModel
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public ManageIndexTopicViewModel Topic { get; set; }   
    }
}
