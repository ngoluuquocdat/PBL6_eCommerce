using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eComSolution.ViewModel.Catalog.ProductDetails;

namespace eComSolution.ViewModel.Catalog.Carts
{
    public class AddToCartRequest
    {
        public int ProductDetail_Id { get; set; }
        public int Quantity { get; set; }
        // public int Price { get; set; }
        public bool IsValid()
        {
            if(ProductDetail_Id==0 || Quantity==0 || Quantity<0)
            {
                return false;
            }

            return true;
        }
    }
}