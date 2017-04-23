using System.ComponentModel.DataAnnotations;

namespace SolveMath.Models.ViewModels
{
    public class ForgotViewModel
    {

        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
