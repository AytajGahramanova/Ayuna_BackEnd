using ayuna_main.DataAccessLayer;
using ayuna_main.Models;
using ayuna_main.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System.Diagnostics;

namespace ayuna_main.Controllers
{
	public class HomeController : Controller
	{
		private readonly AppDbContext _db;
		private readonly UserManager<AppUser> _userManager;
		public HomeController(AppDbContext db, UserManager<AppUser> userManager)
		{
			_db = db;
			_userManager = userManager;
		}
		public IActionResult Index()
		{
			ViewBag.AboutCount = _db.abouts?.Count();
			List<Slider> sliders = _db.sliders.ToList();
			About about = _db.abouts?.FirstOrDefault();
			List<Portfolio> portfolios = _db.portfolios.ToList();
			GiftCard giftCard = _db.giftCards?.FirstOrDefault();
			ViewBag.GiftCardCount = _db.giftCards.Count();
			List<Blog> blogs = _db.blogs.Take(2).ToList();
			List<Product> products = _db.products.Include(x => x.Categories).Take(4).ToList();

			HomeVM homeVm = new HomeVM
			{
				sliders = sliders,
				about = about,
				portfolios = portfolios,
				giftCard = giftCard,
				blogs = blogs,
				products = products
			};

			return View(homeVm);
		}

		public IActionResult ProductDetail(int id)
		{
			if (id == null)
			{
				return RedirectToAction("Index", "Error");
			}

			var product = _db.products
		.Include(x => x.Categories)
		.Include(x => x.Testimonial)
		.Include(x => x.DetailDescription)
		.FirstOrDefault(x => x.Id == id);

			if (product == null)
			{
				return RedirectToAction("Index", "Error");
			}

			ProductDetailVM productDetailVM = new ProductDetailVM
			{
				product = product,
				products = _db.products.ToList(),
				categories = _db.category.ToList(),
				testimonial = new Testimonial(),
				testimonials = _db.testimonials.Where(t => t.ProductId == id).ToList(),
				detailDescription = product.DetailDescription,
			};

			return View(productDetailVM);
		}

		[HttpPost]
		public async Task<IActionResult> CommentCreate(Testimonial testimonial, int rating, int proId)
		{
			var user = await _userManager.GetUserAsync(User);

			testimonial.Star = rating;
			testimonial.ProductId = proId;
			testimonial.UserId = user.Id;

			_db.testimonials.Add(testimonial);
			await _db.SaveChangesAsync();

			return RedirectToAction("ProductDetail", "Home", new { id = proId });
		}
	}
}
