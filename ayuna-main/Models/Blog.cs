using System.ComponentModel.DataAnnotations.Schema;

namespace ayuna_main.Models
{
	public class Blog
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string? Image { get; set; }

		[NotMapped]
		public IFormFile? formFile { get; set; }
	}
}
