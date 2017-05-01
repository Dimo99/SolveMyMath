using System.Collections.Generic;
using System.Web.Mvc;
using SolveMath.Models.BindingModels;
using SolveMath.Models.ViewModels;
using SolveMath.Services;
using SolveMath.Services.Contracts;

namespace SolveMath.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private IForumService forumService;
        private IAdminService service;
        public AdminController()
        {
            forumService = new ForumService();
            service = new AdminService();
        }
        // GET: Admin/Admin
        public ActionResult Index()
        {
            return this.View();
        }

        public ActionResult NewCategory()
        {
            return this.View();
        }

        
        [HttpPost]
        public ActionResult NewCategory(CategoryBindingModel categoryBindingModel)
        {
            service.CreateCategory(categoryBindingModel);
            return this.RedirectToAction("Index");
        }
        public ActionResult AddSubCategory(int id)
        {
            SubCategoryViewModel subCategoryViewModel = service.SubCategory(id);
            return this.View(subCategoryViewModel);
        }

        [HttpPost]
        public ActionResult AddSubCategory(SubCategoryBindingModel subCategoryBindingModel)
        {
            service.CreateSubCategory(subCategoryBindingModel);
            return this.RedirectToAction("Index");
        }
        public ActionResult EditCategory(int id)
        {
            EditCategoryViewModel editCategoryViewModel = service.GetCategory(id);
            return this.View(editCategoryViewModel);
        }

        [HttpPost]
        public ActionResult EditCategory(EditCategoryBindingModel editCategoryBindingModel)
        {
            service.EditCategory(editCategoryBindingModel);
            return this.RedirectToAction("Index");
        }
        public ActionResult DeleteCategory(int id)
        {
            EditCategoryViewModel editCategoryViewModel = service.GetCategory(id);
            return this.View(editCategoryViewModel);
        }

        [HttpPost]
        public ActionResult DeleteCategory(DeleteCategoryBindingModel deleteCategoryBindingModel)
        {
            service.DeleteCategory(deleteCategoryBindingModel);
            return RedirectToAction("Index");
        }
        public ActionResult Categories()
        {
            IEnumerable<CategoryNavbarViewModel> categoryNavbarViewModels = forumService.GetCategories();
            return this.PartialView("_Categories",categoryNavbarViewModels);
        }
    }
}