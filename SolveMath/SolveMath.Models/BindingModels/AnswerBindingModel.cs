

using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace SolveMath.Models.BindingModels
{
    public class AnswerBindingModel
    {
        public int Id { get; set; }
        [AllowHtml]
        [Required(ErrorMessage = "Съдържанието е задължително.")]
        [StringLength(400,MinimumLength = 10,ErrorMessage = "Минималната дължина е 10, а максималната 400")]
        public string Content { get; set; }
    }
}
