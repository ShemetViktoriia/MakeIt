using System.ComponentModel.DataAnnotations;

namespace MakeIt.WebUI.ViewModel
{
    public class MemberViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Member Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required"), RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Invalid email address")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}