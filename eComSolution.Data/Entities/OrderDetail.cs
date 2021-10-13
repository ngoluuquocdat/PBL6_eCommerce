using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eComSolution.Data.Entities
{
    public class OrderDetail
    {
        public int Id { get; set; }
        public int OrderId {get; set;}
        public int Price { get; set; }      // đơn giá
        public int Quantity {get; set;}
        public int ProductDetail_Id {get; set;}

        // navigation prop:
        public Order Order { get; set; }    // 1 order
        public ProductDetail ProductDetail { get; set; }    // 1 product detail
    }
}