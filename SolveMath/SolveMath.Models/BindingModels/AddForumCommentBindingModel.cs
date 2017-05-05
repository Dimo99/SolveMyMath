using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace SolveMath.Models.BindingModels
{
    public class AddForumCommentBindingModel
    {
        public int Id { get; set; }
        public int TopicId { get; set; }
        [AllowHtml]
        [Required(ErrorMessage = "Не може да изпратите празен отговор")]
        [StringLength(400,MinimumLength = 10,ErrorMessage = "Съдържанието трябва да е поне 10 символа и не може да е повече от 400.")]
        public string Content { get; set; }
    }
}
