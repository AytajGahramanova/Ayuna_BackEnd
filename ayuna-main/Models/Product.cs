using System.ComponentModel.DataAnnotations.Schema;

namespace ayuna_main.Models
{
	public class Product
	{
		public int Id { get; set; }
		public string? Image { get; set; }
		public string? hoverImage { get; set; }
		public string Name { get; set; }
		public double Price { get; set; }

		[NotMapped]
		public IFormFile? formFile { get; set; }
		[NotMapped]
		public IFormFile? hoverFormFile { get; set; }	
		public ICollection<Category> Categories { get; set; }
	}
}
