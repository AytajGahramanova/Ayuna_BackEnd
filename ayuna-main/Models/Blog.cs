using System.ComponentModel.DataAnnotations.Schema;

namespace ayuna_main.Models
{
	public class Blog
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string? Image { get; set; }
		public string Description { get; set; }
		public DateTime dateTime { get; set; } = DateTime.Now;

		[NotMapped]
		public IFormFile? formFile { get; set; }

		public ICollection<Category> categories { get; set; }
	}
}
