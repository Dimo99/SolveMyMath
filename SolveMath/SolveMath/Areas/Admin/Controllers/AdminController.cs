using System.Collections.Generic;
using System.Web.Mvc;
using Ninject;
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
        private IManageService manageService;

        public AdminController(IAdminService service,IForumService forumService,IManageService manageService)
        {
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
            if (ModelState.IsValid)
            {
                service.CreateCategory(categoryBindingModel);
                return this.RedirectToAction("Index");
            }
            return this.View();
        }
        public ActionResult AddSubCategory(int id)
        {
            SubCategoryViewModel subCategoryViewModel = service.SubCategory(id);
            return this.View(subCategoryViewModel);
        }

        [HttpPost]
        public ActionResult AddSubCategory(SubCategoryBindingModel subCategoryBindingModel)
        {
            if (ModelState.IsValid)
            {
                service.CreateSubCategory(subCategoryBindingModel);
                return this.RedirectToAction("Index");
            }
            SubCategoryViewModel subCategoryViewModel = service.SubCategory(subCategoryBindingModel.Id);
            return this.View(subCategoryViewModel);
        }
        public ActionResult EditCategory(int id)
        {
            EditCategoryViewModel editCategoryViewModel = service.GetCategory(id);
            return this.View(editCategoryViewModel);
        }

        [HttpPost]
        public ActionResult EditCategory(EditCategoryBindingModel editCategoryBindingModel)
        {
            if (ModelState.IsValid)
            {
                service.EditCategory(editCategoryBindingModel);
                return this.RedirectToAction("Index");
            }
            EditCategoryViewModel editCategoryViewModel = service.GetCategory(editCategoryBindingModel.Id);
            return this.View(editCategoryViewModel);
        }
        public ActionResult DeleteCategory(int id)
        {
            EditCategoryViewModel editCategoryViewModel = service.GetCategory(id);
            return this.View(editCategoryViewModel);
        }

        [HttpPost]
        public ActionResult DeleteCategory(DeleteCategoryBindingModel deleteCategoryBindingModel)
        {
            if (ModelState.IsValid)
            {
                service.DeleteCategory(deleteCategoryBindingModel,manageService);
                return RedirectToAction("Index");
            }
            return this.View();
        }
        public ActionResult Categories()
        {
            IEnumerable<CategoryNavbarViewModel> categoryNavbarViewModels = forumService.GetCategories();
            return this.PartialView("_Categories",categoryNavbarViewModels);
        }
    }
}