using System.ComponentModel.DataAnnotations;

namespace ProductScanner.ViewModels.Token
{
    public class LoginViewModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Login is required")]
        public string Login { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}
