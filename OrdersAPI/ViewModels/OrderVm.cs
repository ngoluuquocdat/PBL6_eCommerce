using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrdersAPI.ViewModels
{
    public class OrderVm
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime DateModified {get; set;}
        public int UserId { get; set; }     // who make this order
        public int ShopId { get; set; }     // order for which shop
        public string ShopName { get; set; }
        public string State {get; set;}
        public string CancelReason {get; set;}
        public string ShipName { get; set; }
        public string ShipAddress { get; set; }
        public string ShipPhone { get; set; }
        public List<OrderDetailVm> OrderDetails {get; set;}
        public int TotalPrice {get; set;}
    }
}