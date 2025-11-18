using ayuna_main.DataAccessLayer;
using ayuna_main.Models;
using ayuna_main.ViewModel;
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
		public IActionResult Index(int page = 1, string search = "", int? categoryId = 0)
		{
			var viewData = 4;

			var query = _db.blogs.Include(x => x.categories).OrderByDescending(x => x.Id).AsQueryable();

			if (!string.IsNullOrEmpty(search))
			{
				query = query.Where(x => x.Name.ToLower().Contains(search));
			}

			if (categoryId != 0)
			{
				query = query.Where(x => x.categories.Any(c => c.Id == categoryId));
			}

			var blogCount = query.Count();
			var blogs = query.Skip((page * viewData) - viewData).Take(viewData).ToList();

			var category = _db.category.ToList();

			var pageCount = (int)Math.Ceiling((double)blogCount / viewData);

			ViewBag.PageCount = pageCount;
			ViewBag.Page = page;
			ViewBag.Categories = category;
			ViewBag.Search = search;

			BlogVM blogVM = new BlogVM()
			{
				Categories = category,
				Blogs = blogs,
			};

			return View(blogVM);
		}


		public IActionResult BlogDetail(int? id)
		{
			BlogDetailVM blogDetailVM = new BlogDetailVM
			{
				blog = _db.blogs.FirstOrDefault(y => y.Id == id),
				blogs = _db.blogs.ToList(),
				category = _db.category.FirstOrDefault(),
				categories = _db.category.ToList(),
			};

			if (id == null)
			{
				return BadRequest();
			}

			return View(blogDetailVM);
		}
	}
}
