using Microsoft.AspNetCore.Mvc;

namespace ayuna_main.Controllers
{
	public class BlogController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
