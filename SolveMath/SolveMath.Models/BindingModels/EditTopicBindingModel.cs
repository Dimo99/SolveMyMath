using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace SolveMath.Models.BindingModels
{
    public class EditTopicBindingModel
    {
        public int Id { get; set; }
        [AllowHtml]
        [Required(ErrorMessage = "Съдържанието е задължително")]
        [StringLength(400,MinimumLength = 10,ErrorMessage = "Съдържанието трябва да е поне 10 символа и не трябва да надвишава 400.")]
        public string Content { get; set; }
        [AllowHtml]
        [Required(ErrorMessage = "Заглавието е задължително")]
        [StringLength(90,MinimumLength = 2,ErrorMessage = "Заглавието трябва да съдържа поне 2 символа и не може да е по-голямо от 90.")]
        public string Title { get; set; }
        [AllowHtml]
        public string CategoryName { get; set; }
        [AllowHtml]
        public string TagNames { get; set; }
        public string[] OldTagNames { get; set; }
    }
}
