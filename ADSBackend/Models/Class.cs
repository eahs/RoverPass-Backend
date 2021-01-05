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
        public int ClassId { get; set; }

        [Display (Name = "Teacher Name")]
        public string TeacherName { get; set; }
        public int PeriodId { get; set; }
        public Period Period { get; set; }
        [Display (Name = "Class Name")]
        public string ClassName { get; set; }
        [Display (Name = "Room Number")]
        public string RoomNumber { get; set; }
        
        [Required, MaxLength(6)]
        [Display(Name = "Join Code")]
        public string JoinCode { get; set; }
    }
}
