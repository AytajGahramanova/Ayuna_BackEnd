using ayuna_main.DataAccessLayer;
using ayuna_main.Models;
using Microsoft.AspNetCore.Mvc;

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
