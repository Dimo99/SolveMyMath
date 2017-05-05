using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SolveMath.Areas.Admin.Controllers;
using SolveMath.Data.Interfaces;
using SolveMath.Data.Mocks;
using SolveMath.Models.Entities;
using SolveMath.Services;
using SolveMath.Services.Contracts;
using TestStack.FluentMVCTesting;

namespace SolveMath.Tests.Controllers
{
    [TestClass]
    public class AdminControllerTest
    {
        private AdminController _adminController;
        private IAdminService _service;
        private IForumService _forumService;
        private IManageService _manageService;
        private ISolveMathContext _context;
        private List<Category> categories;
        [TestInitialize]
        public void Init()
        {
            categories = new List<Category>()
            {
                new Category()
                {
                    Id = 0,
                    Name = "Алгебра"
                },
                new Category()
                {
                    Id = 1,
                    Name = "Висша алгебра"
                },
                new Category()
                {
                    Id = 2,
                    Name = "Геометрия"
                }
            };
            categories[0].SubCategories.Add(categories[1]);
            this._context = new FakeSolveMathContext();
            foreach (var category in categories)
            {
                _context.Categories.Add(category);
            }
            _service = new AdminService(_context);
            _adminController = new AdminController(_service,_forumService,_manageService);
        }

        [TestMethod]
        public void Index_ReturnsDefaultView()
        {
            _adminController.WithCallTo(controller => controller.Index()).ShouldRenderDefaultView();
        }

        [TestMethod]
        public void NewCategory_ReturnsDefaultView()
        {
            _adminController.WithCallTo(controller => controller.NewCategory()).ShouldRenderDefaultView();
        }
    }
}
