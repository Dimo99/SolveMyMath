using System.Collections.Generic;
using SolveMath.Models.Entities;
using SolveMath.Models.ViewModels;

namespace SolveMath.Services.Contracts
{
    public interface IForumService
    {
        IEnumerable<TopicHeaderViewModel> GetTopicsForPage(int? page);
        IEnumerable<CategoriesViewModel> GetCategories();
    }
}