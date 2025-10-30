using ayuna_main.Models;

namespace ayuna_main.ViewModel
{
	public class FooterVM
	{
		public FooterContent footerContent {  get; set; }
		public List<FooterLink> footerLinks { get; set; }
		public List<FooterCategory> footerCategories { get; set; }
		public FooterContact footerContact { get; set; }
	}
}
