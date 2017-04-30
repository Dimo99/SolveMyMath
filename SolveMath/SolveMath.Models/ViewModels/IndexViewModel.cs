using System.Collections.Generic;
using Microsoft.AspNet.Identity;
using SolveMath.Models.Entities;

namespace SolveMath.Models.ViewModels
{
    public class IndexViewModel
    {
        public bool HasPassword { get; set; }
        public IList<UserLoginInfo> Logins { get; set; }
        public string PhoneNumber { get; set; }
        public bool TwoFactor { get; set; }
        public bool BrowserRemembered { get; set; }
        public IEnumerable<ManageIndexTopicViewModel> UserTopics { get; set; }
        public IEnumerable<ManageIndexReplyViewModel> Replies { get; set; }
        public IEnumerable<ManageIndexForumCommentViewModel> ForumComments { get; set; }
    }
}
