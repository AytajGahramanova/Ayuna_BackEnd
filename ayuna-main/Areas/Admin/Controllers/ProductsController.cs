using ayuna_main.DataAccessLayer;
using ayuna_main.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace ayuna_main.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class ProductsController : Controller
	{
		private readonly AppDbContext _db;
		private readonly IWebHostEnvironment _webHostEnvironment;
		public ProductsController(AppDbContext db, IWebHostEnvironment webHostEnvironment)
		{
			_db = db;
			_webHostEnvironment = webHostEnvironment;
		}
		public IActionResult Index()
		{
			List<Product> products = _db.products.ToList();
			return View(products);
		}

		[HttpGet]
		public IActionResult Create()
		{
			ViewBag.Categories = _db.category.ToList();
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Create(Product product, int[] CategoryId)
		{

	
			if (!ModelState.IsValid)
			{
				product.Categories = new List<Category>();

				foreach (var item in CategoryId)
				{
					if (item!=null)
					{
						var findCategories = _db.category.Find(item);

						if (findCategories != null)
						{
							product.Categories.Add(findCategories);
						}
					}
				}
				if (product.formFile != null)
				{
					string uploadFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");

					if (!Directory.Exists(uploadFolder))
						Directory.CreateDirectory(uploadFolder);

					string uniqueFileName = Guid.NewGuid().ToString() + "_" + product.formFile.FileName;

					string filePath = Path.Combine(uploadFolder, uniqueFileName);

					using (var stream = new FileStream(filePath, FileMode.Create))
					{
						await product.formFile.CopyToAsync(stream);
					}

					product.Image = "/uploads/" + uniqueFileName;
				}
				if (product.hoverFormFile != null)
				{
					string uploadFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");

					if (!Directory.Exists(uploadFolder))
						Directory.CreateDirectory(uploadFolder);

					string uniqueFileName = Guid.NewGuid().ToString() + "_" + product.hoverFormFile.FileName;

					string filePath = Path.Combine(uploadFolder, uniqueFileName);

					using (var stream = new FileStream(filePath, FileMode.Create))
					{
						await product.hoverFormFile.CopyToAsync(stream);
					}

					product.hoverImage = "/uploads/" + uniqueFileName;
				}

				if (product.ImageOneFormFile != null)
				{
					string uploadFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");

					if (!Directory.Exists(uploadFolder))
						Directory.CreateDirectory(uploadFolder);

					string uniqueFileName = Guid.NewGuid().ToString() + "_" + product.ImageOneFormFile.FileName;

					string filePath = Path.Combine(uploadFolder, uniqueFileName);

					using (var stream = new FileStream(filePath, FileMode.Create))
					{
						await product.ImageOneFormFile.CopyToAsync(stream);
					}

					product.ImageOne = "/uploads/" + uniqueFileName;
				}
				if (product.ImageTwoFormFile != null)
				{
					string uploadFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");

					if (!Directory.Exists(uploadFolder))
						Directory.CreateDirectory(uploadFolder);

					string uniqueFileName = Guid.NewGuid().ToString() + "_" + product.ImageTwoFormFile.FileName;

					string filePath = Path.Combine(uploadFolder, uniqueFileName);

					using (var stream = new FileStream(filePath, FileMode.Create))
					{
						await product.ImageTwoFormFile.CopyToAsync(stream);
					}

					product.ImageTwo = "/uploads/" + uniqueFileName;
				}
				if (product.ImageThreeFormFile != null)
				{
					string uploadFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");

					if (!Directory.Exists(uploadFolder))
						Directory.CreateDirectory(uploadFolder);

					string uniqueFileName = Guid.NewGuid().ToString() + "_" + product.ImageThreeFormFile.FileName;

					string filePath = Path.Combine(uploadFolder, uniqueFileName);

					using (var stream = new FileStream(filePath, FileMode.Create))
					{
						await product.ImageThreeFormFile.CopyToAsync(stream);
					}

					product.ImageThree = "/uploads/" + uniqueFileName;
				}

				_db.products.Add(product);
				_db.SaveChanges();
				return RedirectToAction("Index", "Products");
			}
			return View(product);
		}

		[HttpGet]
		public IActionResult Read(int id)
		{
			Product product = _db.products.Find(id);

			if (product == null)
			{
				return NotFound();
			}
			return View(product);
		}

		[HttpGet]
		public IActionResult Update(int id)
		{
			if (id == null)
			{
				return BadRequest();
			}

			Product product = _db.products.Find(id);

			if (product == null)
			{
				return NotFound();
			}
			return View(product);
		}

		[HttpPost]
		public IActionResult Update(int id, Product product)
		{
			if (id == null)
			{
				return BadRequest();
			}
			Product dbproduct = _db.products.Find(id);
			dbproduct.Name = product.Name;
			dbproduct.Image = product.Image;
			dbproduct.hoverImage = product.hoverImage;
			dbproduct.Price = product.Price;

			_db.SaveChanges();

			if (dbproduct == null)
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

			Product product = _db.products.Find(id);

			if (product == null)
			{
				return NotFound();
			}

			return View(product);
		}

		[HttpPost, ActionName("Delete")]
		public IActionResult DeleteConfirm(int id)
		{
			if (id == null)
			{
				return BadRequest();
			}
			Product product = _db.products.Find(id);

			_db.products.Remove(product);
			_db.SaveChanges();

			return RedirectToAction("Index");
		}

	}
}
