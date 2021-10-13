using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eComSolution.Data.Entities
{
    public class Cart
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ProductDetail_Id { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }      // đơn giá lúc thêm vào giỏ


        // navigation prop
        public User User { get; set; }      // 1 user
        public ProductDetail ProductDetail { get; set; }    // 1 product detail 
    }
}