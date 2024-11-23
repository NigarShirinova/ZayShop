using Microsoft.AspNetCore.Mvc.Rendering;

namespace ZayShop.Areas.Admin.Models.Work
{
    public class ProductCreateVM
    {
        public string Name { get; set; }
        public string Size { get; set; }
        public decimal Price { get; set; }
		public IFormFile Photo { get; set; }

		public List<SelectListItem> Categories { get; set; }
        public int CategoryId { get; set; }
    }
}
