using System.ComponentModel.DataAnnotations.Schema;

namespace ayuna_main.Models
{
	public class AboutContent
	{
		public int Id { get; set; }	
		public string Title { get; set; }
		public string Description { get; set; }
		public string PreTitle { get; set; }
		public string PreDescription { get; set; }
		public string? Image {  get; set; }

		[NotMapped]
		public IFormFile formFile { get; set; }
	}
}
