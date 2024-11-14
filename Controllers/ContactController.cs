using Microsoft.AspNetCore.Mvc;

namespace ZayShop.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
