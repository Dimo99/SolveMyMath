
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace SolveMath.Models.BindingModels
{
    public class EditForumCommentBindingModel
    {
        public int Id { get; set; }
        [AllowHtml]
        [Required(ErrorMessage = "Съдържанието не може да е празно.")]
        public string Content { get; set; }
    }
}
