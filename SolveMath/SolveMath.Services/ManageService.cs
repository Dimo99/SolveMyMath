using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using AutoMapper;
using SolveMath.Data.Interfaces;
using SolveMath.Models.BindingModels;
using SolveMath.Models.Entities;
using SolveMath.Models.ViewModels;
using SolveMath.Services.Contracts;

namespace SolveMath.Services
{
    public class ManageService : Service, IManageService
    {
        public ManageService() : base()
        {

        }

        public ManageService(ISolveMathContext context) :
            base(context)
        {

        }
        public bool ValidateIsUserTopic(int id, string userId)
        {
            var topicAuthorId = Context.Topics.Find(id).Author.Id;
            return topicAuthorId == userId;
        }

        public TopicEditViewModel Topic(int id)
        {
            var topic = Context.Topics.Find(id);
            var categoryNames = Context.Categories.Select(c => c.Name);
            int Id = topic.Id;
            TopicEditViewModel topicEditViewModel = new TopicEditViewModel()
            {
                Id = Id,
                CategoryName = topic?.Category?.Name,
                CategoryNames = categoryNames,
                Content = topic.Content,
                TagNames = topic?.Tags?.Select(t => t.Name),
                Title = topic.Title
            };
            return topicEditViewModel;
        }

        public void EditTopic(EditTopicBindingModel etbm)
        {

            var topic = Context.Topics.Find(etbm.Id);
            topic.Content = etbm.Content;
            topic.Title = etbm.Title;
            if (topic.Category.Name == etbm.CategoryName)
            {
                var category = Context.Categories.First(c => c.Name == etbm.CategoryName);
                topic.Category = category;
            }
            List<Tag> tagsToDelete = new List<Tag>();
            if (etbm.OldTagNames == null)
            {
                etbm.OldTagNames = new string[] { };
            }
            foreach (var topicTag in topic.Tags)
            {
                if (!etbm.OldTagNames.Contains(topicTag.Name))
                {
                    tagsToDelete.Add(topicTag);
                }
            }
            foreach (var tag in tagsToDelete)
            {
                topic.Tags.Remove(tag);
            }
            if (etbm.TagNames != null)
            {
                string[] tagNames = etbm.TagNames.Split(new string[] { ",", " ," }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var tagName in tagNames)
                {
                    var tag = Context.Tags.FirstOrDefault(t => t.Name == tagName);
                    if (tag == null)
                    {
                        topic.Tags.Add(new Tag()
                        {
                            Name = tagName
                        });
                    }
                    else
                    {
                        topic.Tags.Add(tag);
                    }
                }
            }

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

        public DeleteTopicViewModel DeleteTopicViewModel(int id)
        {
            return Mapper.Map<DeleteTopicViewModel>(Context.Topics.Find(id));
        }

        public void DeleteTopic(DeleteTopicBindingModel deleteTopicBindingModel)
        {
            var topic = Context.Topics.Find(deleteTopicBindingModel.Id);
            var replies = topic.Replies.ToList();
            for (var i = 0; i < replies.Count; i++)
            {
                topic.Replies.Remove(replies[i]);
                this.DeleteReply(new DeleteReplyBindingModel() { Id = replies[i].Id });
            }
            Context.Topics.Remove(topic);
            Context.SaveChanges();
        }

        public DeleteForumCommentViewModel DeleteForumCommentViewModel(int id)
        {
            return Mapper.Map<DeleteForumCommentViewModel>(Context.ForumComments.Find(id));
        }

        public void DeleteForumComment(DeleteForumCommentBindingModel deleteForumCommentBindingModel)
        {
            var forumComment = Context.ForumComments.Find(deleteForumCommentBindingModel.Id);
            Context.ForumComments.Remove(forumComment);
            Context.SaveChanges();
        }

        public DeleteReplyViewModel DeleteReplyViewModel(int id)
        {
            return Mapper.Map<DeleteReplyViewModel>(Context.Replies.Find(id));
        }

        public void DeleteReply(DeleteReplyBindingModel bindingModel)
        {
            var deleteReply = Context.Replies.Find(bindingModel.Id);
            var forumComments = deleteReply.ForumComments.ToList();
            for (var i = 0; i < forumComments.Count; i++)
            {
                deleteReply.ForumComments.Remove(forumComments[i]);
                this.DeleteForumComment(new DeleteForumCommentBindingModel() {Id= forumComments[i].Id});
            }
            Context.Replies.Remove(deleteReply);
            Context.SaveChanges();
        }
    }
}
