using Microsoft.AspNetCore.Mvc;

namespace ayuna_main.Controllers
{
	public class ErrorController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
