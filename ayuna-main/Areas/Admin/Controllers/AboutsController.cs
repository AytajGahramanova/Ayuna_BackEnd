using ayuna_main.DataAccessLayer;
using ayuna_main.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace ayuna_main.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = "SuperAdmin, Admin")]
	public class AboutsController : Controller
	{
		private readonly IWebHostEnvironment _webHostEnvironment;
		private readonly AppDbContext _db;
		public AboutsController(AppDbContext db, IWebHostEnvironment webHostEnvironment)
		{
			_db = db;
			_webHostEnvironment = webHostEnvironment;
		}
		public IActionResult Index()
		{
			About about = _db.abouts?.FirstOrDefault();
			ViewBag.Count = _db.abouts?.Count();
			return View(about);
		}

		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Create(About about)
		{
			if (ModelState.IsValid)
			{
				if (about.formFile != null)
				{
					string uploadFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");

					if (!Directory.Exists(uploadFolder))
						Directory.CreateDirectory(uploadFolder);

					string uniqueFileName = Guid.NewGuid().ToString() + "_" + about.formFile.FileName;

					string filePath = Path.Combine(uploadFolder, uniqueFileName);

					using (var stream = new FileStream(filePath, FileMode.Create))
					{
						await about.formFile.CopyToAsync(stream);
					}

					about.Image = "/uploads/" + uniqueFileName;
				}
				_db.abouts.Add(about);
				_db.SaveChanges();
				return RedirectToAction("Index", "Abouts");
			}
			return View(about);
		}

		[HttpGet]
		public IActionResult Read(int id)
		{
			About about = _db.abouts.Find(id);

			if (about == null)
			{
				return NotFound();
			}
			return View(about);
		}

		[HttpGet]
		public IActionResult Update(int id)
		{
			if (id == null)
			{
				return BadRequest();
			}
			About about = _db.abouts.Find(id);

			if (about == null)
			{
				return NotFound();
			}
			return View(about);
		}

		[HttpPost]
		public IActionResult Update(int id, About about)
		{
			if (id == null)
			{
				return BadRequest();
			}
			About dbAbout = _db.abouts.Find(id);
			dbAbout.Title = about.Title;
			dbAbout.Description = about.Description;
			dbAbout.Image = about.Image;
			_db.SaveChanges();

			if (dbAbout == null)
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

			About about = _db.abouts.FirstOrDefault(x => x.Id == id);

			if (about == null)
			{
				return NotFound();
			}
			return View(about);
		}

		[HttpPost, ActionName("Delete")]
		public IActionResult DeleteConfirm(int id)
		{
			if (id == null)
			{
				return BadRequest();
			}
			About about = _db.abouts.FirstOrDefault(y => y.Id == id);
			_db.abouts.Remove(about);
			_db.SaveChanges();

			return RedirectToAction("Index");
		}
	}
}
