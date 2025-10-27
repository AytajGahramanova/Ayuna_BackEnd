using System.ComponentModel.DataAnnotations.Schema;

namespace ayuna_main.Models
{
	public class GiftCardPage
	{
		public int Id { get; set; }	
		public string? Image {  get; set; }
		public double bgPrice { get; set; }
		public string bgCategory { get; set; }
		public string Category { get; set; }
		public string Title { get; set; }
		public double Price { get; set; }

		[NotMapped]
		public IFormFile? formFile { get; set; }	
	}
}
