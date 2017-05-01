using SolveMath.Models.BindingModels;
using SolveMath.Models.ViewModels;

namespace SolveMath.Services.Contracts
{
    public interface IAdminService
    {
        void CreateCategory(CategoryBindingModel categoryBindingModel);
        void EditCategory(EditCategoryBindingModel editCategoryBindingModel);
        void DeleteCategory(DeleteCategoryBindingModel model);
        EditCategoryViewModel GetCategory(int id);
        void CreateSubCategory(SubCategoryBindingModel subCategoryBindingModel);
        SubCategoryViewModel SubCategory(int id);
    }
}
