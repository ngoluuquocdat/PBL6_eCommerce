using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eComSolution.Data.Entities;
using eComSolution.ViewModel.Catalog.Orders;
using eComSolution.ViewModel.Catalog.ProductDetails;

namespace eComSolution.ViewModel.Catalog.Carts
{
    public class CheckOutRequest
    {
        //public int UserId { get; set; }
        //public int ShopId { get; set; }
        public List<int> CartIds { get; set; } 
        public string ShipName { get; set; }
        public string ShipAddress { get; set; }
        public string ShipPhone { get; set; }

        //public List<int> ProductDetail_Ids { get; set; }        
    }
}