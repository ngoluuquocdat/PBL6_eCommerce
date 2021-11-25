using System;
using System.Collections.Generic;
using ProductAPI.ViewModels.ProductDetails;

namespace ProductAPI.ViewModels.Products
{
    public class CreateProductRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Gender {get; set;}
        public int Price { get; set; }
        public int OriginalPrice { get; set; }
        //public int ViewCount { get; set; }
        //public DateTime DateCreated { get; set; }
        public int CategoryId { get; set; }
        //public int ShopId { get; set; }
        //public bool IsDeleted { get; set; }
        public List<CreateProductDetailVm> Details {get; set;}
        //public List<CreateProductImagesRequest> NewImages { get; set; }
        // public List<ImageInfo> ImageInfos {get; set;}
        // public List<IFormFile> NewImages {get; set;}
        public bool IsValid()
        {
            // null or empty check
            if(String.IsNullOrEmpty(Name) || String.IsNullOrEmpty(Description)
                        || Gender==0 || Price==0 || OriginalPrice==0 || CategoryId==0)
            {
                return false;
            }
            // valid gender 
            if(Gender!=1 && Gender!=2 && Gender!= 3)
            {
                return false;
            }
            // valid price & original price
            if(Price<0 || OriginalPrice<0)
            {
                return false;
            }
            // valid product details
            foreach(var detail in Details)
            {
                if(detail.IsValid()==false)
                    return false;
            }

            return true;
        }
    }
}