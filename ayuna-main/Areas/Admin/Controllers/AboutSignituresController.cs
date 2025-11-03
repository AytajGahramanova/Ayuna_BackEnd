using ayuna_main.DataAccessLayer;
using ayuna_main.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace ayuna_main.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = "SuperAdmin, Admin")]
	public class AboutSignituresController : Controller
	{
		private readonly IWebHostEnvironment _webHostEnvironment;
		private readonly AppDbContext _db;
		public AboutSignituresController(AppDbContext db, IWebHostEnvironment webHostEnvironment)
		{
			_db = db;
			_webHostEnvironment = webHostEnvironment;
		}
		public IActionResult Index()
		{
			AboutSigniture aboutSigniture = _db.aboutSignitures.FirstOrDefault();
			ViewBag.Count = _db.aboutSignitures?.Count();
			return View(aboutSigniture);
		}

		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Create(AboutSigniture aboutSigniture)
		{
			if (ModelState.IsValid)
			{
				if (aboutSigniture.formFile != null)
				{
					string uploadFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");

					if (!Directory.Exists(uploadFolder))
						Directory.CreateDirectory(uploadFolder);

					string uniqueFileName = Guid.NewGuid().ToString() + "_" + aboutSigniture.formFile.FileName;

					string filePath = Path.Combine(uploadFolder, uniqueFileName);

					using (var stream = new FileStream(filePath, FileMode.Create))
					{
						await aboutSigniture.formFile.CopyToAsync(stream);
					}

					aboutSigniture.Image = "/uploads/" + uniqueFileName;
				}
				_db.aboutSignitures.Add(aboutSigniture);
				_db.SaveChanges();
				return RedirectToAction("Index", "AboutSignitures");
			}
			return View(aboutSigniture);
		}

		[HttpGet]
		public IActionResult Read(int id)
		{
			AboutSigniture aboutSigniture = _db.aboutSignitures.Find(id);

			if (aboutSigniture == null)
			{
				return NotFound();
			}
			return View(aboutSigniture);
		}

		[HttpGet]
		public IActionResult Update(int id)
		{
			if (id == null)
			{
				return BadRequest();
			}
			AboutSigniture aboutSigniture = _db.aboutSignitures.Find(id);

			if (aboutSigniture == null)
			{
				return NotFound();
			}
			return View(aboutSigniture);
		}

		[HttpPost]
		public IActionResult Update(int id, AboutSigniture aboutSigniture)
		{
			if (id == null)
			{
				return BadRequest();
			}
			AboutSigniture dbaboutSigniture = _db.aboutSignitures.Find(id);

			dbaboutSigniture.Description = aboutSigniture.Description;
			dbaboutSigniture.Image = aboutSigniture.Image;
			_db.SaveChanges();

			if (dbaboutSigniture == null)
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

			AboutSigniture aboutSigniture = _db.aboutSignitures.FirstOrDefault(x => x.Id == id);

			if (aboutSigniture == null)
			{
				return NotFound();
			}
			return View(aboutSigniture);
		}

		[HttpPost, ActionName("Delete")]
		public IActionResult DeleteConfirm(int id)
		{
			if (id == null)
			{
				return BadRequest();
			}
			AboutSigniture aboutSigniture = _db.aboutSignitures.FirstOrDefault(y => y.Id == id);
			_db.aboutSignitures.Remove(aboutSigniture);
			_db.SaveChanges();

			return RedirectToAction("Index");
		}
	}
}
