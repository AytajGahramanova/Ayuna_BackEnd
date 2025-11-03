using ayuna_main.DataAccessLayer;
using ayuna_main.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ayuna_main.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = "SuperAdmin, Admin")]
	public class AboutFaqsController : Controller
	{
		private readonly AppDbContext _db;
		public AboutFaqsController(AppDbContext db)
		{
			_db = db;
		}
		public IActionResult Index()
		{
			List<AboutFaq> aboutFaqs = _db.aboutFaqs.ToList();
			return View(aboutFaqs);
		}

		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Create(AboutFaq aboutFaq)
		{
			if (ModelState.IsValid)
			{
				_db.aboutFaqs.Add(aboutFaq);
				_db.SaveChanges();
				return RedirectToAction("Index", "AboutFaqs");
			}
			return View(aboutFaq);
		}

		[HttpGet]
		public IActionResult Read(int id)
		{
			AboutFaq aboutFaq = _db.aboutFaqs.Find(id);

			if (aboutFaq == null)
			{
				return NotFound();
			}
			return View(aboutFaq);
		}

		[HttpGet]
		public IActionResult Update(int id)
		{
			if (id == null)
			{
				return BadRequest();
			}

			AboutFaq aboutFaq = _db.aboutFaqs.Find(id);

			if (aboutFaq == null)
			{
				return NotFound();
			}
			return View(aboutFaq);
		}

		[HttpPost]
		public IActionResult Update(int id, AboutFaq aboutFaq)
		{
			if (id == null)
			{
				return BadRequest();
			}
			AboutFaq dbaboutFaq = _db.aboutFaqs.Find(id);
			dbaboutFaq.Title = aboutFaq.Title;
			dbaboutFaq.Description = aboutFaq.Description;
			_db.SaveChanges();

			if (dbaboutFaq == null)
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

			AboutFaq aboutFaq = _db.aboutFaqs.Find(id);

			if (aboutFaq == null)
			{
				return NotFound();
			}

			return View(aboutFaq);
		}

		[HttpPost, ActionName("Delete")]

		public IActionResult DeleteConfirm(int id)
		{
			if (id == null)
			{
				return BadRequest();
			}
			AboutFaq aboutFaq = _db.aboutFaqs.Find(id);

			_db.aboutFaqs.Remove(aboutFaq);
			_db.SaveChanges();

			return RedirectToAction("Index");
		}
	}
}
