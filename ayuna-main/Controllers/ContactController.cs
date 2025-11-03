using ayuna_main.DataAccessLayer;
using ayuna_main.Models;
using ayuna_main.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace ayuna_main.Controllers
{
	public class ContactController : Controller
	{
		private readonly AppDbContext _db;
		public ContactController(AppDbContext db)
		{
			_db = db;
		}
		public IActionResult Index()
		{
			ViewBag.ContactCount = _db.contact.Count();

			ContactVM contactVM = new ContactVM
			{
				contact = _db.contact.FirstOrDefault(),
			};
			return View(contactVM);
		}

		[HttpPost]
		public IActionResult SendMessage(ContactSubmit contactSubmit)
		{
			if (ModelState.IsValid)
			{
				_db.contactSubmit.Add(contactSubmit);
				_db.SaveChanges();
				return RedirectToAction("Index", "Contact");
			}

			return View(contactSubmit);
		}
	}
}
