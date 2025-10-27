using ayuna_main.Models;

namespace ayuna_main.ViewModel
{
	public class AboutVM
	{
		public List<AboutBreadcrumb> aboutBreadcrumbs { get; set; }
		public AboutSigniture aboutSigniture { get; set; }
		public AboutContent aboutContent { get; set; }
		public List<AboutFaq> aboutFaqs { get; set; }
	}
}
