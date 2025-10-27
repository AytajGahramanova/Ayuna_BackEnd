using ayuna_main.DataAccessLayer;
using ayuna_main.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace ayuna_main.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class AboutBreadcrumbsController : Controller
	{
		private readonly IWebHostEnvironment _webHostEnvironment;
		private readonly AppDbContext _db;

		public AboutBreadcrumbsController(AppDbContext db, IWebHostEnvironment webHostEnvironment)
		{
			_db = db;
			_webHostEnvironment = webHostEnvironment;
		}
		public IActionResult Index()
		{
			List<AboutBreadcrumb> aboutBreadcrumbs = _db.aboutBreadcrumbs.ToList();
			return View(aboutBreadcrumbs);
		}

		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Create(AboutBreadcrumb aboutBreadcrumb)
		{
			if (ModelState.IsValid)
			{
				if (aboutBreadcrumb.formFile != null)
				{
					string uploadFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");

					if (!Directory.Exists(uploadFolder))
						Directory.CreateDirectory(uploadFolder);

					string uniqueFileName = Guid.NewGuid().ToString() + "_" + aboutBreadcrumb.formFile.FileName;

					string filePath = Path.Combine(uploadFolder, uniqueFileName);

					using (var stream = new FileStream(filePath, FileMode.Create))
					{
						await aboutBreadcrumb.formFile.CopyToAsync(stream);
					}

					aboutBreadcrumb.Image = "/uploads/" + uniqueFileName;
				}
				_db.aboutBreadcrumbs.Add(aboutBreadcrumb);
				_db.SaveChanges();
				return RedirectToAction("Index", "AboutBreadcrumbs");
			}
			return View(aboutBreadcrumb);
		}

		[HttpGet]
		public IActionResult Read(int id)
		{
			AboutBreadcrumb aboutBreadcrumb = _db.aboutBreadcrumbs.Find(id);

			if (aboutBreadcrumb == null)
			{
				return NotFound();
			}
			return View(aboutBreadcrumb);
		}

		[HttpGet]
		public IActionResult Update(int id)
		{
			if (id == null)
			{
				return BadRequest();
			}

			AboutBreadcrumb aboutBreadcrumbs = _db.aboutBreadcrumbs.Find(id);

			if (aboutBreadcrumbs == null)
			{
				return NotFound();
			}
			return View(aboutBreadcrumbs);
		}

		[HttpPost]
		public IActionResult Update(int id, AboutBreadcrumb aboutBreadcrumb)
		{
			if (id == null)
			{
				return BadRequest();
			}
			AboutBreadcrumb aboutBreadcrumb1 = _db.aboutBreadcrumbs.Find(id);
			aboutBreadcrumb1.Image = aboutBreadcrumb.Image;
			_db.SaveChanges();

			if (aboutBreadcrumb1 == null)
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

			AboutBreadcrumb aboutBreadcrumb = _db.aboutBreadcrumbs.Find(id);

			if (aboutBreadcrumb == null)
			{
				return NotFound();
			}

			return View(aboutBreadcrumb);
		}

		[HttpPost, ActionName("Delete")]

		public IActionResult DeleteConfirm(int id)
		{
			if (id == null)
			{
				return BadRequest();
			}
			AboutBreadcrumb aboutBreadcrumb = _db.aboutBreadcrumbs.Find(id);

			_db.aboutBreadcrumbs.Remove(aboutBreadcrumb);
			_db.SaveChanges();

			return RedirectToAction("Index");
		}
	}
}
