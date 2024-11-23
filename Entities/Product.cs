﻿using Microsoft.AspNetCore.Mvc.Rendering;

namespace ZayShop.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string Size { get; set; }
        public decimal Price { get; set; }
        public string PhotoPath { get; set; }
        public Category Category { get; set; }
        public int CategoryId { get; set; }
    }
}
