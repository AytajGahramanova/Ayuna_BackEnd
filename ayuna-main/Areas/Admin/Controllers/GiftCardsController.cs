using ayuna_main.DataAccessLayer;
using ayuna_main.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace ayuna_main.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class GiftCardsController : Controller
	{
		private readonly IWebHostEnvironment _webHostEnvironment;
		private readonly AppDbContext _db;
		public GiftCardsController(AppDbContext db, IWebHostEnvironment webHostEnvironment)
		{
			_db = db;
			_webHostEnvironment = webHostEnvironment;
		}
		public IActionResult Index()
		{
			GiftCard giftCard = _db.giftCards?.FirstOrDefault();
			ViewBag.CountGC = _db.giftCards?.Count();
			return View(giftCard);
		}

		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Create(GiftCard giftCard)
		{
			if (ModelState.IsValid)
			{
				if (giftCard.formFile != null)
				{
					string uploadFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");

					if (!Directory.Exists(uploadFolder))
						Directory.CreateDirectory(uploadFolder);

					string uniqueFileName = Guid.NewGuid().ToString() + "_" + giftCard.formFile.FileName;

					string filePath = Path.Combine(uploadFolder, uniqueFileName);

					using (var stream = new FileStream(filePath, FileMode.Create))
					{
						await giftCard.formFile.CopyToAsync(stream);
					}

					giftCard.Image = "/uploads/" + uniqueFileName;
				}
				_db.giftCards.Add(giftCard);
				_db.SaveChanges();
				return RedirectToAction("Index", "GiftCards");
			}
			return View(giftCard);
		}

		[HttpGet]
		public IActionResult Read(int id)
		{
			GiftCard giftCard = _db.giftCards.Find(id);

			if (giftCard == null)
			{
				return NotFound();
			}
			return View(giftCard);
		}

		[HttpGet]
		public IActionResult Update(int id)
		{
			if (id == null)
			{
				return BadRequest();
			}
			GiftCard giftCard = _db.giftCards.Find(id);

			if (giftCard == null)
			{
				return NotFound();
			}
			return View(giftCard);
		}

		[HttpPost]
		public IActionResult Update(int id, GiftCard giftCard)
		{
			if (id == null)
			{
				return BadRequest();
			}
			GiftCard dbGiftCard = _db.giftCards.Find(id);
			dbGiftCard.Title = giftCard.Title;
			dbGiftCard.Description = giftCard.Description;
			_db.SaveChanges();

			if (dbGiftCard == null)
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

			GiftCard giftCard = _db.giftCards.FirstOrDefault(x => x.Id == id);

			if (giftCard == null)
			{
				return NotFound();
			}
			return View(giftCard);
		}

		[HttpPost, ActionName("Delete")]
		public IActionResult DeleteConfirm(int id)
		{
			if (id == null)
			{
				return BadRequest();
			}
			GiftCard giftCard = _db.giftCards.FirstOrDefault(y => y.Id == id);
			_db.giftCards.Remove(giftCard);
			_db.SaveChanges();

			return RedirectToAction("Index");
		}
	}
}
