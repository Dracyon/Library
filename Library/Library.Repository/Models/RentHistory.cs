using System;
using System.ComponentModel.DataAnnotations;

namespace Library.Repository.Models
{
	public class RentHistory
	{
		[Key]
		public int Id { get; set; }

		[Required]
		[Display(Name = "Renter name")]
		[MaxLength(100)]
		public string RenterName { get; set; }

		[Display(Name = "Creation date")]
		[DataType(DataType.Date)]
		[DisplayFormat(DataFormatString = "{0:dd-MM-yyyy HH:mm}", ApplyFormatInEditMode = true)]
		public DateTime CreationDate { get; set; }

		[Display(Name = "Update date")]
		[DataType(DataType.Date)]
		[DisplayFormat(DataFormatString = "{0:dd-MM-yyyy HH:mm}", ApplyFormatInEditMode = true)]
		public DateTime UpdatenDate { get; set; }

		public int BookId { get; set; }

		public virtual Book Book { get; set; }
	}
}