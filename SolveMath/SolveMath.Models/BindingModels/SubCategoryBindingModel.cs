using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace SolveMath.Models.BindingModels
{
    public class SubCategoryBindingModel
    {
        public int Id { get; set; }
        [AllowHtml]
        [Required(ErrorMessage = "Името на категорията е задължително")]
        [StringLength(50,MinimumLength = 2,ErrorMessage = "Името на категорията трябва да е поне 2 символа и не повече от 50")]
        public string Name { get; set; }
    }
}
