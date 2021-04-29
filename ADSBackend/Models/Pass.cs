using ADSBackend.Models.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ADSBackend.Models
{
    public class Pass
    {        
        [Key]
        public int PassId { get; set; }

        [Display(Name = "Pass Type")]
        public int PassTypeId { get; set; }
        public PassType PassType { get; set; }
        public bool IsApproved { get; set; }
        public string Reason { get; set; }
        [Display(Name = "Date Issued")]
        public DateTime IssuedDate { get; set; }


        [Display(Name = "Sign In Time")]
       
        public DateTime SignInTime { get; set; }
        [Display(Name = "Sign Out Time")]
        public DateTime SignOutTime { get; set; }
        [Display(Name = "Date")]
        public DateTime StartDate { get; set; }

        [Display(Name = "Student")]
        public int UserId { get; set; }
        public ApplicationUser User { get; set; }

        [Display(Name = "Teacher")]
        public int? ReviewerId { get; set; }
        public ApplicationUser Reviewer { get; set; }
    }
}
