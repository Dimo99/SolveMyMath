using System.ComponentModel.DataAnnotations;

namespace SolveMath.Models.ViewModels
{
    public class LoginViewModel
    {

        [Required]
        [Display(Name = "Емайл")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Парола")]
        public string Password { get; set; }

        [Display(Name = "Запомни ме?")]
        public bool RememberMe { get; set; }
    }
}
