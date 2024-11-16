using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZayShop.Areas.Admin.Models.Category;
using ZayShop.Areas.Admin.Models.Slider;
using ZayShop.Data;

namespace ZayShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly AppDbContext _context;

        public CategoryController(AppDbContext context)
        {
            _context = context;
        }
        #region List

        [HttpGet]
        public IActionResult Index()
        {
            var model = new CategoryIndexVM
            {
                Categories = _context.Categories.ToList()
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
        public IActionResult Create(CategoryCreateVM model)
        {
            if (!ModelState.IsValid) return View();

            var category = _context.Categories.FirstOrDefault(c => c.Name.ToLower() == model.Name.ToLower());


            category = new Entities.Category
            {
                Name = model.Name
                
            };

            _context.Categories.Add(category);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        #endregion


        #region Update

        [HttpGet]
        public IActionResult Update(int id)
        {
            var category = _context.Categories.Find(id);


            var model = new CategoryUpdateVM
            {
                Name = category.Name
               
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Update(int id, CategoryUpdateVM model)
        {
            if (!ModelState.IsValid) return View();

            var category = _context.Categories.Find(id);


            category.ModifiedAt = DateTime.Now;

            category.Name = model.Name;

            _context.Categories.Update(category);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        #endregion

        #region Delete

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var category = _context.Categories.Find(id);
            if (category is null) return NotFound();

            _context.Categories.Remove(category);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        #endregion
    }
}
