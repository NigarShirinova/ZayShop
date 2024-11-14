using Microsoft.AspNetCore.Mvc;

namespace ZayShop.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
