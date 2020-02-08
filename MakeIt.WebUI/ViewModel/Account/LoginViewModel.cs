using System.ComponentModel.DataAnnotations;

namespace MakeIt.WebUI.ViewModel
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email is required"), RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Invalid Email")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required"), RegularExpression(@"(?=.*?[#?!@$%^&*-])(?=.*?[A-Za-z0-9]).{8,}", ErrorMessage = "At least 8 length, at least one special character")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }
    }
}