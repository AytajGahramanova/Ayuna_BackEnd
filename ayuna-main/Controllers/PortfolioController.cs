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
			List<Portfolio> portfolio = _db.portfolios.ToList();
			return View(portfolio);
		}
	}
}
