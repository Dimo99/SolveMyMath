using System.Web.Mvc;
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
        // GET: Forum/Forum
        public ActionResult Index(int? page)
        {
            var pageTopics = service.GetTopicsForPage(page);
            return View(pageTopics);
        }

        public ActionResult Category(int? id)
        {
            return this.View();
        }

        public ActionResult Categories()
        {
            var categories = service.GetCategories();
            return this.PartialView("_Categories",categories);
        }
    }
}