using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MakeIt.EF;

namespace MakeIt.BLL.DTO
{
    public class TaskDTO
    {
        public int Id { get; set; }

        public string Title { get; set; }
        
        public string Description { get; set; }

        public DateTime DueDate { get; set; }

        public string Priority { get; set; }

        public string Status { get; set; }

        public string Project { get; set; }

        public string CreatedUser { get; set; }

        public string AssignedUser { get; set; }
    }
}
