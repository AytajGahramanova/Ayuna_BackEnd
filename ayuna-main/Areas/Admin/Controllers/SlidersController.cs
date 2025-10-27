using ayuna_main.DataAccessLayer;
using ayuna_main.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace ayuna_main.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class SlidersController : Controller
	{
		private readonly IWebHostEnvironment _webHostEnvironment;
		private readonly AppDbContext _db;

		public SlidersController(AppDbContext db, IWebHostEnvironment webHostEnvironment)
		{
			_db = db;
			_webHostEnvironment = webHostEnvironment;
		}
		public IActionResult Index()
		{
			List<Slider> sliders = _db.sliders.ToList();
			return View(sliders);
		}

		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Create(Slider slider)
		{
			if (ModelState.IsValid)
			{
				if (slider.formFile != null)
				{
					string uploadFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");

					if (!Directory.Exists(uploadFolder))
						Directory.CreateDirectory(uploadFolder);

					string uniqueFileName = Guid.NewGuid().ToString() + "_" + slider.formFile.FileName;

					string filePath = Path.Combine(uploadFolder, uniqueFileName);

					using (var stream = new FileStream(filePath, FileMode.Create))
					{
						await slider.formFile.CopyToAsync(stream);
					}

					slider.Image = "/uploads/" + uniqueFileName;
				}
				_db.sliders.Add(slider);
				_db.SaveChanges();
				return RedirectToAction("Index", "Sliders");
			}
			return View(slider);
		}

		[HttpGet]
		public IActionResult Read(int id)
		{
			Slider slider = _db.sliders.Find(id);

			if (slider == null)
			{
				return NotFound();
			}
			return View(slider);
		}

		[HttpGet]
		public IActionResult Update(int id)
		{
			if (id == null)
			{
				return BadRequest();
			}

			Slider slider = _db.sliders.Find(id);

			if (slider == null)
			{
				return NotFound();
			}
			return View(slider);
		}

		[HttpPost]
		public IActionResult Update(int id, Slider slider)
		{
			if (id == null)
			{
				return BadRequest();
			}
			Slider dbslider = _db.sliders.Find(id);
			dbslider.Title = slider.Title;
			dbslider.Image = slider.Image;
			_db.SaveChanges();

			if (dbslider == null)
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

			Slider slider = _db.sliders.Find(id);

			if (slider == null)
			{
				return NotFound();
			}

			return View(slider);
		}

		[HttpPost, ActionName("Delete")]

		public IActionResult DeleteConfirm(int id)
		{
			if (id == null)
			{
				return BadRequest();
			}
			Slider slider = _db.sliders.Find(id);

			_db.sliders.Remove(slider);
			_db.SaveChanges();

			return RedirectToAction("Index");
		}
	}

}
