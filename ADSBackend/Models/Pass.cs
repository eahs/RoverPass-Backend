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
        public int PassTypeId { get; set; }
        public PassType PassType { get; set; }
        public bool IsApproved { get; set; }
        public string Reason { get; set; }
        [Display(Name = "Date Issued")]
        public DateTime IssuedDate { get; set; }
        public int UserId { get; set; }
        public ApplicationUser User { get; set; }
        public int? ReviewerId { get; set; }
        public ApplicationUser Reviewer { get; set; }
    }
}
