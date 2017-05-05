using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace SolveMath.Models.BindingModels
{
    public class EditCategoryBindingModel
    {
        public int Id { get; set; }
        [AllowHtml]
        [Required(ErrorMessage = "Не може да изпратите празна категория")]
        [StringLength(50,MinimumLength = 2,ErrorMessage = "Категорията трябва да съдържа поне два символа и не може да надвишава 50.")]
        public string Name { get; set; }   
    }
}
