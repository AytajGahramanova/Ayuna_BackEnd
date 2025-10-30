using System.ComponentModel.DataAnnotations.Schema;

namespace ayuna_main.Models
{
	public class HeaderTop
	{
		public int Id { get; set; }	
		public string? Logo { get; set; }

		[NotMapped]
		public IFormFile? formFile { get; set; }
	}
}
