using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PagedList;
using SolveMath.Areas.Forum.Controllers;
using SolveMath.Data.Interfaces;
using SolveMath.Data.Mocks;
using SolveMath.Models.BindingModels;
using SolveMath.Models.Entities;
using SolveMath.Models.ViewModels;
using SolveMath.Services;
using SolveMath.Services.Contracts;
using TestStack.FluentMVCTesting;
namespace SolveMath.Tests.Controllers
{
    [TestClass]
    public class ForumControllerTest
    {
        private ForumController _forumController;
        private IForumService _service;
        private ISolveMathContext _context;
        private List<Category> categories;
        private List<Topic> topics;
        private List<Reply> replies;
        private List<ForumComment> forumComments;
        private List<ApplicationUser> users;
        private List<IdentityRole> roles;
        private void ConfigureMapper()
        {
            Mapper.Initialize(expression =>
            {
                expression.CreateMap<Topic, TopicHeaderViewModel>().ForMember(vm => vm.Score, configurationExpression =>
                        configurationExpression.MapFrom(topic => (topic.UpVotes - topic.DownVotes)));
                expression.CreateMap<Category, CategoryNavbarViewModel>();
                expression.CreateMap<Category, CategoryViewModel>();
            });
        }
        [TestInitialize]
        public void Init()
        {
            this.ConfigureMapper();
            users = new List<ApplicationUser>()
            {
                new ApplicationUser()
                {
                    Id = "123",
                    Email = "dimodimov1999@gmail.com",
                    UserName = "dimodimov1999@gmail.com",
                    
                }
            };
            categories = new List<Category>()
            {
                new Category()
                {
                    Id = 0,
                    Name = "Математика 1кл."
                },
                new Category()
                {
                    Id = 1,
                    Name = "Математика 2кл."
                }
            };
            topics = new List<Topic>()
            {
                new Topic()
                {
                    Id = 0,
                    Category = categories[0],
                    Content = "Търся добър сборник за 1кл. за деца напреднали с материала",
                    Title = "Сборник 1кл.",
                    PublishDate = new DateTime(2012,10,10)
                },
                new Topic()
                {
                    Id = 1,
                    Category = categories[1],
                    Content = "Търся добър сборник за 2кл. за деца напреднали с материала",
                    Title = "Сборник 2кл.",
                    PublishDate = new DateTime(2013,10,15)
                }
            };
            categories[0].Topics.Add(topics[0]);
            categories[1].Topics.Add(topics[1]);
            replies = new List<Reply>()
            {
                new Reply()
                {
                    Id = 0,
                    Content = "Препоръчвам този на анубис струва 10лв и го има в почти всички книжарници.Сина ми го реши когато беше първи клас тази година е 5 клас и взе бронзов медал от национална олимпиада",
                    UpVotes = 10,
                    DownVotes = 3,
                    PublishDate = new DateTime(2012,10,11),
                    Topic = topics[0]
                },
                new Reply()
                {
                    Id = 1,
                    Content = "Според мен на пазара няма добри сборници за способни деца 2кл.Препоръчвам ти да купиш такива за 3 и 4 и да решава от тях.",
                    UpVotes = 3,
                    DownVotes = 2,
                    PublishDate = new DateTime(2014,1,1),
                    Topic = topics[1]
                }
            };
            topics[0].Replies.Add(replies[0]);
            topics[1].Replies.Add(replies[1]);
            forumComments = new List<ForumComment>()
            {
                new ForumComment()
                {
                    Id = 0,
                    Content = "Невероятен сборник!!!!",
                    PublishDate = new DateTime(2012,10,12),
                    Topic = topics[0],
                    Parent = replies[0]
                },
                new ForumComment()
                {
                    Id = 1,
                    Content = "Мисля, че това е прекалено",
                    PublishDate = new DateTime(2014,2,3),
                    Topic = topics[1],
                    Parent = replies[1]
                }
            };
            replies[0].ForumComments.Add(forumComments[0]);
            replies[1].ForumComments.Add(forumComments[1]);
            this._context = new FakeSolveMathContext();
            foreach (var category in categories)
            {
                _context.Categories.Add(category);
            }
            foreach (var topic in topics)
            {
                _context.Topics.Add(topic);
            }
            foreach (var reply in replies)
            {
                _context.Replies.Add(reply);
            }
            foreach (var forumComment in forumComments)
            {
                _context.ForumComments.Add(forumComment);
            }
            foreach (var applicationUser in users)
            {
                _context.Users.Add(applicationUser);
            }
            this._service = new ForumService(this._context);
            this._forumController = new ForumController(_service);
        }

        [TestMethod]
        public void Index_ReturnsDefaultView()
        {
            _forumController.WithCallTo(forumController =>
                forumController.Index(null)).ShouldRenderDefaultView();
        }

        [TestMethod]
        public void Index_ReturnsCorectViewModel()
        {
            _forumController.WithCallTo(forrumController => forrumController.Index(null))
                .ShouldRenderDefaultView()
                .WithModel<ForumIndexViewModel>(m => Assert.AreEqual(null, m.Page));
            _forumController.WithCallTo(forrumController => forrumController.Index(1))
                .ShouldRenderDefaultView()
                .WithModel<ForumIndexViewModel>(m => Assert.AreEqual(1, m.Page));
            _forumController.WithCallTo(forrumController => forrumController.Index(2))
                .ShouldRenderDefaultView()
                .WithModel<ForumIndexViewModel>(m => Assert.AreEqual(2, m.Page));
        }
        [TestMethod]
        public void Topic_ReturnsCorrectPartialView()
        {
            _forumController.WithCallTo(forumController =>
                forumController.Topics(null, null)).ShouldRenderPartialView("_Topics");
        }

        [TestMethod]
        public void Topic_ReturnsTheCorrectViewModelType()
        {
            _forumController.WithCallTo(forumController =>
                forumController.Topics(null, null)).ShouldRenderPartialView("_Topics").WithModel<IPagedList<TopicHeaderViewModel>>();
        }

        [TestMethod]
        public void Topic_ReturnsTheCorrectViewModel()
        {
            IPagedList<TopicHeaderViewModel> expectedModel = _service.GetTopics(null).ToPagedList(1, 10);
            _forumController.WithCallTo(forumController =>
                forumController.Topics(null, null)).ShouldRenderPartialView("_Topics").WithModel<IPagedList<TopicHeaderViewModel>>(m => Assert.AreEqual(expectedModel.Count, m.Count));
        }

        [TestMethod]
        public void Categories_ReturnsCorrectPartialView()
        {
            _forumController.WithCallTo(forumController => forumController.Categories())
                .ShouldRenderPartialView("_Categories");
        }

        [TestMethod]
        public void Categories_ReturnsCorrectViewModel()
        {
            IEnumerable<CategoryNavbarViewModel> exprectedModel = _service.GetCategories();
            _forumController.WithCallTo(forumController => forumController.Categories())
                .ShouldRenderPartialView("_Categories")
                .WithModel<IEnumerable<CategoryNavbarViewModel>>(m => Assert.AreEqual(exprectedModel.Count(), m.Count()));
            _forumController.WithCallTo(forumController => forumController.Categories())
                .ShouldRenderPartialView("_Categories")
                .WithModel<IEnumerable<CategoryNavbarViewModel>>(m => Assert.AreEqual(exprectedModel.First().Name, m.First().Name));
            _forumController.WithCallTo(forumController => forumController.Categories())
                .ShouldRenderPartialView("_Categories")
                .WithModel<IEnumerable<CategoryNavbarViewModel>>(m => Assert.AreEqual(exprectedModel.Last().Name, m.Last().Name));
        }

        [TestMethod]
        public void Category_ReturnsCorrectView()
        {
            _forumController.WithCallTo(forumController =>
                forumController.Category(0, null)).ShouldRenderDefaultView();
        }

        [TestMethod]
        public void Category_ReturnsCorrectViewModel()
        {
            CategoryViewModel expectedModel = _service.GetCategoryViewModel(0);
            _forumController.WithCallTo(forumController =>
                forumController.Category(0, null))
                .ShouldRenderDefaultView()
                .WithModel<CategoryViewModel>(m =>
                Assert.AreEqual(expectedModel.Name, m.Name));
            expectedModel = _service.GetCategoryViewModel(1);
            _forumController.WithCallTo(forumContrller =>
                    forumContrller.Category(1, null))
                .ShouldRenderDefaultView()
                .WithModel<CategoryViewModel>(m => Assert.AreEqual(expectedModel.Id, m.Id));
        }

        [TestMethod]
        public void UpVoteTopic_WorksAsExpected()
        {
            _forumController.GetUserId = () => "123";
            var before = _service.GetTopic(0).Score;
            VoteBindingModel model = new VoteBindingModel() { Id = 0 };
            _forumController.WithCallTo(forumController =>
                forumController.UpVoteTopic(model)).ShouldRedirectToRoute("");
            var after = _service.GetTopic(0).Score;
            Assert.AreEqual(after-before,1);
        }
        [TestMethod]
        public void UpVoteTopic_UpVoteTopicTwiceNotChangeVotes()
        {
            _forumController.GetUserId = () => "123";
            var before = _service.GetTopic(0).Score;
            VoteBindingModel model = new VoteBindingModel() { Id = 0 };
            _forumController.WithCallTo(forumController =>
                forumController.UpVoteTopic(model)).ShouldRedirectToRoute("");
            _forumController.WithCallTo(forumController =>
                forumController.UpVoteTopic(model)).ShouldRedirectToRoute("");
            var after = _service.GetTopic(0).Score;
            Assert.AreEqual(after - before, 0);
        }
        [TestMethod]
        public void DownVoteTopic_WorksAsExpected()
        {
            _forumController.GetUserId = () => "123";
            var before = _service.GetTopic(0).Score;
            VoteBindingModel model = new VoteBindingModel() { Id = 0 };
            _forumController.WithCallTo(forumController =>
                forumController.DownVoteTopic(model)).ShouldRedirectToRoute("");
            var after = _service.GetTopic(0).Score;
            Assert.AreEqual(before-after,1);
        }
        [TestMethod]
        public void DownVoteTopic_DownVoteTopicTwiceNotChangeVotes()
        {
            _forumController.GetUserId = () => "123";
            var before = _service.GetTopic(0).Score;
            VoteBindingModel model = new VoteBindingModel() { Id = 0 };
            _forumController.WithCallTo(forumController =>
                forumController.DownVoteTopic(model)).ShouldRedirectToRoute("");
            _forumController.WithCallTo(forumController =>
                forumController.DownVoteTopic(model)).ShouldRedirectToRoute("");
            var after = _service.GetTopic(0).Score;
            Assert.AreEqual(before - after,0);
        }
        [TestMethod]
        public void UpVoteReply_WorksAsExpected()
        {
            _forumController.GetUserId = () => "123";
            var before = _service.GetTopic(0).Replies.First().UpVotes;
            VoteBindingModel model = new VoteBindingModel() { Id = 0 };
            _forumController.WithCallTo(forumController =>
                forumController.UpVoteReply(model)).ShouldRedirectToRoute("");
            var after = _service.GetTopic(0).Replies.First().UpVotes;
            Assert.AreEqual(after-before, 1);
        }
        [TestMethod]
        public void UpVoteReply_UpVoteReplyTwiceNotChangeVotes()
        {
            _forumController.GetUserId = () => "123";
            var before = _service.GetTopic(0).Replies.First().UpVotes;
            VoteBindingModel model = new VoteBindingModel() { Id = 0 };
            _forumController.WithCallTo(forumController =>
                forumController.UpVoteReply(model)).ShouldRedirectToRoute("");
            _forumController.WithCallTo(forumController =>
                forumController.UpVoteReply(model)).ShouldRedirectToRoute("");
            var after = _service.GetTopic(0).Replies.First().UpVotes;
            Assert.AreEqual(after - before, 0);
        }
        [TestMethod]
        public void DownVoteReply_WorksAsExpected()
        {
            _forumController.GetUserId = () => "123";
            var before = _service.GetTopic(0).Replies.First().DownVotes;
            VoteBindingModel model = new VoteBindingModel() { Id = 0 };
            _forumController.WithCallTo(forumController =>
                forumController.DownVoteReply(model)).ShouldRedirectToRoute("");
            var after = _service.GetTopic(0).Replies.First().DownVotes;
            Assert.AreEqual(after - before, 1);
        }
        [TestMethod]
        public void DownVoteReply_DownVoteReplyTwiceNotChangeVotes()
        {
            _forumController.GetUserId = () => "123";
            var before = _service.GetTopic(0).Replies.First().DownVotes;
            VoteBindingModel model = new VoteBindingModel() { Id = 0 };
            _forumController.WithCallTo(forumController =>
                forumController.DownVoteReply(model)).ShouldRedirectToRoute("");
            _forumController.WithCallTo(forumController =>
                forumController.DownVoteReply(model)).ShouldRedirectToRoute("");
            var after = _service.GetTopic(0).Replies.First().DownVotes;
            Assert.AreEqual(after - before, 0);
        }

        [TestMethod]
        public void UpVoteForumComment_WorksAsExpected()
        {
            _forumController.GetUserId = () => "123";
            var before = _service.GetTopic(0).Replies.First().ForumComments.First().UpVotes;
            VoteBindingModel model = new VoteBindingModel() { Id = 0 };
            _forumController.WithCallTo(forumController =>
                forumController.UpVoteForumComment(model)).ShouldRedirectToRoute("");
            var after = _service.GetTopic(0).Replies.First().ForumComments.First().UpVotes;
            Assert.AreEqual(after - before, 1);
        }
        [TestMethod]
        public void UpVoteForumComment_UpVoteForumCommentTwiceNotChangeVotes()
        {
            _forumController.GetUserId = () => "123";
            var before = _service.GetTopic(0).Replies.First().ForumComments.First().UpVotes;
            VoteBindingModel model = new VoteBindingModel() { Id = 0 };
            _forumController.WithCallTo(forumController =>
                forumController.UpVoteForumComment(model)).ShouldRedirectToRoute("");
            _forumController.WithCallTo(forumController =>
                forumController.UpVoteForumComment(model)).ShouldRedirectToRoute("");
            var after = _service.GetTopic(0).Replies.First().ForumComments.First().UpVotes;
            Assert.AreEqual(after - before, 0);
        }
        [TestMethod]
        public void DownVoteForumComment_WorksAsExpected()
        {
            _forumController.GetUserId = () => "123";
            var before = _service.GetTopic(0).Replies.First().ForumComments.First().DownVotes;
            VoteBindingModel model = new VoteBindingModel() { Id = 0 };
            _forumController.WithCallTo(forumController =>
                forumController.DownVoteForumComment(model)).ShouldRedirectToRoute("");
            var after = _service.GetTopic(0).Replies.First().ForumComments.First().DownVotes;
            Assert.AreEqual(after - before, 1);
        }
        [TestMethod]
        public void DownVoteForumComment_DownVoteForumCommentTwiceNotChangeVotes()
        {
            _forumController.GetUserId = () => "123";
            var before = _service.GetTopic(0).Replies.First().ForumComments.First().DownVotes;
            VoteBindingModel model = new VoteBindingModel() { Id = 0 };
            _forumController.WithCallTo(forumController =>
                forumController.DownVoteForumComment(model)).ShouldRedirectToRoute("");
            _forumController.WithCallTo(forumController =>
                forumController.DownVoteForumComment(model)).ShouldRedirectToRoute("");
            var after = _service.GetTopic(0).Replies.First().ForumComments.First().DownVotes;
            Assert.AreEqual(after - before, 0);
        }
    }
}
