using ayuna_main.DataAccessLayer;
using ayuna_main.Models;
using ayuna_main.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ayuna_main.Controllers
{
	public class GiftCartController : Controller
	{
		private readonly AppDbContext _db;

		public GiftCartController(AppDbContext db)
		{
			_db = db;
		}
		public IActionResult Index()
		{
			List<GiftCardPage> giftCardPages = _db.giftCardsPage.ToList();
			return View(giftCardPages);
		}
	}
}
