using ayuna_main.DataAccessLayer;
using ayuna_main.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace ayuna_main.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class GiftCardPagesController : Controller
	{
		private readonly IWebHostEnvironment _webHostEnvironment;
		private readonly AppDbContext _db;
		public GiftCardPagesController(AppDbContext db, IWebHostEnvironment webHostEnvironment)
		{

			_db = db;
			_webHostEnvironment = webHostEnvironment;
		}
		public IActionResult Index()
		{
			List<GiftCardPage> giftCardPages = _db.giftCardsPage.ToList();
			return View(giftCardPages);
		}

		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Create(GiftCardPage giftCardPage)
		{
			if (ModelState.IsValid)
			{
				if (giftCardPage.formFile != null)
				{
					string uploadFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");

					if (!Directory.Exists(uploadFolder))
						Directory.CreateDirectory(uploadFolder);

					string uniqueFileName = Guid.NewGuid().ToString() + "_" + giftCardPage.formFile.FileName;

					string filePath = Path.Combine(uploadFolder, uniqueFileName);

					using (var stream = new FileStream(filePath, FileMode.Create))
					{
						await giftCardPage.formFile.CopyToAsync(stream);
					}

					giftCardPage.Image = "/uploads/" + uniqueFileName;
				}
				_db.giftCardsPage.Add(giftCardPage);
				_db.SaveChanges();
				return RedirectToAction("Index", "GiftCardPages");
			}
			return View(giftCardPage);
		}

		[HttpGet]
		public IActionResult Read(int id)
		{
			GiftCardPage giftCardPage = _db.giftCardsPage.Find(id);

			if (giftCardPage == null)
			{
				return NotFound();
			}
			return View(giftCardPage);
		}

		[HttpGet]
		public IActionResult Update(int id)
		{
			if (id == null)
			{
				return BadRequest();
			}

			GiftCardPage giftCardPage = _db.giftCardsPage.Find(id);

			if (giftCardPage == null)
			{
				return NotFound();
			}
			return View(giftCardPage);
		}

		[HttpPost]
		public IActionResult Update(int id, GiftCardPage giftCardPage)
		{
			if (id == null)
			{
				return BadRequest();
			}
			GiftCardPage giftCardPage1 = _db.giftCardsPage.Find(id);
			giftCardPage1.Image = giftCardPage.Image;
			giftCardPage1.bgPrice = giftCardPage.bgPrice;
			giftCardPage1.bgCategory = giftCardPage.bgCategory;
			giftCardPage1.Category = giftCardPage.Category;
			giftCardPage1.Title = giftCardPage.Title;
			giftCardPage1.Price = giftCardPage.Price;
			_db.SaveChanges();

			if (giftCardPage1 == null)
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

			GiftCardPage giftCardPage = _db.giftCardsPage.Find(id);

			if (giftCardPage == null)
			{
				return NotFound();
			}

			return View(giftCardPage);
		}

		[HttpPost, ActionName("Delete")]

		public IActionResult DeleteConfirm(int id)
		{
			if (id == null)
			{
				return BadRequest();
			}
			GiftCardPage giftCardPage = _db.giftCardsPage.Find(id);

			_db.giftCardsPage.Remove(giftCardPage);
			_db.SaveChanges();

			return RedirectToAction("Index");
		}
	}
}
