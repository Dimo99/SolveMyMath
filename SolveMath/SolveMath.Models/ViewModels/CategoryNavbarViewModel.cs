using System.Collections.Generic;

namespace SolveMath.Models.ViewModels
{
    public class CategoryNavbarViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<CategoryNavbarViewModel> SubCategories { get; set; }
    }
}
