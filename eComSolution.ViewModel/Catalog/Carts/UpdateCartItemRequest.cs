using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eComSolution.ViewModel.Catalog.ProductDetails;

namespace eComSolution.ViewModel.Catalog.Carts
{
    public class UpdateCartItemRequest
    {
        public int CartId { get; set; }
        public int Quantity { get; set; }
    }
}