using Microsoft.AspNetCore.Mvc;

namespace ayuna_main.Controllers
{
	public class CartController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
