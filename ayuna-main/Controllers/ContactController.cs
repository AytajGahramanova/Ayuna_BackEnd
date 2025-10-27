using Microsoft.AspNetCore.Mvc;

namespace ayuna_main.Controllers
{
	public class ContactController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
