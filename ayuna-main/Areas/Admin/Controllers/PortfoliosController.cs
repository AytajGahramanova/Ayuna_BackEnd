using ayuna_main.DataAccessLayer;
using ayuna_main.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace ayuna_main.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = "SuperAdmin, Admin")]
	public class PortfoliosController : Controller
	{
		private readonly IWebHostEnvironment _webHostEnvironment;
		private readonly AppDbContext _db;

		public PortfoliosController(AppDbContext db, IWebHostEnvironment webHostEnvironment)
		{
			_db = db;
			_webHostEnvironment = webHostEnvironment;
		}
		public IActionResult Index()
		{
			List<Portfolio> portfolios = _db.portfolios.ToList();
			return View(portfolios);
		}


		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Create(Portfolio portfolio)
		{
			if (ModelState.IsValid)
			{
				if (portfolio.formFile != null)
				{
					string uploadFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");

					if (!Directory.Exists(uploadFolder))
						Directory.CreateDirectory(uploadFolder);

					string uniqueFileName = Guid.NewGuid().ToString() + "_" + portfolio.formFile.FileName;

					string filePath = Path.Combine(uploadFolder, uniqueFileName);

					using (var stream = new FileStream(filePath, FileMode.Create))
					{
						await portfolio.formFile.CopyToAsync(stream);
					}

					portfolio.Image = "/uploads/" + uniqueFileName;
				}
				_db.portfolios.Add(portfolio);
				_db.SaveChanges();
				return RedirectToAction("Index", "Portfolios");
			}
			return View(portfolio);
		}

		[HttpGet]
		public IActionResult Read(int id)
		{
			Portfolio portfolio = _db.portfolios.Find(id);

			if (portfolio == null)
			{
				return NotFound();
			}
			return View(portfolio);
		}

		[HttpGet]
		public IActionResult Update(int id)
		{
			if (id == null)
			{
				return BadRequest();
			}

			Portfolio portfolio = _db.portfolios.Find(id);

			if (portfolio == null)
			{
				return NotFound();
			}
			return View(portfolio);
		}

		[HttpPost]
		public IActionResult Update(int id, Portfolio portfolio)
		{
			if (id == null)
			{
				return BadRequest();
			}
			Portfolio dbportfolio = _db.portfolios.Find(id);
			dbportfolio.Image = portfolio.Image;
			_db.SaveChanges();

			if (dbportfolio == null)
			{
				return NotFound();
			}

			return RedirectToAction("Index");
		}

		[HttpGet]
		public IActionResult Delete(int id)
		{
			if (id == null)
			{
				return BadRequest();
			}

			Portfolio portfolio = _db.portfolios.Find(id);

			if (portfolio == null)
			{
				return NotFound();
			}

			return View(portfolio);
		}

		[HttpPost, ActionName("Delete")]
		public IActionResult DeleteConfirm(int id)
		{
			if (id == null)
			{
				return BadRequest();
			}
			Portfolio portfolio = _db.portfolios.Find(id);

			_db.portfolios.Remove(portfolio);
			_db.SaveChanges();

			return RedirectToAction("Index");
		}
	}
}
