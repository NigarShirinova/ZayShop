using ZayShop.Entities;

namespace ZayShop.Models.Shop
{
	public class CategoryVM
	{
		public string Name { get; set; }
		public ICollection<Product> Products { get; set; }
	}
}
