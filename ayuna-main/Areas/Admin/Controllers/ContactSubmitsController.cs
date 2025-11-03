using ayuna_main.DataAccessLayer;
using ayuna_main.Models;
using ayuna_main.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ayuna_main.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = "SuperAdmin, Admin")]
	public class ContactSubmitsController : Controller
	{
		private readonly AppDbContext _db;
		public ContactSubmitsController(AppDbContext db)
		{
			_db = db;
		}
		public IActionResult Index()
		{
			List<ContactSubmit> contactSubmits = _db.contactSubmit.ToList();
			return View(contactSubmits);
		}
		[HttpPost]
		public IActionResult Delete(int id)
		{
			var result = _db.contactSubmit.Find(id);

			if(result == null)
			{
				return BadRequest();
			}
			_db.contactSubmit.Remove(result);	
			_db.SaveChanges();
			return RedirectToAction("Index");
		}
	}
}
