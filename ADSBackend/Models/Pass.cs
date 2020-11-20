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
        public string Reason { get; set; }
        public DateTime IsssuedDate { get; set; }
        public int memberId { get; set; }
        public Member Member { get; set; }
    }
}
