using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eComSolution.Data.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public int UserId { get; set; }     // who make this order
        public int ShopId { get; set; }     // order for which shop
        public string State {get; set;}
        public string ShipName { get; set; }
        public string ShipAddress { get; set; }
        public string ShipPhone { get; set; }

        // navigation prop
        public User User { get; set; }     // 1 user
        public Shop Shop { get; set; }     // 1 shop
        public List<OrderDetail> OrderDetails {get; set;}   // many order details
    }
}