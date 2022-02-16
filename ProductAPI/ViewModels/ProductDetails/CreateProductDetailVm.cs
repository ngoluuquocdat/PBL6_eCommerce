using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductAPI.ViewModels.ProductDetails
{
    public class CreateProductDetailVm
    {
        public string Color { get; set; }
        public string Size { get; set; }
        public int Stock { get; set; }

        public bool IsValid()
        {
            // null or empty check
            if(String.IsNullOrEmpty(Color) || String.IsNullOrEmpty(Size) || Stock==0)
            {
                return false;
            }
            // valid stock 
            if(Stock<0)
            {
                return false;
            }

            return true;
        }
    }
}