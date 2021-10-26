using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eComSolution.ViewModel.Catalog.ProductDetails;
using eComSolution.ViewModel.Catalog.ProductImages;
using Microsoft.AspNetCore.Http;

namespace eComSolution.ViewModel.Catalog.Products
{
    public class UpdateProductRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Gender {get; set;}
        public int Price { get; set; }
        public int OriginalPrice { get; set; }
        public int CategoryId { get; set; }
        public List<ProductDetailVm> Details {get; set;}
        
    }
}