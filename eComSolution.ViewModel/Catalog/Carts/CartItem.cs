using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eComSolution.ViewModel.Catalog.Carts
{
    public class CartItem
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public int ProductDetail_Id { get; set; }
        public int ShopId { get; set; }
        public string ProductName { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }
        public string ShopName {get; set;}
        public int Quantity { get; set; }   // số lượng đặt mua
        public int Stock { get; set; }      // số lượng tồn kho
        public int Price { get; set; }      // đơn giá
        public string Image {get; set;}
    }
}