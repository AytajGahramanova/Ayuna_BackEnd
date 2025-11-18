namespace ayuna_main.Models
{
	public class DetailDescription
	{
		public int Id { get; set; }
		public string? TitleOne { get; set; }
		public string? TitleTwo{ get; set; }
		public string? DescriptionOne { get; set; }
		public string? DescriptionTwo { get; set; }

		public int ProductId { get; set; }
		public Product Product { get; set; }
	}
}
