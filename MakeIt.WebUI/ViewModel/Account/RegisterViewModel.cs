using System.ComponentModel.DataAnnotations;

namespace MakeIt.WebUI.ViewModel
{
    public class RegisterViewModel
    {
        public RegisterViewModel()
        {

        }
        public RegisterViewModel(string recaptchaPublicKey)
        {
            RecaptchaPublicKey = recaptchaPublicKey;
        }

        [Required(ErrorMessage = "User name is required"), RegularExpression(@"^[a-zA-Z][a-zA-Z0-9_.@]{4,}$", ErrorMessage = "At least 5 characters required")]
        [Display(Name = "UserName")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Email is required"), RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Invalid email address")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required"), RegularExpression(@"(?=.*?[#?!@$%^&*-])(?=.*?[A-Za-z0-9]).{8,}", ErrorMessage = "Minimum length is 8 characters, require non letter or digit, require digit")]
        [StringLength(100, ErrorMessage = "Minimum Length = 8", MinimumLength = 8)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Password confirmation is required")]
        [DataType(DataType.Password)]
        [Display(Name = "Password confirmation")]
        [Compare("Password", ErrorMessage = "Password and confirmation do not match")]
        public string ConfirmPassword { get; set; }

        public string RecaptchaPublicKey { get; }
    }
}