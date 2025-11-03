using ayuna_main.Models;

namespace ayuna_main.ViewModel
{
	public class BlogDetailVM
	{
		public Blog blog { get; set; }
		public List<Blog> blogs { get; set; }
		public Category category { get; set; }
		public List<Category> categories { get; set; }

	}
}
