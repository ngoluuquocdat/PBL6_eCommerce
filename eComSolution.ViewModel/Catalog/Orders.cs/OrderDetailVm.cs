using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eComSolution.ViewModel.Catalog.Orders
{
    public class OrderDetailVm
    {
        public int OrderId {get; set;}
        public int ProductDetail_Id {get; set;}
        public int Price { get; set; }      // đơn giá
        public int Quantity {get; set;}
    }
}