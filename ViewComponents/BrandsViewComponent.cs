using Microsoft.AspNetCore.Mvc;
using ZayShop.Data;

namespace ZayShop.ViewComponents
{
    public class BrandsViewComponent : ViewComponent
    {
        private readonly AppDbContext _context;
        public BrandsViewComponent(AppDbContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            var brands = _context.Brands.ToList();
            return View(brands);
        }
    }
}
