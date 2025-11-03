using ayuna_main.DataAccessLayer;
using ayuna_main.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace ayuna_main.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = "SuperAdmin, Admin")]
	public class BlogsController : Controller
	{
		private readonly IWebHostEnvironment _webHostEnvironment;
		private readonly AppDbContext _db;

		public BlogsController(AppDbContext db, IWebHostEnvironment webHostEnvironment)
		{
			_db = db;
			_webHostEnvironment = webHostEnvironment;
		}
		public IActionResult Index()
		{
			List<Blog> blogs = _db.blogs.ToList();
			return View(blogs);
		}

		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Create(Blog blog)
		{
			if (ModelState.IsValid)
			{
				if (blog.formFile != null)
				{
					string uploadFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");

					if (!Directory.Exists(uploadFolder))
						Directory.CreateDirectory(uploadFolder);

					string uniqueFileName = Guid.NewGuid().ToString() + "_" + blog.formFile.FileName;

					string filePath = Path.Combine(uploadFolder, uniqueFileName);

					using (var stream = new FileStream(filePath, FileMode.Create))
					{
						await blog.formFile.CopyToAsync(stream);
					}

					blog.Image = "/uploads/" + uniqueFileName;
				}
				_db.blogs.Add(blog);
				_db.SaveChanges();
				return RedirectToAction("Index", "Blogs");
			}
			return View(blog);
		}

		[HttpGet]
		public IActionResult Read(int id)
		{
			Blog blog = _db.blogs.Find(id);

			if (blog == null)
			{
				return NotFound();
			}
			return View(blog);
		}

		[HttpGet]
		public IActionResult Update(int id)
		{
			if (id == null)
			{
				return BadRequest();
			}

			Blog blog = _db.blogs.Find(id);

			if (blog == null)
			{
				return NotFound();
			}
			return View(blog);
		}

		[HttpPost]
		public IActionResult Update(int id, Blog blog)
		{
			if (id == null)
			{
				return BadRequest();
			}
			Blog dblog = _db.blogs.Find(id);
			dblog.Name = blog.Name;
			dblog.Image = blog.Image;
			_db.SaveChanges();

			if (dblog == null)
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

			Blog blog = _db.blogs.Find(id);

			if (blog == null)
			{
				return NotFound();
			}

			return View(blog);
		}

		[HttpPost, ActionName("Delete")]

		public IActionResult DeleteConfirm(int id)
		{
			if (id == null)
			{
				return BadRequest();
			}
			Blog blog = _db.blogs.Find(id);

			_db.blogs.Remove(blog);
			_db.SaveChanges();

			return RedirectToAction("Index");
		}
	}
}
