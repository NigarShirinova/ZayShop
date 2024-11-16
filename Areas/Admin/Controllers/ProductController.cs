using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ZayShop.Areas.Admin.Models.Category;
using ZayShop.Areas.Admin.Models.Product;
using ZayShop.Areas.Admin.Models.Work;
using ZayShop.Data;
using ZayShop.Entities;

namespace ZayShop.Areas.Admin.Controllers
{
	[Area("admin")]
	public class ProductController : Controller
	{
		private readonly AppDbContext _context;

		public ProductController(AppDbContext context)
		{
			_context = context;
		}

		[HttpGet]
		public IActionResult Index()
		{
			return View();
		}

		[HttpGet]
		public IActionResult Create()
		{
			var model = new ProductCreateVM
			{
				Categories = _context.Categories.Select(c => new SelectListItem
				{
					Text = c.Name,
					Value = c.Id.ToString()
				}).ToList()
			};

			return View(model);
		}

		[HttpPost]
		public IActionResult Create(ProductCreateVM model)
		{
			if (!ModelState.IsValid)
			{
				model.Categories = _context.Categories.Select(c => new SelectListItem
				{
					Text = c.Name,
					Value = c.Id.ToString()
				}).ToList();
				return View(model);
			}

			var existingProduct = _context.Products.FirstOrDefault(w => w.Name.ToLower() == model.Name.ToLower());
			if (existingProduct != null)
			{
				ModelState.AddModelError("Name", "A product with the same name already exists.");
				model.Categories = _context.Categories.Select(c => new SelectListItem
				{
					Text = c.Name,
					Value = c.Id.ToString()
				}).ToList();
				return View(model);
			}

			var product = new Product
			{
				CategoryId = model.CategoryId,
				Name = model.Name,
				PhotoPath = model.PhotoPath,
				Price = model.Price,
				Size = model.Size
			};

			_context.Products.Add(product);
			_context.SaveChanges();
			return RedirectToAction(nameof(Index));
		}

		#region Update
		[HttpGet]
		public IActionResult Update(int id)
		{
			var product = _context.Products.Find(id);
			if (product == null)
			{
				return NotFound();
			}

			var model = new ProductUpdateVM
			{
				Name = product.Name,
				PhotoPath = product.PhotoPath,
				Price = product.Price,
				Size = product.Size,
				CategoryId = product.CategoryId,
				Categories = _context.Categories.Select(c => new SelectListItem
				{
					Text = c.Name,
					Value = c.Id.ToString()
				}).ToList()
			};

			return View(model);
		}


		[HttpPost]
		public IActionResult Update(int id, ProductUpdateVM model) 
		{
			var product = _context.Products.Find(id);
			var category = _context.Categories.Find(product.CategoryId);

			product.Name = model.Name;
			product.PhotoPath = model.PhotoPath;
			product.Price = model.Price;
			product.Size = model.Size;
			product.CategoryId = category.Id;
			product.ModifiedAt = DateTime.Now;

			_context.Products.Update(product);
			_context.SaveChanges();

			return RedirectToAction("Index");
		
		}
		#endregion

		#region Delete

		[HttpPost]
		public IActionResult Delete(int id)
		{
			var product = _context.Products.Find(id);
			if (product is null) return NotFound();

			_context.Products.Remove(product);
			_context.SaveChanges();

			return RedirectToAction(nameof(Index));
		}

		#endregion
	}
}
