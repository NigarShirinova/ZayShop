using Microsoft.AspNetCore.Mvc;
using ZayShop.Data;
using ZayShop.Models.Home;

namespace ZayShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var sliders = _context.Sliders.OrderBy(x => x.Id).ToList();

            var slidersList = new List<SliderVM>();
            foreach (var slider in sliders)
            {
                var sliderVM = new SliderVM
                {
                    Name = slider.Name,
                    Description1 = slider.Description1,
                    PhotoPath = slider.PhotoPath,
                    Description2 = slider.Description2  
                };

                slidersList.Add(sliderVM);
            }

            var model = new HomeIndexVM
            {
                Sliders = slidersList
            };

            return View(model);
        }
    }
}
