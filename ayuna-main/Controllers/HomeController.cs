using System.Diagnostics;
using ayuna_main.DataAccessLayer;
using ayuna_main.Models;
using ayuna_main.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace ayuna_main.Controllers
{
	public class HomeController : Controller
	{
		private readonly AppDbContext _db;
		public HomeController(AppDbContext db)
		{
			_db = db;
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

			HomeVM homeVm = new HomeVM
			{
				sliders = sliders,
				about = about,
				portfolios = portfolios,
				giftCard = giftCard,
				blogs = blogs,
			};

			return View(homeVm);
		}
	}
}
