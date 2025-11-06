using ayuna_main.DataAccessLayer;
using ayuna_main.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ayuna_main.Controllers
{
	public class BlogController : Controller
	{
		private readonly AppDbContext _db;
		public BlogController(AppDbContext db)
		{

			_db = db;
		}
		public IActionResult Index(int page = 1)
		{
			var viewData = 4;

			var query = _db.blogs.Include(x => x.categories).OrderByDescending(x => x.Id);

			var blogCount = query.Count();
			var blogs = query.Skip((page * viewData) - viewData).Take(viewData).ToList();

			var category = _db.category.ToList();

			var pageCount = (int)Math.Ceiling((double)blogCount / viewData);

			ViewBag.PageCount = pageCount;
			ViewBag.Page = page;
			ViewBag.Categories = category;

			return View(blogs);
		}


		public IActionResult BlogDetail(int? id)
		{
			if (id == null)
			{
				return BadRequest();
			}
			Blog blog = _db.blogs.FirstOrDefault(y => y.Id == id);

			if (blog == null)
			{
				return NotFound();
			}
			return View(blog);
		}
	}
}
