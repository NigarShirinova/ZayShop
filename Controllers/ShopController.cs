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
    }
}
