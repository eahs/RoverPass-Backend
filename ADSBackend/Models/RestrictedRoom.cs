using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ADSBackend.Models.Identity;
namespace ADSBackend.Models
{
    public class RestrictedRoom
    {
        [Key]
        public int RestrictedRoomId { get; set; }
        [Display(Name = "Staff Name")]
        public int UserId { get; set; }
        public ApplicationUser User { get; set; }
        [Display(Name = "Date Issued")]
        public DateTime IssuedDate { get; set; }
        [Display(Name = "Class Name")]
        public int ClassId { get; set; }
        public Class ClassName { get; set; }
        [Display(Name = "Room Number")]
        public int RoomNumberId { get; set; }
        public Class RoomNumber { get; set; }
        [Display(Name = "Restriction Status")]
        public Boolean RestrictionType { get; set; }
    }
}