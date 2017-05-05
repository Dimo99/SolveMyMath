using System;
using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using SolveMath.Models.BindingModels;
using SolveMath.Models.ViewModels;
using SolveMath.Services;
using SolveMath.Services.Contracts;
using PagedList;
using PagedList.Mvc;
using SolveMath.Models.Entities;

namespace SolveMath.Areas.Forum.Controllers
{
    public class ForumController : Controller
    {
        private IForumService service;
        public Func<string> GetUserId;//For testing
        public ForumController(IForumService service)
        {
            GetUserId = () => User.Identity.GetUserId();
            this.service = service;
        }
        [Authorize(Roles = "User")]
        public ActionResult Post()
        {
            var model = service.GetCategoryNames();
            return this.View(model);
        }
        [HttpPost]
        [Authorize(Roles = "User")]
        public ActionResult Post(TopicBindingModel tbm)
        {
            if (ModelState.IsValid)
            {
                tbm.AuthorId = User.Identity.GetUserId();
                tbm.PublishDate = DateTime.Now;

                service.CreateTopic(tbm);
                return this.RedirectToAction("Index");
            }
            var model = service.GetCategoryNames();
            return this.View(model);
        }
        [Authorize(Roles = "User")]
        public ActionResult AddAnswer(int id)
        {
            AddingAnswerViewModel aav = new AddingAnswerViewModel() {Id = id};
            return this.View(aav);
        }
        [Authorize(Roles = "User")]
        [HttpPost]
        public ActionResult AddAnswer(AnswerBindingModel abm)
        {
            if (ModelState.IsValid)
            {
                string userId = User.Identity.GetUserId();
                service.CreateAnswer(abm, userId);
                return this.RedirectToAction("Topic", new {id= abm.Id});
            }
            return this.View(new AddingAnswerViewModel {Id = abm.Id});
        }
        [Authorize(Roles = "User")]
        public ActionResult AddComment(int id,int topicId)
        {
            AddCommentViewModel acvm = new AddCommentViewModel() {Id = id,TopicId = topicId};
            return View(acvm);
        }
        [Authorize(Roles = "User")]
        [HttpPost]
        public ActionResult AddComment(AddForumCommentBindingModel acbm)
        {
            if (ModelState.IsValid)
            {
                service.CreateComment(acbm, User.Identity.GetUserId());
                return this.RedirectToAction("Topic", new {id = acbm.TopicId});
            }
            AddCommentViewModel acvm = new AddCommentViewModel() {Id = acbm.Id,TopicId = acbm.TopicId};
            return this.View(acvm);
        }
        public ActionResult Topic(int id)
        {
            
            var model = service.GetTopic(id);
            return this.View(model);
        }

        public ActionResult TopicPartialView(int id)
        {
            var model = service.GetTopic(id);
            return this.PartialView("_TopicPartialView", model);
        }
        // GET: Forum/Forum
        public ActionResult Index(int? page)
        {
            ForumIndexViewModel forumIndexViewModel = new ForumIndexViewModel() {Page = page};
            return this.View(forumIndexViewModel);
        }

        public ActionResult Topics(int? page,int? categoryId)
        {
            var pageTopics = service.GetTopics(categoryId);
            var pagedList = pageTopics.ToPagedList(page ?? 1, 10);
            return this.PartialView("_Topics", pagedList);
        }
        public ActionResult Category(int id,int? page)
        {
            CategoryViewModel categoryViewModel = service.GetCategoryViewModel(id);
            return this.View(categoryViewModel);
        }
        public ActionResult Categories()
        {
            var categories = service.GetCategories();
            return this.PartialView("_Categories", categories);
        }
        [HttpPost]
        [Authorize(Roles = "User")]
        public ActionResult UpVoteTopic(VoteBindingModel model)
        {
            service.UpVoteTopic(model,GetUserId());
            return this.RedirectToAction("Index");
        }
        [HttpPost]
        [Authorize(Roles = "User")]
        public ActionResult DownVoteTopic(VoteBindingModel model)
        {
            service.DownVoteTopic(model,GetUserId());
            return this.RedirectToAction("Index");
        }
        [HttpPost]
        [Authorize(Roles = "User")]
        public ActionResult UpVoteReply(VoteBindingModel model)
        {
            service.UpVoteReply(model,GetUserId());
            return this.RedirectToAction("Index");
        }

        [HttpPost]
        [Authorize(Roles = "User")]
        public ActionResult DownVoteReply(VoteBindingModel model)
        {
            service.DownVoteReply(model,GetUserId());
            return this.RedirectToAction("Index");
        }

        [HttpPost]
        [Authorize(Roles = "User")]
        public ActionResult UpVoteForumComment(VoteBindingModel model)
        {
            service.UpVoteForumComment(model,GetUserId());
            return this.RedirectToAction("Index");
        }

        [HttpPost]
        [Authorize(Roles = "User")]
        public ActionResult DownVoteForumComment(VoteBindingModel model)
        {
            service.DownVoteForumComment(model,GetUserId());
            return this.RedirectToAction("Index");
        }
    }
}