using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZayShop.Data;
using ZayShop.Models.Home;
using ZayShop.Models.Shop;

namespace ZayShop.Controllers
{
    public class ShopController : Controller
    {
        private readonly AppDbContext _context;
        public ShopController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
         
            var model = new ShopIndexVM
            {
				Products = _context.Products.Include(c => c.Category).ToList(),
			};


            return View(model);
        }

        [HttpPost]
        public IActionResult FilterProducts(int categoryId)
        {
            var products = _context.Products
                .Where(p => p.CategoryId == categoryId)
                .Select(p => new
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    PhotoPath = p.PhotoPath,
                    Size = p.Size
                })
                .ToList();

            return Json(products);
        }
    }
}
