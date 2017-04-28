using System.Collections.Generic;

namespace SolveMath.Models.ViewModels
{
    public class CategoriesViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<CategoriesViewModel> Categories { get; set; }
        public bool IsUsed { get; set; }
    }
}
