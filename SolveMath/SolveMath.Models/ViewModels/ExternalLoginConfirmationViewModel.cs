using System.ComponentModel.DataAnnotations;

namespace SolveMath.Models.ViewModels
{
    public class ExternalLoginConfirmationViewModel
    {
        [Microsoft.Build.Framework.Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
