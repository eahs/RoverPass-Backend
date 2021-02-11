using ADSBackend.Models.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ADSBackend.Models
{
    public class Restriction
    {
        [Key]
        public int UserId { get; set; }
        [Display(Name = "Staff Name")]
        public ApplicationUser staffName { get; set; }
        [Display(Name = "Date Issued")]
        public DateTime IssuedDate { get; set; }
        public int ClassId { get; set; }
        [Display(Name = "Class Name")]
        public Class cName { get; set; }
        [Display(Name = "Restricted")]
        public Boolean restrictionType { get; set; }
    }
}
