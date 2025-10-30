using System.ComponentModel.DataAnnotations.Schema;

namespace ayuna_main.Models
{
	public class FooterContent
	{
		public int Id { get; set; }
		public string? Logo {  get; set; }
		public string Description { get; set; }

		[NotMapped]
		public IFormFile? formFile { get; set; }
	}
}
