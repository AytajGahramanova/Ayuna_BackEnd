using System.ComponentModel.DataAnnotations.Schema;

namespace ayuna_main.Models
{
	public class AboutSigniture
	{
		public int Id { get; set; }	
		public string Description { get; set; }
		public string? Image { get; set; }

		[NotMapped]
		public IFormFile? formFile { get; set; }
	}
}
