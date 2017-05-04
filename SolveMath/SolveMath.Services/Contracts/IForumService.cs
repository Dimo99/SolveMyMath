using System.Collections.Generic;
using SolveMath.Models.BindingModels;
using SolveMath.Models.ViewModels;

namespace SolveMath.Services.Contracts
{
    public interface IForumService
    {
        IEnumerable<TopicHeaderViewModel> GetTopics(int? categoryId);
        IEnumerable<CategoryNavbarViewModel> GetCategories();
        IEnumerable<CategoryNamesViewModel> GetCategoryNames();
        void CreateTopic(TopicBindingModel tbm);
        TopicViewModel GetTopic(int id);
        void CreateAnswer(AnswerBindingModel abm,string userId);
        void CreateComment(AddForumCommentBindingModel abm, string userId);
        CategoryViewModel GetCategoryViewModel(int id);
        void UpVoteTopic(VoteBindingModel model,string userId);
        void DownVoteTopic(VoteBindingModel model,string userId);
        void UpVoteForumComment(VoteBindingModel model,string userId);
        void DownVoteForumComment(VoteBindingModel model,string userId);
        void UpVoteReply(VoteBindingModel model,string userId);
        void DownVoteReply(VoteBindingModel model,string userId);
    }
}