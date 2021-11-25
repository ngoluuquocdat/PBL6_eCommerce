using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrdersAPI.ViewModels
{
    public class OrderDetailVm
    {
        public int OrderId {get; set;}
        public int ProductDetail_Id {get; set;}
        public int Price { get; set; }      // đơn giá
        public int Quantity {get; set;}
        public string ProductName { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }
        public string Image {get; set;}
    }
}