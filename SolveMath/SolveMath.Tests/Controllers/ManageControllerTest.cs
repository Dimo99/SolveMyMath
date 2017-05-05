using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SolveMath.Controllers;
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
    public class ManageControllerTest
    {
        private ManageController _controller;
        private IManageService _service;
        private ISolveMathContext _context;
        private List<Topic> topics;
        private List<Reply> replies;
        private List<ForumComment> forumComments;
        private List<ApplicationUser> users;

        private void ConfigureMapper()
        {
            Mapper.Initialize(expression =>
            {
                expression.CreateMap<Topic, DeleteTopicViewModel>();
                expression.CreateMap<ForumComment, DeleteForumCommentViewModel>();
                expression.CreateMap<Reply, DeleteReplyViewModel>();
                expression.CreateMap<Reply, ReplyEditViewModel>();
                expression.CreateMap<ForumComment, ForumCommentEditViewModel>();
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
                    UserName = "dimodimov1999@gmail.com"
                },
                new ApplicationUser()
                {
                    Id = "1234",
                    Email = "dimodimov1999@gmail.com",
                    UserName = "dimodimov1999@gmail.com"
                }
            };
            topics = new List<Topic>()
            {
                new Topic()
                {
                    Id = 0,
                    Author = users[0],
                    Content = "Търся добър сборник за 1кл. за деца напреднали с материала",
                    Title = "Сборник 1кл.",
                    PublishDate = new DateTime(2012,10,10)
                },
                new Topic()
                {
                    Id = 1,
                    Author = users[0],
                    Content = "Търся добър сборник за 2кл. за деца напреднали с материала",
                    Title = "Сборник 2кл.",
                    PublishDate = new DateTime(2013,10,15)
                }
            };
            users[0].Topics.Add(topics[0]);
            users[0].Topics.Add(topics[1]);
            replies = new List<Reply>()
            {
                new Reply()
                {
                    Id = 0,
                    Content = "Препоръчвам този на анубис струва 10лв и го има в почти всички книжарници." +
                              "Сина ми го реши когато беше първи клас тази година е 5 клас и взе бронзов медал" +
                              " от национална олимпиада",
                    UpVotes = 10,
                    DownVotes = 3,
                    PublishDate = new DateTime(2012,10,11),
                    Topic = topics[0],
                    Author = users[0]
                },
                new Reply()
                {
                    Id = 1,
                    Content = "Според мен на пазара няма добри сборници за способни деца 2кл." +
                              "Препоръчвам ти да купиш такива за 3 и 4 и да решава от тях.",
                    UpVotes = 3,
                    DownVotes = 2,
                    PublishDate = new DateTime(2014,1,1),
                    Topic = topics[1],
                    Author = users[0]
                }
            };
            users[0].Replies.Add(replies[0]);
            users[0].Replies.Add(replies[1]);
            forumComments = new List<ForumComment>()
            {
                new ForumComment()
                {
                    Id = 0,
                    Content = "Невероятен сборник!!!!",
                    PublishDate = new DateTime(2012,10,12),
                    Topic = topics[0],
                    Parent = replies[0],
                    Author = users[0]
                },
                new ForumComment()
                {
                    Id = 1,
                    Content = "Мисля, че това е прекалено",
                    PublishDate = new DateTime(2014,2,3),
                    Topic = topics[1],
                    Author = users[0],
                    Parent = replies[1]
                }
            };
            users[0].ForumComments.Add(forumComments[0]);
            users[0].ForumComments.Add(forumComments[1]);
            this._context = new FakeSolveMathContext();
            foreach (var applicationUser in users)
            {
                _context.Users.Add(applicationUser);
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
            this._service = new ManageService(this._context);
            _controller = new ManageController(this._service);

        }

        [TestMethod]
        public void DeleteTopic_ReturnsDefaultView()
        {
            var controllerContextMock = new Mock<ControllerContext>();
            controllerContextMock.Setup(x => x.HttpContext.User.IsInRole(It.Is<string>(s => s.Equals("admin"))))
                .Returns(false);
            _controller.ControllerContext = controllerContextMock.Object;
            _controller.GetUserId = () => "123";
            _controller.WithCallTo(controller => controller.DeleteTopic(0)).ShouldRenderDefaultView();
        }

        [TestMethod]
        public void DeleteTopic_ReturnsCorrectVM()
        {
            var controllerContextMock = new Mock<ControllerContext>();
            controllerContextMock.Setup(x => x.HttpContext.User.IsInRole(It.Is<string>(s => s.Equals("admin"))))
                .Returns(false);
            _controller.ControllerContext = controllerContextMock.Object;
            _controller.GetUserId = () => "123";
            DeleteTopicViewModel expectedModel = _service.DeleteTopicViewModel(0);
            _controller.WithCallTo(controller => controller.DeleteTopic(0)).ShouldRenderDefaultView()
                .WithModel<DeleteTopicViewModel>
                (m=>Assert.AreEqual(expectedModel.Id,m.Id));
        }
        [ExpectedException(typeof(InvalidOperationException))]
        [TestMethod]
        public void DeleteTopic_AsNonAuthor()
        {
            var controllerContextMock = new Mock<ControllerContext>();
            controllerContextMock.Setup(x => x.HttpContext.User.IsInRole(It.Is<string>(s => s.Equals("admin"))))
                .Returns(false);
            _controller.ControllerContext = controllerContextMock.Object;
            _controller.GetUserId = () => "1234";
            _controller.WithCallTo(controller => controller.DeleteTopic(0));
        }
        [TestMethod]
        public void DeleteTopic_AsAdmin()
        {
            var controllerContextMock = new Mock<ControllerContext>();
            controllerContextMock.Setup(x => x.HttpContext.User.IsInRole(It.Is<string>(s => s.Equals("Admin"))))
                .Returns(true);
            _controller.ControllerContext = controllerContextMock.Object;
            _controller.GetUserId = () => "1234";
            _controller.WithCallTo(controller => controller.DeleteTopic(new DeleteTopicBindingModel() {Id = 0}))
                .ShouldRedirectToRoute("");
            foreach (var contextTopic in _context.Topics)
            {
                Assert.AreNotSame(0,contextTopic.Id);
            }
        }
        [TestMethod]
        public void DeleteTopic_AsModerator()
        {
            var controllerContextMock = new Mock<ControllerContext>();
            controllerContextMock.Setup(x => x.HttpContext.User.IsInRole(It.Is<string>(s => s.Equals("Moderator"))))
                .Returns(true);
            _controller.ControllerContext = controllerContextMock.Object;
            _controller.GetUserId = () => "1234";
            _controller.WithCallTo(controller => controller.DeleteTopic(new DeleteTopicBindingModel() { Id = 0 }))
                .ShouldRedirectToRoute("");
            foreach (var contextTopic in _context.Topics)
            {
                Assert.AreNotSame(0, contextTopic.Id);
            }
        }
        [ExpectedException(typeof(InvalidOperationException))]
        [TestMethod]
        public void DeleteForumComment_AsNonAuthor()
        {
            var controllerContextMock = new Mock<ControllerContext>();
            controllerContextMock.Setup(x => x.HttpContext.User.IsInRole(It.Is<string>(s => s.Equals("Moderator"))))
                .Returns(false);
            _controller.ControllerContext = controllerContextMock.Object;
            _controller.GetUserId = () => "1234";
            _controller.WithCallTo(controller => controller.DeleteForumComment(0));
        }

        [TestMethod]
        public void DeleteForumComment_ReturnsCorrectView()
        {
            var controllerContextMock = new Mock<ControllerContext>();
            controllerContextMock.Setup(x => x.HttpContext.User.IsInRole(It.Is<string>(s => s.Equals("Moderator"))))
                .Returns(false);
            _controller.ControllerContext = controllerContextMock.Object;
            _controller.GetUserId = () => "123";
            _controller.WithCallTo(controller => controller.DeleteForumComment(0)).ShouldRenderDefaultView();
        }

        [TestMethod]
        public void DeleteForumComment_AsAdmin()
        {
            var controllerContextMock = new Mock<ControllerContext>();
            controllerContextMock.Setup(x => x.HttpContext.User.IsInRole(It.Is<string>(s => s.Equals("Admin"))))
                .Returns(true);
            _controller.ControllerContext = controllerContextMock.Object;
            _controller.GetUserId = () => "1234";
            _controller.WithCallTo(controller => controller.DeleteForumComment(new DeleteForumCommentBindingModel() {Id = 0}));
            foreach (var contextForumComment in _context.ForumComments)
            {
                Assert.AreNotEqual(contextForumComment.Id,0);
            }
        }

        [TestMethod]
        public void DeleteForumComment_AsModerator()
        {
            var controllerContextMock = new Mock<ControllerContext>();
            controllerContextMock.Setup(x => x.HttpContext.User.IsInRole(It.Is<string>(s => s.Equals("Moderator"))))
                .Returns(true);
            _controller.ControllerContext = controllerContextMock.Object;
            _controller.GetUserId = () => "1234";
            _controller.WithCallTo(controller => controller.DeleteForumComment(new DeleteForumCommentBindingModel() { Id = 0 }));
            foreach (var contextForumComment in _context.ForumComments)
            {
                Assert.AreNotEqual(contextForumComment.Id, 0);
            }
        }
        [ExpectedException(typeof(InvalidOperationException))]
        [TestMethod]
        public void DeleteReply_AsNonAuthor()
        {
            var controllerContextMock = new Mock<ControllerContext>();
            controllerContextMock.Setup(x => x.HttpContext.User.IsInRole(It.Is<string>(s => s.Equals("Moderator"))))
                .Returns(false);
            _controller.ControllerContext = controllerContextMock.Object;
            _controller.GetUserId = () => "1234";
            _controller.WithCallTo(controller => controller.DeleteReply(0));
        }

        [TestMethod]
        public void DeleteReply_ReturnsCorrectVM()
        {
            var controllerContextMock = new Mock<ControllerContext>();
            controllerContextMock.Setup(x => x.HttpContext.User.IsInRole(It.Is<string>(s => s.Equals("Moderator"))))
                .Returns(false);
            _controller.ControllerContext = controllerContextMock.Object;
            _controller.GetUserId = () => "123";
            DeleteReplyViewModel expectedModel = _service.DeleteReplyViewModel(0);
            _controller.WithCallTo(controller => controller.DeleteReply(0)).ShouldRenderDefaultView()
                .WithModel<DeleteReplyViewModel>(m=>Assert.AreEqual(m.Id,expectedModel.Id));
        }

        [TestMethod]
        public void DeleteReply_AsAdmin()
        {

            var controllerContextMock = new Mock<ControllerContext>();
            controllerContextMock.Setup(x => x.HttpContext.User.IsInRole(It.Is<string>(s => s.Equals("Admin"))))
                .Returns(true);
            _controller.ControllerContext = controllerContextMock.Object;
            _controller.GetUserId = () => "1234";
            _controller.WithCallTo(controller => controller.DeleteReply(new DeleteReplyBindingModel() {Id = 0}));
            _controller.WithCallTo(controller => controller.DeleteReply(new DeleteReplyBindingModel() { Id = 1 }));
            Assert.AreEqual(_context.Replies.Count(),0);
        }

        [TestMethod]
        public void DeleteReply_AsModerator()
        {
            var controllerContextMock = new Mock<ControllerContext>();
            controllerContextMock.Setup(x => x.HttpContext.User.IsInRole(It.Is<string>(s => s.Equals("Moderator"))))
                .Returns(true);
            _controller.ControllerContext = controllerContextMock.Object;
            _controller.GetUserId = () => "1231234";
            _controller.WithCallTo(controller => controller.DeleteReply(new DeleteReplyBindingModel() { Id = 0 }));
            _controller.WithCallTo(controller => controller.DeleteReply(new DeleteReplyBindingModel() { Id = 1 }));
            Assert.AreEqual(_context.Replies.Count(), 0);
        }

        [TestMethod]
        public void EditTopic_ReturnsCorrectVM()
        {
            var controllerContextMock = new Mock<ControllerContext>();
            controllerContextMock.Setup(x => x.HttpContext.User.IsInRole(It.Is<string>(s => s.Equals("Moderator"))))
                .Returns(false);
            _controller.ControllerContext = controllerContextMock.Object;
            _controller.GetUserId = () => "123";
            TopicEditViewModel topicEditViewModel = _service.Topic(0);
            _controller.WithCallTo(conroller => conroller.EditTopic(0)).ShouldRenderDefaultView()
                .WithModel<TopicEditViewModel>(m=>Assert.AreEqual(m.Id,topicEditViewModel.Id));
        }
        [ExpectedException(typeof(InvalidOperationException))]
        [TestMethod]
        public void EditTopic_AsNonAuthor()
        {
            var controllerContextMock = new Mock<ControllerContext>();
            controllerContextMock.Setup(x => x.HttpContext.User.IsInRole(It.Is<string>(s => s.Equals("Moderator"))))
                .Returns(false);
            _controller.ControllerContext = controllerContextMock.Object;
            _controller.GetUserId = () => "1234";
            _controller.WithCallTo(conroller => conroller.EditTopic(0));
        }
        [ExpectedException(typeof(InvalidOperationException))]
        [TestMethod]
        public void EditReply_AsNonAuthor()
        {
            var controllerContextMock = new Mock<ControllerContext>();
            controllerContextMock.Setup(x => x.HttpContext.User.IsInRole(It.Is<string>(s => s.Equals("Moderator"))))
                .Returns(false);
            _controller.ControllerContext = controllerContextMock.Object;
            _controller.GetUserId = () => "1234";
            _controller.WithCallTo(controller => controller.EditReply(0));
        }

        [TestMethod]
        public void EditReply_ReturnsCorrectVM()
        {
            var controllerContextMock = new Mock<ControllerContext>();
            controllerContextMock.Setup(x => x.HttpContext.User.IsInRole(It.Is<string>(s => s.Equals("Moderator"))))
                .Returns(false);
            _controller.ControllerContext = controllerContextMock.Object;
            _controller.GetUserId = () => "123";
            ReplyEditViewModel expectedModel = _service.Reply(0);
            _controller.WithCallTo(controller => controller.EditReply(0))
                .ShouldRenderDefaultView()
                .WithModel<ReplyEditViewModel>(m => Assert.AreEqual(expectedModel.Id, m.Id));
        }

        [ExpectedException(typeof(InvalidOperationException))]
        [TestMethod]
        public void EditForumComment_AsNonAuthor()
        {
            var controllerContextMock = new Mock<ControllerContext>();
            controllerContextMock.Setup(x => x.HttpContext.User.IsInRole(It.Is<string>(s => s.Equals("Moderator"))))
                .Returns(false);
            _controller.ControllerContext = controllerContextMock.Object;
            _controller.GetUserId = () => "1234";
            _controller.WithCallTo(controller => controller.EditForumComment(0));
        }

        [TestMethod]
        public void EditForumComment_ReturnsCorrectVM()
        {
            var controllerContextMock = new Mock<ControllerContext>();
            controllerContextMock.Setup(x => x.HttpContext.User.IsInRole(It.Is<string>(s => s.Equals("Moderator"))))
                .Returns(false);
            _controller.ControllerContext = controllerContextMock.Object;
            _controller.GetUserId = () => "123";
            ForumCommentEditViewModel expectedModel = _service.ForumComment(0);
            _controller.WithCallTo(controller => controller.EditForumComment(0))
                .ShouldRenderDefaultView()
                .WithModel<ForumCommentEditViewModel>(m => Assert.AreEqual(expectedModel.Id, m.Id));
        }
    }
}