using Microsoft.AspNetCore.Mvc;

namespace ayuna_main.Controllers
{
	public class ShopController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
