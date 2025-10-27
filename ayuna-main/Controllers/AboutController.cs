using ayuna_main.DataAccessLayer;
using ayuna_main.Models;
using ayuna_main.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace ayuna_main.Controllers
{
	public class AboutController : Controller
	{
		private readonly AppDbContext _db;
		public AboutController(AppDbContext db)
		{
			_db = db;
		}
		public IActionResult Index()
		{
			List<AboutBreadcrumb> aboutBreadcrumbs = _db.aboutBreadcrumbs.ToList();
			AboutSigniture aboutSigniture = _db.aboutSignitures.FirstOrDefault();
			ViewBag.AboutSignitureCount = _db.aboutSignitures.Count();
			AboutContent aboutContent = _db.aboutContents.FirstOrDefault();
			ViewBag.AboutContentCount = _db.aboutContents.Count();
			List<AboutFaq> aboutFaqs = _db.aboutFaqs.ToList();

			AboutVM aboutVM = new AboutVM
			{
				aboutBreadcrumbs = aboutBreadcrumbs,
				aboutSigniture = aboutSigniture,
				aboutContent = aboutContent,
				aboutFaqs = aboutFaqs
			};
			return View(aboutVM);
		}
	}
}
