namespace ayuna_main.Models
{
	public class Testimonial
	{
		public int Id { get; set; }
		public int? Star {  get; set; }
		public string? Comment { get; set; }
		public string? UserName { get; set; }

		public string? Location { get; set; }

		public DateTime dateTime { get; set; } = DateTime.UtcNow;

		public int ProductId { get; set; }
		public Product Product { get; set; }

	}
}
