using ayuna_main.DataAccessLayer;
using ayuna_main.Models;
using ayuna_main.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ayuna_main.ViewComponents
{
	public class HeaderViewComponent : ViewComponent
	{
		private readonly AppDbContext _db;
		public HeaderViewComponent(AppDbContext db)
		{
			_db = db;
		}
		public async Task<IViewComponentResult> InvokeAsync()
		{
			HeaderTop headerTop = await _db.headerTop.FirstOrDefaultAsync();
			List<HeaderNav> headerNavs = await _db.headerNav.ToListAsync();

			HeaderVM headerVM = new HeaderVM
			{
				headerNavs = headerNavs,
				headerTop = headerTop
			};

			return View(headerVM);
		}
	}
}
