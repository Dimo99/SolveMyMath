using System.ComponentModel.DataAnnotations;

namespace SolveMath.Models.ViewModels
{
    public class ReplyEditViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Съдържанието е задължително")]
        public string Content { get; set; }
    }
}
