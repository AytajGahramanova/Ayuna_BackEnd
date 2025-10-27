using Microsoft.AspNetCore.Mvc;

namespace ayuna_main.Controllers
{
	public class WishlistController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
