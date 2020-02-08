using System.ComponentModel.DataAnnotations;

namespace MakeIt.WebUI.ViewModel
{
    public class ResetPasswordViewModel
    {
        [Required(ErrorMessage = "Password is required"), RegularExpression(@"(?=.*?[#?!@$%^&*-])(?=.*?[A-Za-z0-9]).{8,}", ErrorMessage = "Minimum length is 8 characters, require non letter or digit, require digit")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Password confirmation is required")]
        [Compare("Password", ErrorMessage = "Password and confirmation do not match")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }
}