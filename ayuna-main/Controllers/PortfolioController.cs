using ayuna_main.DataAccessLayer;
using ayuna_main.Models;
using Microsoft.AspNetCore.Mvc;

namespace ayuna_main.Controllers
{
	public class PortfolioController : Controller
	{
		private readonly AppDbContext _db;

		public PortfolioController(AppDbContext db)
		{
			_db = db;
		}
		public IActionResult Index()
		{
			ViewBag.portfolioCount = _db.portfolios.Count();
			List<Portfolio> portfolio = _db.portfolios.OrderByDescending(x=>x.Id).Take(4).ToList();
			return View(portfolio);
		}

		public IActionResult LoadMore(int skip)
		{
			List<Portfolio> portfolio = _db.portfolios.OrderByDescending(x => x.Id).Skip(skip).Take(4).ToList();
			return PartialView("_PortfolioLoadMorePartial", portfolio);
		}
	}
}
