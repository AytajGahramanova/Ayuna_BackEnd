using ayuna_main.Models;

namespace ayuna_main.ViewModel
{
	public class HomeVM
	{
		public List<Slider> sliders {  get; set; }	
		public About about { get; set; }	
		public List<Portfolio> portfolios { get; set; }
		public GiftCard giftCard { get; set; }	
		public List<Blog> blogs { get; set; }	
	}
}
