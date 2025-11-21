using ayuna_main.DataAccessLayer;
using ayuna_main.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace ayuna_main.Controllers
{
	[Authorize]
	public class WishlistController : Controller
	{
		private readonly AppDbContext _db;
		private readonly UserManager<AppUser> _userManager;
		public WishlistController(AppDbContext db, UserManager<AppUser> userManager)
		{
			_db = db;
			_userManager = userManager;
		}
		public async Task<IActionResult> Index()
		{
			var users = await _userManager.GetUserAsync(User);

			var items = _db.wishlist.Include(x => x.Product).Where(x => x.UserId == users.Id).ToList();
			return View(items);
		}

		public async Task<IActionResult> Add(int productId)
		{
			var users = await _userManager.GetUserAsync(User);

			bool isStock = _db.wishlist.Any(x => x.ProductId == productId && x.UserId == users.Id);
			var product = _db.products.FirstOrDefault(x => x.Id == productId);

			if (!isStock)
			{
				var item = new Wishlist
				{
					ProductId = productId,
					UserId = users.Id,
				};
				await _db.wishlist.AddAsync(item);
		        product.isLike = true;
				await _db.SaveChangesAsync();
			}
			else
			{
				var removePro = _db.wishlist
					.FirstOrDefault(x => x.ProductId == productId && x.UserId == users.Id);

				if (removePro != null)
				{
					_db.wishlist.Remove(removePro);
					removePro.Product.isLike = false;
					await _db.SaveChangesAsync();
				}
			}

			var controller = ControllerContext.RouteData.Values["controller"]?.ToString();

			if (controller == "Shop")
			{
				return RedirectToAction("Index", "Shop");
			}
			if (controller == "Home")
			{
				return RedirectToAction("Index", "Home");
			}

			return RedirectToAction("Index", "Home");


		}

		public async Task<IActionResult> Delete(int id)
		{
			var users = await _userManager.GetUserAsync(User);

			var item = _db.wishlist
				.Include(x => x.Product)
				.FirstOrDefault(x => x.ProductId == id && x.UserId == users.Id);

			if (item != null)
			{
		
				_db.wishlist.Remove(item);
				item.Product.isLike = false;
				await _db.SaveChangesAsync();
			}

			return RedirectToAction("Index");
		}

	}
}
