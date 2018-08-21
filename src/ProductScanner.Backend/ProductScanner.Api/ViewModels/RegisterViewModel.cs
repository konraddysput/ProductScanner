using System.ComponentModel.DataAnnotations;

namespace ProductScanner.Api.ViewModels
{
    public class RegisterViewModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage ="Login is required")]
        [MinLength(6, ErrorMessage = "Login require at least 6 characters")]
        public string Login { get; set; }

        [EmailAddress(ErrorMessage = "You need to provide valid e-mail address")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "E-mail is requried")]
        public string Email { get; set; }

        [MinLength(6, ErrorMessage ="Password require at least 6 characters")]
        public string Password { get; set; }
    }
}
