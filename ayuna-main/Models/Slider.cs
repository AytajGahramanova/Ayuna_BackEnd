using System.ComponentModel.DataAnnotations.Schema;

namespace ayuna_main.Models
{
	public class Slider
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string? Image { get; set; }

		[NotMapped]
		public IFormFile? formFile { get; set; }	
	}
}
