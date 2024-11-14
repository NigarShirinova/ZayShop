using System.ComponentModel.DataAnnotations;

namespace ZayShop.Areas.Admin.Models.Slider
{
    public class SliderUpdateVM
    {
        [Display(Name = "Title")]
        public string Name { get; set; }
        public string Description1 { get; set; }
        public string Description2 { get; set; }
        public string PhotoPath { get; set; }
    }
}
