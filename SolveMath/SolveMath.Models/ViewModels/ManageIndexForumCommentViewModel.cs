namespace SolveMath.Models.ViewModels
{
    public class ManageIndexForumCommentViewModel
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public ManageIndexTopicViewModel Topic { get; set; }
    }
}