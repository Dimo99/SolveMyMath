using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace SolveMath.Models.BindingModels
{
    public class EditReplyBindingModel
    {
        public int Id { get; set; }
        [AllowHtml]
        [Required(ErrorMessage = "Не може да изпратите празен отговор")]
        [StringLength(400,MinimumLength = 10,ErrorMessage = "Минималната дължина на съдържанието е 10, а максималната 400")]
        public string Content { get; set; }
    }
}
