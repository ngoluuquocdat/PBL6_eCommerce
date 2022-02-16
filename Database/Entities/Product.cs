using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Database.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Gender { get; set; }
        public int Price { get; set; }
        public int OriginalPrice { get; set; }
        public int ViewCount { get; set; }
        public DateTime DateCreated { get; set; }
        public int CategoryId { get; set; }
        public int ShopId { get; set; }
        public bool IsDeleted { get; set; }


        // navigation prop
        public Category Category {get; set;}    // 1 category
        public Shop Shop {get; set;}    // 1 shop
        public List<ProductDetail> ProductDetails { get; set; }     // many Product Details
        public List<ProductImage> ProductImages { get; set; }       // many product images
        public List<History> Histories {get; set;}                  // many histories
    }
}