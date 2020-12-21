using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ADSBackend.Models
{
    public class Class
    {
        [Key]
        [Display (Name = "Teacher Name")]
        public string teacherName { get; set; }
        public int Block { get; set; }
        [Display (Name = "Class Name")]
        public string className { get; set; }
        [Display (Name = "Room Number")]
        public string roomNumber { get; set; }
    }
}
