using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using SolveMath.Data.Interfaces;
using SolveMath.Models.BindingModels;
using SolveMath.Models.Entities;
using SolveMath.Models.ViewModels;
using SolveMath.Services.Contracts;

namespace SolveMath.Services
{
    public class AdminService : Service, IAdminService
    {
        public AdminService() : base()
        {

        }

        public AdminService(ISolveMathContext context) : base(context)
        {

        }
        public void CreateCategory(CategoryBindingModel categoryBindingModel)
        {
            var category = Mapper.Map<Category>(categoryBindingModel);
            Context.Categories.Add(category);
            Context.SaveChanges();
        }

        public void EditCategory(EditCategoryBindingModel editCategoryBindingModel)
        {
            var category = Context.Categories.Find(editCategoryBindingModel.Id);
            category.Name = editCategoryBindingModel.Name;
            Context.SaveChanges();
        }

        public void DeleteCategory(DeleteCategoryBindingModel model,IManageService service)
        {
            var category = Context.Categories.Find(model.Id);
            var categories = Context.Categories;
            foreach (var category1 in categories)
            {
                if (category1.SubCategories.Contains(category))
                {
                    category1.SubCategories.Remove(category);
                }
            }
            var categoriesToDelete = category.SubCategories;
            foreach (var categoryToDelete in categoriesToDelete)
            {
                var topicsToRemove = categoryToDelete.Topics.ToList();
                for (var i = 0; i < topicsToRemove.Count; i++)
                {
                    categoryToDelete.Topics.Remove(topicsToRemove[i]);
                    service.DeleteTopic(new DeleteTopicBindingModel() {Id = topicsToRemove[i].Id});
                }
                Context.Categories.Remove(categoryToDelete);
            }
            var topics = category.Topics.ToList();
            for (var i = 0; i < topics.Count; i++)
            {
                category.Topics.Remove(topics[i]);
                service.DeleteTopic(new DeleteTopicBindingModel() { Id = topics[i].Id });
            }
            Context.Categories.Remove(category);
            Context.SaveChanges();
        }

        public EditCategoryViewModel GetCategory(int id)
        {
            return Mapper.Map<EditCategoryViewModel>(Context.Categories.Find(id));
        }

        public void CreateSubCategory(SubCategoryBindingModel subCategoryBindingModel)
        {
            var subCategory = new Category() { Name = subCategoryBindingModel.Name };
            var parrantCategory = Context.Categories.Find(subCategoryBindingModel.Id);
            parrantCategory.SubCategories.Add(subCategory);
            Context.SaveChanges();
        }

        public SubCategoryViewModel SubCategory(int id)
        {
            var category = Context.Categories.Find(id);
            SubCategoryViewModel model = new SubCategoryViewModel()
            {
                CategoryId = category.Id,
                CategoryName = category.Name
            };
            return model;
        }
    }
}
