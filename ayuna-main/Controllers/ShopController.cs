using ayuna_main.DataAccessLayer;
using ayuna_main.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ayuna_main.Controllers
{
	public class ShopController : Controller
	{
		private readonly AppDbContext _db;
		public ShopController(AppDbContext db)
		{
			_db = db;
		}
		public IActionResult Index(int page = 1, string search = "", int? categoryId = 0, string select = "")
		{
			var viewData = 6;

			var query = _db.products.Include(x => x.Categories).AsQueryable();

			if (!string.IsNullOrEmpty(search))
			{
				query = query.Where(x => x.Name.ToLower().Contains(search));
			}

			if (categoryId != 0)
			{
				query = query.Where(x => x.Categories.Any(c => c.Id == categoryId));
			}

			switch (select)
			{
				case "price_asc":
					query = query.OrderBy(x => x.Price);
					break;
				case "price_desc":
					query = query.OrderByDescending(x => x.Price);
					break;
				case "name_asc":
					query = query.OrderBy(x => x.Name);
					break;
				case "name_desc":
					query = query.OrderByDescending(x => x.Name);
					break;
				default:
					query = query.OrderByDescending(x => x.Id);
					break;
			}

			var productCount = query.Count();
			var products = query.Skip((page * viewData) - viewData).Take(viewData).ToList();

			var category = _db.category.ToList();

			var pageCount = (int)Math.Ceiling((double)productCount / viewData);


			ViewBag.PageCount = pageCount;
			ViewBag.Page = page;
			ViewBag.Categories = category;
			ViewBag.Search = search;
			ViewBag.TotalProducts = productCount;
			ViewBag.CurrentShowing = products.Count;
			
			ShopVM shopVM = new ShopVM
			{
				Products = products,
				Categories = category
			};
			return View(shopVM);
		}
	}
}
