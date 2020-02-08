using System.ComponentModel.DataAnnotations;

namespace MakeIt.WebUI.ViewModel
{
    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage = "Email is required"), RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Invalid email address")]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}