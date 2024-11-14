using Microsoft.AspNetCore.Mvc;
using ZayShop.Areas.Admin.Models.Slider;
using ZayShop.Data;

namespace ZayShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SliderController : Controller
    {
        private readonly AppDbContext _context;

        public SliderController(AppDbContext context)
        {
            _context = context;
        }

        #region List

        [HttpGet]
        public IActionResult Index()
        {
            var model = new SliderIndexVM
            {
                Sliders = _context.Sliders.ToList()
            };

            return View(model);
        }

        #endregion

        #region Create

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(SliderCreateVM model)
        {
            if (!ModelState.IsValid) return View();

            var slider = _context.Sliders.FirstOrDefault(wc => wc.Name.ToLower() == model.Name.ToLower());
           

            slider = new Entities.Slider
            {
                Name = model.Name,
                Description1 = model.Description1,
                Description2 = model.Description2,
                PhotoPath = model.PhotoPath
            };

            _context.Sliders.Add(slider);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        #endregion


        #region Update

        [HttpGet]
        public IActionResult Update(int id)
        {
            var slider = _context.Sliders.Find(id);
            

            var model = new SliderUpdateVM
            {
                Name = slider.Name,
                Description1 = slider.Description1,
                Description2 = slider.Description2,
                PhotoPath = slider.PhotoPath
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Update(int id, SliderUpdateVM model)
        {
            if (!ModelState.IsValid) return View();

            var workCategory = _context.Sliders.Find(id);
            

            workCategory.ModifiedAt = DateTime.Now;

            workCategory.Name = model.Name;

            _context.Sliders.Update(workCategory);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        #endregion
        #region Delete

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var slider = _context.Sliders.Find(id);
            if (slider is null) return NotFound();

            _context.Sliders.Remove(slider);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
 
        #endregion
    }
}