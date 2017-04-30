using System;

namespace SolveMath.Models.BindingModels
{
    public class TopicBindingModel
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime PublishDate { get; set; }
        public string AuthorId { get; set; }
        public  string CategoryName { get; set; }
        public string TagsNames { get; set; }
    }
}
