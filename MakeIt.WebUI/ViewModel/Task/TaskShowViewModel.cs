using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using MakeIt.EF;

namespace MakeIt.WebUI.ViewModel
{
    public class TaskShowViewModel
    {
        public int? Id { get; set; }
        [Required(ErrorMessage = "Title is required")]
        [StringLength(50, ErrorMessage = "Maximum length is 50 characters")]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [StringLength(500, ErrorMessage = "Maximum Length is 500 characters")]
        [Display(Name = "Description")]
        public string Description { get; set; }
        [DataType(DataType.Date)]
        public DateTime DueDate { get; set; }
        
        public string Priority { get; set; }

        public string Status { get; set; }

        public string Project { get; set; }

        public string CreatedUser { get; set; }

        public string AssignedUser { get; set; }
    }
}