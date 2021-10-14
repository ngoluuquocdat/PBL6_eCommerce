using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eComSolution.ViewModel.Catalog.ProductDetails;
using eComSolution.ViewModel.Catalog.ProductImages;
using Microsoft.AspNetCore.Http;

namespace eComSolution.ViewModel.Catalog.Products
{
    public class CreateProductRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int OriginalPrice { get; set; }
        //public int ViewCount { get; set; }
        //public DateTime DateCreated { get; set; }
        public int CategoryId { get; set; }
        public int ShopId { get; set; }
        //public bool IsDeleted { get; set; }
        public List<ProductDetailVm> Details {get; set;}
        //public List<CreateProductImagesRequest> NewImages { get; set; }
        // public List<ImageInfo> ImageInfos {get; set;}
        // public List<IFormFile> NewImages {get; set;}
    }
}