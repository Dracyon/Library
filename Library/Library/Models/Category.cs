using System;
using System.ComponentModel.DataAnnotations;

namespace Library.Models
{
	public class Category
	{
		[Key]
		public int Id { get; set; }

		[Display(Name = "Category: ")]
		[Required]
		[MaxLength(50)]
		public string Name { get; set; }

		[Display(Name = "Creation date")]
		[DataType(DataType.Date)]
		[DisplayFormat(DataFormatString = "{0:dd-MM-yyyy HH:mm}", ApplyFormatInEditMode = true)]
		public DateTime CreationDate { get; set; }

		[Display(Name = "Update date")]
		[DataType(DataType.Date)]
		[DisplayFormat(DataFormatString = "{0:dd-MM-yyyy HH:mm}", ApplyFormatInEditMode = true)]
		public DateTime UpdatenDate { get; set; }
	}
}