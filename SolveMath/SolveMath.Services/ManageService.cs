using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using SolveMath.Models.BindingModels;
using SolveMath.Models.ViewModels;
using SolveMath.Services.Contracts;

namespace SolveMath.Services
{
    public class ManageService : Service, IManageService
    {
        public bool ValidateIsUserTopic(int id, string userId)
        {
            var topicAuthorId = Context.Topics.Find(id).Author.Id;
            return topicAuthorId == userId;
        }

        public TopicEditViewModel Topic(int id)
        {
            return Mapper.Map<TopicEditViewModel>(this.Context.Topics.Find(id));
        }

        public void EditTopic(EditTopicBindingModel etbm)
        {
            var topic = Context.Topics.Find(etbm.Id);
            topic.Content = etbm.Content;
            Context.SaveChanges();
        }

        public bool IsUserForumComment(int id, string userId)
        {
            var commentAuthorId = Context.ForumComments.Find(id).Author.Id;
            return commentAuthorId == userId;
        }

        public void EditForumComment(EditForumCommentBindingModel editForumCommentBinding)
        {
            var comment = Context.ForumComments.Find(editForumCommentBinding.Id);
            comment.Content = editForumCommentBinding.Content;
            Context.SaveChanges();
        }

        public void EditReply(EditReplyBindingModel editReplyBindingModel)
        {
            var reply = Context.Replies.Find(editReplyBindingModel.Id);
            reply.Content = editReplyBindingModel.Content;
            Context.SaveChanges();
        }

        public ForumCommentEditViewModel ForumComment(int id)
        {
            return Mapper.Map<ForumCommentEditViewModel>(Context.ForumComments.Find(id));
        }

        public ReplyEditViewModel Reply(int id)
        {
            return Mapper.Map<ReplyEditViewModel>(Context.Replies.Find(id));
        }

        public int TopicIdFromForumComment(int id)
        {
            return Context.ForumComments.Find(id).Topic.Id;
        }

        public bool IsUserReply(int id, string userId)
        {
            var replyAuthorId = Context.Replies.Find(id).Author.Id;
            return replyAuthorId == userId;
        }

        public int TopicIdFromReply(int id)
        {
            return Context.Replies.Find(id).Topic.Id;
        }

        public IEnumerable<ManageIndexTopicViewModel> UserTopics(string userId)
        {
            return Mapper.Map<IEnumerable<ManageIndexTopicViewModel>>(Context.Topics.Where(t => t.Author.Id == userId));
        }

        public IEnumerable<ManageIndexReplyViewModel> UserReplies(string userId)
        {
            return Mapper.Map<IEnumerable<ManageIndexReplyViewModel>>(Context.Replies.Where(t => t.Author.Id == userId));
        }

        public IEnumerable<ManageIndexForumCommentViewModel> UserForumComments(string userId)
        {
            return
                Mapper.Map<IEnumerable<ManageIndexForumCommentViewModel>>(
                    Context.ForumComments.Where(fc => fc.Author.Id == userId));
        }
    }
}
