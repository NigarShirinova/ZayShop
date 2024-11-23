using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ZayShop.Areas.Admin.Models.Category;
using ZayShop.Areas.Admin.Models.Product;
using ZayShop.Areas.Admin.Models.Work;
using ZayShop.Data;
using ZayShop.Entities;
using ZayShop.Utilities.File;

namespace ZayShop.Areas.Admin.Controllers
{
	[Area("admin")]
	public class ProductController : Controller
	{
		private readonly AppDbContext _context;
		private readonly IFileService _fileService;

		private List<SelectListItem> GetCategories()
		{
			return _context.Categories.Select(c => new SelectListItem
			{
				Text = c.Name,
				Value = c.Id.ToString()
			}).ToList();
		}

		public ProductController(AppDbContext context, IFileService fileService)
		{
			_context = context;
			_fileService = fileService;
		}

		[HttpGet]
		public IActionResult Index()
		{
			var model = new ProductIndexVM
			{
				Products = _context.Products.ToList()
			};

			return View(model);
		}

		[HttpGet]
		public IActionResult Create()
		{
			var model = new ProductCreateVM
			{
				Categories = GetCategories(),
			};

			return View(model);
		}

		[HttpPost]
		public IActionResult Create(ProductCreateVM model)
		{
			if (!ModelState.IsValid || model.CategoryId == null)
			{
				model.Categories = GetCategories();
				if (model.CategoryId == null)
				{
					ModelState.AddModelError("CategoryId", "Please select a category.");
				}
				return View(model);
			}

			if (!_fileService.IsImage(model.Photo.ContentType))
			{
				ModelState.AddModelError("Photo", "This is not an image.");
				return View(model);
			}

			if (!_fileService.IsAvailableSize(model.Photo.Length))
			{
				ModelState.AddModelError("Photo", "Image's volume is higher than 100kb.");
				return View(model);
			}

			var photoName = _fileService.Upload(model.Photo, "assets/img");

			if (_context.Products.Any(w => w.Name.ToLower() == model.Name.ToLower()))
			{
				ModelState.AddModelError("Name", "A product with the same name already exists.");
				model.Categories = GetCategories();
				return View(model);
			}

			var product = new Product
			{
				CategoryId = model.CategoryId,
				Name = model.Name,
				Price = model.Price,
				Size = model.Size,
				PhotoPath = photoName,
				CreatedAt = DateTime.Now
			};

			_context.Products.Add(product);
			_context.SaveChanges();
			return RedirectToAction(nameof(Index));
		}

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
				Price = product.Price,
				Size = product.Size,
				CategoryId = product.CategoryId,
				Categories = GetCategories()
			};

			return View(model);
		}

		[HttpPost]
		public IActionResult Update(int id, ProductUpdateVM model)
		{
			var product = _context.Products.Find(id);
			if (product == null)
			{
				return NotFound();
			}

			if (!ModelState.IsValid)
			{
				model.Categories = GetCategories();
				return View(model);
			}

			if (model.Photo != null)
			{
				if (!_fileService.IsImage(model.Photo.ContentType))
				{
					ModelState.AddModelError("Photo", "This is not an image.");
					model.Categories = GetCategories();
					return View(model);
				}

				if (!_fileService.IsAvailableSize(model.Photo.Length))
				{
					ModelState.AddModelError("Photo", "Image's volume is higher than 100kb.");
					model.Categories = GetCategories();
					return View(model);
				}

				_fileService.Delete(product.PhotoPath, "assets/img");
				product.PhotoPath = _fileService.Upload(model.Photo, "assets/img");
			}

			product.Name = model.Name;
			product.Price = model.Price;
			product.Size = model.Size;
			product.CategoryId = model.CategoryId;
			product.ModifiedAt = DateTime.Now;

			_context.Products.Update(product);
			_context.SaveChanges();

			return RedirectToAction(nameof(Index));
		}

		[HttpPost]
		public IActionResult Delete(int id)
		{
			var product = _context.Products.Find(id);
			if (product == null)
			{
				return NotFound();
			}

			_fileService.Delete(product.PhotoPath, "assets/img");
			_context.Products.Remove(product);
			_context.SaveChanges();

			return RedirectToAction(nameof(Index));
		}
	}
}
