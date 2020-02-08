using MakeIt.BLL.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MakeIt.WebUI.ViewModel
{
    public class ProjectViewModel
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(20, ErrorMessage = "Maximum Length is 20 characters")]
        [Display(Name = "Project name")]
        public string Name { get; set; }

        [StringLength(500, ErrorMessage = "Maximum Length is 500 characters")]
        [Display(Name = "Description (optional)")]
        public string Description { get; set; }

        [Display(Name = "Last Update Date")]
        public DateTime? LastUpdateDate { get; set; }

        public bool IsClosed { get; set; }

        public RoleInProjectEnum RoleInProject { get; set; }

        public IEnumerable<MemberViewModel> Members { get; set; }
        public MemberViewModel Owner { get; set; }
    }

}