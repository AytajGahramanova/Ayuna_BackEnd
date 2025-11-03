using ayuna_main.DataAccessLayer;
using ayuna_main.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ayuna_main.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = "SuperAdmin, Admin")]
	public class ContactsController : Controller
	{
		private readonly AppDbContext _db;
		public ContactsController(AppDbContext db)
		{
			_db = db;
		}

		public IActionResult Index()
		{
			Contact contact = _db.contact.FirstOrDefault();
			ViewBag.Count = _db.contact?.Count();
			return View(contact);
		}

		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Create(Contact contact)
		{
			if (ModelState.IsValid)
			{
				_db.contact.Add(contact);
				_db.SaveChanges();
				return RedirectToAction("Index", "Contacts");
			}

			return View(contact);
		}

		[HttpGet]
		public IActionResult Read(int id)
		{
			Contact contact = _db.contact.Find(id);

			if (contact == null)
			{
				return NotFound();
			}
			return View(contact);
		}

		[HttpGet]
		public IActionResult Update(int id)
		{
			if (id == null)
			{
				return BadRequest();
			}
			Contact contact = _db.contact.Find(id);

			if (contact == null)
			{
				return NotFound();
			}
			return View(contact);
		}

		[HttpPost]
		public IActionResult Update(int id, Contact contact)
		{
			if (id == null)
			{
				return BadRequest();
			}
			Contact dbContact = _db.contact.Find(id);
			dbContact.Description = contact.Description;
			dbContact.Phone = contact.Phone;
			dbContact.Address = contact.Address;
			dbContact.Email = contact.Email;

			_db.SaveChanges();

			if (dbContact == null)
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

			Contact contact = _db.contact.FirstOrDefault(x => x.Id == id);

			if (contact == null)
			{
				return NotFound();
			}
			return View(contact);
		}

		[HttpPost, ActionName("Delete")]
		public IActionResult DeleteConfirm(int id)
		{
			if (id == null)
			{
				return BadRequest();
			}
			Contact contact = _db.contact.FirstOrDefault(y => y.Id == id);
			_db.contact.Remove(contact);
			_db.SaveChanges();

			return RedirectToAction("Index");
		}

	}
}
