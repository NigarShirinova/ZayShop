using Microsoft.AspNetCore.Mvc.Rendering;
using ZayShop.Entities;
namespace ZayShop.Models.Shop
{
    public class ShopIndexVM
    {
        

       public List<Category> Categories { get; set; }
       public List<Product> Products { get; set; }


	}
}
