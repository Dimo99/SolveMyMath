using System.Collections.Generic;
using SolveMath.Models.BindingModels;
using SolveMath.Models.ViewModels;

namespace SolveMath.Services.Contracts
{
    public interface IManageService
    {
        bool ValidateIsUserTopic(int id,string userId);
        TopicEditViewModel Topic(int id);
        void EditTopic(EditTopicBindingModel etbm);
        bool IsUserForumComment(int id, string userId);
        void EditForumComment(EditForumCommentBindingModel editForumCommentBinding);
        void EditReply(EditReplyBindingModel editReplyBindingModel);
        ForumCommentEditViewModel ForumComment(int id);
        ReplyEditViewModel Reply(int id);
        int TopicIdFromForumComment(int id);
        bool IsUserReply(int id,string userId);
        int TopicIdFromReply(int id);
        IEnumerable<ManageIndexTopicViewModel> UserTopics(string userId);
        IEnumerable<ManageIndexReplyViewModel> UserReplies(string userId);
        IEnumerable<ManageIndexForumCommentViewModel> UserForumComments(string userId);
        DeleteTopicViewModel DeleteTopicViewModel(int id);
        void DeleteTopic(DeleteTopicBindingModel deleteTopicBindingModel);
        DeleteForumCommentViewModel DeleteForumCommentViewModel(int id);
        void DeleteForumComment(DeleteForumCommentBindingModel deleteForumCommentBindingModel);
        DeleteReplyViewModel DeleteReplyViewModel(int id);
        void DeleteReply(DeleteReplyBindingModel bindingModel);
    }
}