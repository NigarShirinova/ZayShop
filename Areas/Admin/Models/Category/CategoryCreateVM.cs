using System.ComponentModel.DataAnnotations;

namespace ZayShop.Areas.Admin.Models.Category
{
    public class CategoryCreateVM
    {
        [Display(Name = "Title")]
        public string Name { get; set; }
    }
}
