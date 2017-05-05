using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace SolveMath.Models.BindingModels
{
    public class CategoryBindingModel
    {
        [AllowHtml]
        [Required(ErrorMessage = "Името на категорията е задължително!")]
        [StringLength(50,MinimumLength = 2,ErrorMessage = "Категорията трябва да е с поне два символа и да е по-малко от 50.")]
        public string Name { get; set; }
    }
}
