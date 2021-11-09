using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eComSolution.ViewModel.Catalog.ProductDetails;
using eComSolution.ViewModel.Catalog.ProductImages;

namespace eComSolution.ViewModel.Catalog.Products
{
    public class ProductVm
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Gender { get; set; }
        public int Price { get; set; }
        public int OriginalPrice { get; set; }
        public int ViewCount { get; set; }
        public DateTime DateCreated { get; set; }
        public string CategoryName { get; set; }
        public int ShopId { get; set; }
        public string ShopName {get; set;}
        public int TotalStock {get; set;}
        public List<ProductDetailVm> Details {get; set;}
        public List<ProductImageVm> Images {get; set;}       
    }
}