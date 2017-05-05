using System.ComponentModel.DataAnnotations;

namespace SolveMath.Models.ViewModels
{
    public class RegisterViewModel
    {

        [Required]
        [EmailAddress]
        [Display(Name = "Емайл")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0}та трябва да бъде поне {2} знака.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Парола")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Потвърди паролата")]
        [Compare("Password", ErrorMessage = "Паролата и потвърдената парола не съвпадат")]
        public string ConfirmPassword { get; set; }
    }
}
