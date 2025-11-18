using ayuna_main.Models;

namespace ayuna_main.ViewModel
{
	public class ProductDetailVM
	{
		public Product product { get; set; }	
		public List<Product> products { get; set; }
		public List<Category> categories { get; set; }
		public Testimonial testimonial { get; set; }
		public List<Testimonial> testimonials { get; set; }
		public DetailDescription detailDescription { get; set; }
	}
}
