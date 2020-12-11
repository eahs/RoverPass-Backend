using ADSBackend.Helpers;
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
		[Required]
		[StringLength(32, MinimumLength = 1, ErrorMessage = "Teacher name is required")]  // Max 32 characters, min 1 character
		[Display(Name = "Teacher Name")]
		public string TeacherName { get; set; }
		
		[Required]
		[StringLength(32, MinimumLength = 1, ErrorMessage = "Class Name is required")]  // Max 32 characters, min 1 character
		[Display(Name = "Class Name")]
		public string Name { get; set; }

		[Required]
		[StringLength(32, MinimumLength = 1, ErrorMessage = "Block is required")]  // Max 32 characters, min 1 character
		[Display(Name = "Block")]
		public String Block { get; set; }

		[Required]
		[StringLength(32, MinimumLength = 1, ErrorMessage = "Block is required")]  // Max 32 characters, min 1 character
		[Display(Name = "Class Code")]
		public string JoinCode { get; set; } //= RandomString.Generate(6);
	}
}
