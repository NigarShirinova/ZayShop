using ZayShop.Entities;

namespace ZayShop.Models.Shop
{
	public class ProductVM
	{
		public string Name { get; set; }
		public string Size { get; set; }
		public decimal Price { get; set; }
		public string PhotoPath { get; set; }
		public Category Category { get; set; }
		public int CategoryId { get; set; }
	}
}
