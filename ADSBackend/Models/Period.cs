using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ADSBackend.Models
{
    public class Period
    {
        [Key]
        public int PeriodId { get; set; }
        [Display(Name = "Period")]
        public string Name { get; set; }
        public int Order { get; set; }
    }
}
