using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using SolveMath.Models.BindingModels;
using SolveMath.Models.Entities;
using SolveMath.Models.ViewModels;
using SolveMath.Services.Contracts;

namespace SolveMath.Services
{
    public class ForumService : Service, IForumService
    {
        private const int TopicsPerPage = 10;
        public IEnumerable<TopicHeaderViewModel> GetTopics(int? categoryId)
        {
            List<Topic> topics = new List<Topic>();
            if (categoryId == null)
            {
                topics = Context.Topics.ToList();
            }
            else
            {
                var currentCategory = Context.Categories.Find(categoryId);
                Queue<Category> categories = new Queue<Category>();
                categories.Enqueue(currentCategory);
                while (categories.Count!=0)
                {
                    var category = categories.Dequeue();
                    foreach (var categorySubCategory in category.SubCategories)
                    {
                        categories.Enqueue(categorySubCategory);
                    }
                    foreach (var categoryTopic in category.Topics)
                    {
                        topics.Add(categoryTopic);
                    }
                }
            }
            return Mapper.Map<IEnumerable<Topic>, IEnumerable<TopicHeaderViewModel>>(topics);
        }

        public IEnumerable<CategoryNavbarViewModel> GetCategories()
        {
            var categories = Context.Categories;
            return Mapper.Map<IEnumerable<CategoryNavbarViewModel>>(categories);
        }

        public IEnumerable<CategoryNamesViewModel> GetCategoryNames()
        {
            return Mapper.Map<IEnumerable<CategoryNamesViewModel>>(Context.Categories);
        }

        public void CreateTopic(TopicBindingModel tbm)
        {
            Topic topic = new Topic()
            {
                Content = tbm.Content,
                Title = tbm.Title,
                PublishDate = tbm.PublishDate
            };
            Context.Topics.Add(topic);
            ApplicationUser user = Context.Users.Find(tbm.AuthorId);
            Category category = Context.Categories.First(c => c.Name == tbm.CategoryName);
            string[] tagsNames = tbm.TagsNames.Split(new string[] { ",", " ," }, StringSplitOptions.None);
            List<Tag> tags = new List<Tag>();
            foreach (var tagName in tagsNames)
            {
                if (Context.Tags.Any(x => x.Name == tagName))
                {
                    tags.Add(Context.Tags.First(x => x.Name == tagName));
                }
                else
                {
                    Tag tag = new Tag() { Name = tagName, Topics = new List<Topic>() { topic } };
                    Context.Tags.Add(tag);
                    tags.Add(tag);
                }
            }
            topic.Author = user;
            topic.Category = category;
            topic.Tags = tags;
            Context.SaveChanges();
        }

        public TopicViewModel GetTopic(int id)
        {
            var topic = Context.Topics.Find(id);
            var topicViewModel = new TopicViewModel()
            {
                Author = topic.Author,
                Category = topic.Category,
                Content = topic.Content,
                PublishDate = topic.PublishDate.Value,
                Replies = topic.Replies,
                Score = topic.UpVotes - topic.DownVotes,
                Title = topic.Title,
                Tags = topic.Tags,
                Id = topic.Id
            };
            return topicViewModel;
        }

        public void CreateAnswer(AnswerBindingModel abm, string userId)
        {
            Reply reply = new Reply()
            {
                PublishDate = DateTime.Now,
                Content = abm.Content
            };
            var user = Context.Users.Find(userId);
            reply.Author = user;
            var topic = Context.Topics.Find(abm.Id);
            topic.Replies.Add(reply);
            Context.SaveChanges();
        }

        public void CreateComment(AddForumCommentBindingModel abm, string userId)
        {
            ApplicationUser author = Context.Users.Find(userId);
            Reply parent = Context.Replies.Find(abm.Id);
            Topic topic = Context.Topics.Find(abm.TopicId);
            ForumComment comment = new ForumComment()
            {
                Content = abm.Content,
                Author = author,
                Parent = parent,
                Topic = topic,
                PublishDate = DateTime.Now
            };
            Context.ForumComments.Add(comment);
            Context.SaveChanges();
        }

        public CategoryViewModel GetCategoryViewModel(int id)
        {
            return Mapper.Map<CategoryViewModel>(Context.Categories.Find(id));
        }

        public void UpVoteTopic(VoteBindingModel model,string userId)
        {
            var user = Context.Users.Find(userId);
            var topic = Context.Topics.Find(model.Id);
            if (topic.UpVotedUsers.Contains(user))
            {
                topic.UpVotes--;
                topic.UpVotedUsers.Remove(user);
            }
            else
            {
                if (topic.DownVotedUsers.Contains(user))
                {
                    topic.DownVotedUsers.Remove(user);
                    topic.DownVotes--;
                }
                topic.UpVotedUsers.Add(user);
                topic.UpVotes++;
            }
            Context.SaveChanges();
        }

        public void DownVoteTopic(VoteBindingModel model,string userId)
        {
            var user = Context.Users.Find(userId);
            var topic = Context.Topics.Find(model.Id);
            if (topic.DownVotedUsers.Contains(user))
            {
                topic.DownVotedUsers.Remove(user);
                topic.DownVotes--;
            }
            else
            {
                if (topic.UpVotedUsers.Contains(user))
                {
                    topic.UpVotedUsers.Remove(user);
                    topic.UpVotes--;
                }
                topic.DownVotedUsers.Add(user);
                topic.DownVotes++;
            }
            Context.SaveChanges();
        }

        public void UpVoteForumComment(VoteBindingModel model,string userId)
        {

            var user = Context.Users.Find(userId);
            var forumComment = Context.ForumComments.Find(model.Id);
            if (forumComment.UpVotedUsers.Contains(user))
            {
                forumComment.UpVotedUsers.Remove(user);
                forumComment.UpVotes--;
            }
            else
            {
                if (forumComment.DownVotedUsers.Contains(user))
                {
                    forumComment.DownVotedUsers.Remove(user);
                    forumComment.DownVotes--;
                }
                forumComment.UpVotedUsers.Add(user);
                forumComment.UpVotes++;
            }
            Context.SaveChanges();
        }

        public void DownVoteForumComment(VoteBindingModel model,string userId)
        {

            var user = Context.Users.Find(userId);
            var forumComment = Context.ForumComments.Find(model.Id);
            if (forumComment.DownVotedUsers.Contains(user))
            {
                forumComment.DownVotedUsers.Remove(user);
                forumComment.DownVotes--;
            }
            else
            {
                if (forumComment.UpVotedUsers.Contains(user))
                {
                    forumComment.UpVotedUsers.Remove(user);
                    forumComment.UpVotes--;
                }
                forumComment.DownVotedUsers.Add(user);
                forumComment.DownVotes++;
            }
            Context.SaveChanges();
        }

        public void UpVoteReply(VoteBindingModel model,string userId)
        {

            var user = Context.Users.Find(userId);
            var reply = Context.Replies.Find(model.Id);
            if (reply.UpVotedUsers.Contains(user))
            {
                reply.UpVotedUsers.Remove(user);
                reply.UpVotes--;
            }
            else
            {
                if (reply.DownVotedUsers.Contains(user))
                {
                    reply.DownVotedUsers.Remove(user);
                    reply.DownVotes--;
                }
                reply.UpVotedUsers.Add(user);
                reply.UpVotes++;
            }
            Context.SaveChanges();
        }

        public void DownVoteReply(VoteBindingModel model,string userId)
        {

            var user = Context.Users.Find(userId);
            var reply = Context.Replies.Find(model.Id);
            if (reply.DownVotedUsers.Contains(user))
            {
                reply.DownVotedUsers.Remove(user);
                reply.DownVotes--;
            }
            else
            {
                if (reply.UpVotedUsers.Contains(user))
                {
                    reply.UpVotedUsers.Remove(user);
                    reply.UpVotes--;
                }
                reply.DownVotedUsers.Add(user);
                reply.DownVotes++;
            }
            Context.SaveChanges();
        }
    }
}