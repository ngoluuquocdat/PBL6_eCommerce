using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Database.Entities
{
    public class ProductDetail
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }
        public int Stock { get; set; }
        public bool IsDeleted {get; set;}

        // navigation prop
        public Product Product { get; set; }    // 1 product
        public List<Cart> Carts { get; set; }   // many carts
        public List<OrderDetail> OrderDetails { get; set; }     // many order details

    }
}