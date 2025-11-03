using ayuna_main.DataAccessLayer;
using ayuna_main.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace ayuna_main.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = "SuperAdmin, Admin")]
	public class AboutContentsController : Controller
	{
		private readonly IWebHostEnvironment _webHostEnvironment; 
		private readonly AppDbContext _db;
		public AboutContentsController(AppDbContext db, IWebHostEnvironment webHostEnvironment)
		{
			_db = db;
			_webHostEnvironment = webHostEnvironment;
		}
		public IActionResult Index()
		{
			AboutContent aboutContent = _db.aboutContents.FirstOrDefault();
			ViewBag.Count = _db.aboutContents?.Count();
			return View(aboutContent);
		}

		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Create(AboutContent aboutContent)
		{
			if (ModelState.IsValid)
			{
				if (aboutContent.formFile != null)
				{
					string uploadFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");

					if (!Directory.Exists(uploadFolder))
						Directory.CreateDirectory(uploadFolder);

					string uniqueFileName = Guid.NewGuid().ToString() + "_" + aboutContent.formFile.FileName;

					string filePath = Path.Combine(uploadFolder, uniqueFileName);

					using (var stream = new FileStream(filePath, FileMode.Create))
					{
						await aboutContent.formFile.CopyToAsync(stream);
					}

					aboutContent.Image = "/uploads/" + uniqueFileName;
				}
				_db.aboutContents.Add(aboutContent);
				_db.SaveChanges();
				return RedirectToAction("Index", "AboutContents");
			}
			return View(aboutContent);
		}

		[HttpGet]
		public IActionResult Read(int id)
		{
			AboutContent aboutContent = _db.aboutContents.Find(id);

			if (aboutContent == null)
			{
				return NotFound();
			}
			return View(aboutContent);
		}

		[HttpGet]
		public IActionResult Update(int id)
		{
			if (id == null)
			{
				return BadRequest();
			}
			AboutContent aboutContent = _db.aboutContents.Find(id);

			if (aboutContent == null)
			{
				return NotFound();
			}
			return View(aboutContent);
		}

		[HttpPost]
		public IActionResult Update(int id, AboutContent aboutContent)
		{
			if (id == null)
			{
				return BadRequest();
			}
			AboutContent dbaboutContent = _db.aboutContents.Find(id);
			dbaboutContent.Title = aboutContent.Title;
			dbaboutContent.Description = aboutContent.Description;
			dbaboutContent.PreTitle = aboutContent.PreTitle;
			dbaboutContent.PreDescription = aboutContent.PreDescription;
			dbaboutContent.Image = aboutContent.Image;
			_db.SaveChanges();

			if (dbaboutContent == null)
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

			AboutContent aboutContent = _db.aboutContents.FirstOrDefault(x => x.Id == id);

			if (aboutContent == null)
			{
				return NotFound();
			}
			return View(aboutContent);
		}

		[HttpPost, ActionName("Delete")]
		public IActionResult DeleteConfirm(int id)
		{
			if (id == null)
			{
				return BadRequest();
			}
			AboutContent aboutContent = _db.aboutContents.FirstOrDefault(y => y.Id == id);
			_db.aboutContents.Remove(aboutContent);
			_db.SaveChanges();

			return RedirectToAction("Index");
		}
	}
}
