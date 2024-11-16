using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace ZayShop.Areas.Admin.Models.Product
{
	public class ProductUpdateVM
	{
		public string Name { get; set; }
		public string Size { get; set; }
		public decimal Price { get; set; }
		public string PhotoPath { get; set; }
		public List<SelectListItem> Categories { get; set; }
		public int CategoryId { get; set; }

	}
}
