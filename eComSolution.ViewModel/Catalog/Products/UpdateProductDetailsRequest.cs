using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eComSolution.ViewModel.Catalog.ProductDetails;
using eComSolution.ViewModel.Catalog.ProductImages;
using Microsoft.AspNetCore.Http;

namespace eComSolution.ViewModel.Catalog.Products
{
    public class UpdateProductDetailsRequest
    {
        public int Id { get; set; }
        public List<UpdateProductDetailVm> Details {get; set;}
        
        public bool IsValid()
        {
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