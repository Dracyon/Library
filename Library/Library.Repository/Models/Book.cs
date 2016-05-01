using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Repository.Models
{
	public class Book
	{
		[Key]
		public int Id { get; set; }

		[Display(Name = "Title: ")]
		[Required]
		[MaxLength(120)]
		public string Title { get; set; }

		[Display(Name = "Author: ")]
		[Required]
		[MaxLength(80)]
		public string Author { get; set; }

		public int CategoryId { get; set; }
		public virtual Category Category { get; set; }

		[Display(Name = "ISBN: ")]
		[Required]
		[Index("ISBN_index", 1, IsUnique = true)]
		[MaxLength(13)]
		public string Isbn { get; set; }

		[Required]
		public bool Available { get; set; }

		public List<RentHistory> RentHistories { get; set; }

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