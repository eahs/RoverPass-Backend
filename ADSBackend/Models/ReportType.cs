using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ADSBackend.Models
{
    public class ReportType
    {
       
            [Key]
            public int ReportTypeId { get; set; }
            public String Name { get; set; }
            public bool IsEnabled { get; set; }
        
    }
}
