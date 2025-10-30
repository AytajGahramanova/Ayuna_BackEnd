using ayuna_main.DataAccessLayer;
using ayuna_main.Models;
using ayuna_main.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ayuna_main.ViewComponents
{
	public class FooterViewComponent : ViewComponent
	{
		private readonly AppDbContext _db;
		public FooterViewComponent(AppDbContext db)
		{
			_db = db;
		}
		public async Task<IViewComponentResult> InvokeAsync()
		{
			FooterContent footerContent = await _db.footerContent.FirstOrDefaultAsync();
			List<FooterLink> footerLinks = await _db.footerLink.ToListAsync();
			List<FooterCategory> footerCategories = await _db.footerCategory.ToListAsync();
			FooterContact footerContact = await _db.footerContacts.FirstOrDefaultAsync();

			FooterVM footerVM = new FooterVM
			{
				footerContent = footerContent,
				footerLinks = footerLinks,
				footerCategories = footerCategories,
				footerContact = footerContact
				
			};

			return View(footerVM);
		}
	}
}
