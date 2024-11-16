using System.ComponentModel.DataAnnotations;

namespace ZayShop.Areas.Admin.Models.Category
{
    public class CategoryUpdateVM
    {
        [Display(Name = "Title")]
        public string Name { get; set; }
    }
}
