using System;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using SolveMath.Models.BindingModels;
using SolveMath.Models.ViewModels;
using SolveMath.Services;
using SolveMath.Services.Contracts;

namespace SolveMath.Areas.Forum.Controllers
{
    public class ForumController : Controller
    {
        private IForumService service;

        public ForumController()
        {
            service = new ForumService();
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
            }
            return this.RedirectToAction("Index");
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
            string userId = User.Identity.GetUserId();
            service.CreateAnswer(abm,userId);
            return this.RedirectToAction("Topic",new {abm.Id});
        }

        public ActionResult AddComment(int id,int topicId)
        {
            AddCommentViewModel acvm = new AddCommentViewModel() {Id = id,TopicId = topicId};
            return View(acvm);
        }
        [HttpPost]
        public ActionResult AddComment(AddForumCommentBindingModel acbm)
        {
            service.CreateComment(acbm,User.Identity.GetUserId());
            return this.RedirectToAction("Topic", new {id = acbm.TopicId});
        }
        public ActionResult Topic(int id)
        {
            
            var model = service.GetTopic(id);
            return this.View(model);
        }
        // GET: Forum/Forum
        public ActionResult Index(int? page)
        {
            var pageTopics = service.GetTopicsForPage(page);
            return View(pageTopics);
        }
        public ActionResult Category(string name)
        {
            return this.View();
        }

        public ActionResult Category()
        {
            CategoryViewModel categoryViewModel = service.GetCategoryViewModel();
            return this.View();
        }
        public ActionResult Categories()
        {
            var categories = service.GetCategories();
            return this.PartialView("_Categories", categories);
        }
        
    }
}