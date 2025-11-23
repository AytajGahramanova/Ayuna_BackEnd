using ayuna_main.DataAccessLayer;
using ayuna_main.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace ayuna_main.Controllers
{
	[Authorize]

	public class BasketController : Controller
	{
		private readonly AppDbContext _db;
		private readonly UserManager<AppUser> _userManager;
		public BasketController(AppDbContext db, UserManager<AppUser> userManager)
		{
			_db = db;
			_userManager = userManager;
		}
		public IActionResult Index()
		{
			var userId = _userManager.GetUserId(User);

			var basket = _db.baskets.Include(x => x.Product).Where(x => x.UserId == userId).ToList();

			ViewBag.Count = basket.Count;

			return View(basket);
		}

		public IActionResult Add(int productId)
		{
			var userId = _userManager.GetUserId(User);

			var product = _db.baskets.FirstOrDefault(x=> x.UserId == userId && x.ProductId == productId);

			if(product == null)
			{
				var item = new Basket
				{
					UserId = userId,
					ProductId = productId,
				};
				_db.baskets.Add(item);
				_db.SaveChanges();
			}

			return RedirectToAction("Index", "Shop");
		}

		public IActionResult Delete(int id)
		{
			var userId = _userManager.GetUserId(User);

			var removeProduct = _db.baskets.FirstOrDefault(x => x.UserId == userId && x.ProductId == id);

			if(removeProduct != null)
			{
				_db.baskets.Remove(removeProduct);
				_db.SaveChanges();
			}

			return RedirectToAction("Index");
		}

	}
}
