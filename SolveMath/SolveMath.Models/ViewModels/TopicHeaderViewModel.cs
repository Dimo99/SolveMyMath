using System;

namespace SolveMath.Models.ViewModels
{
    public class TopicHeaderViewModel
    {
        public string Title { get; set; }
        public int NumberOfAnswers { get; set; }
        public int Score { get; set; }
        public DateTime PublishDate { get; set; }
        public string AuthorUsername { get; set; }
        public string CategoryName { get; set; }
    }
}
