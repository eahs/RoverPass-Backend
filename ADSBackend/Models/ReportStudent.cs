using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ADSBackend.Models.Identity;

namespace ADSBackend.Models
{
    public class ReportStudent
    {
        [Key]
        public int ReportId { get; set; }
        [Display(Name = "Staff Name")]
        public int UserId { get; set; }
        public ApplicationUser User { get; set; }
        [Display(Name = "Student")]
        public int StudentId { get; set; }
        public ApplicationUser Student { get; set; }
        [Display(Name = "Reason")]
        public int NameId { get; set; }
        public ReportType Name { get; set; }
        public string Description { get; set; }
    }
}
