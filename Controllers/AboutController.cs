using Microsoft.AspNetCore.Mvc;
using ZayShop.Data;
using ZayShop.Models.About;

namespace ZayShop.Controllers
{
    public class AboutController : Controller
    {

        private readonly AppDbContext _context;
        public AboutController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
