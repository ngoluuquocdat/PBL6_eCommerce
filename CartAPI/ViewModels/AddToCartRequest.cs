using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CartAPI.ViewModels
{
    public class AddToCartRequest
    {
        public int ProductDetail_Id { get; set; }
        public int Quantity { get; set; }

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