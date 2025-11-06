using System.ComponentModel.DataAnnotations.Schema;

namespace ayuna_main.Models
{
	public class BlogDetail
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string? Image { get; set; }
		public string Description { get; set; }
		public DateTime dateTime { get; set; } = DateTime.Now;

		[NotMapped]
		public IFormFile? formFile { get; set; }

		[ForeignKey("Blog")]
		public int BlogId { get; set; }
		public Blog Blog { get; set; }
	}
}
