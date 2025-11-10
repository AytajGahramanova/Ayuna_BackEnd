namespace ayuna_main.Models
{
	public class Category
	{
		public int Id { get; set; }
		public string Name { get; set; }

		public ICollection<Blog> blogs { get; set; }
		public ICollection<Product> products { get; set; }
	}
}
