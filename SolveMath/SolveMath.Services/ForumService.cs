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
    public class ForumService : Service,IForumService
    {
        private const int TopicsPerPage = 10;
        public IEnumerable<TopicHeaderViewModel> GetTopicsForPage(int? page)
        {
            if (page == null||page <= 1)
            {
                page = 0;
            }
            int pageNotNull = (int)page;
            IEnumerable<Topic> topics = Context.Topics;
            topics = topics.Skip(pageNotNull * TopicsPerPage).Take(TopicsPerPage);
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
            string[] tagsNames = tbm.TagsNames.Split(new string[] {",", " ,"}, StringSplitOptions.None);
            List<Tag> tags = new List<Tag>();
            foreach (var tagName in tagsNames)
            {
                if (Context.Tags.Any(x => x.Name == tagName))
                {
                    tags.Add(Context.Tags.First(x=>x.Name==tagName));
                }
                else
                {
                    Tag tag = new Tag() {Name = tagName,Topics = new List<Topic>() {topic} };
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

        public void CreateAnswer(AnswerBindingModel abm,string userId)
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
    }
}