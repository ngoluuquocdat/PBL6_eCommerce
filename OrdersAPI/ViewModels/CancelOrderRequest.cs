using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrdersAPI.ViewModels
{
    public class CancelOrderRequest
    {
        public int OrderId { get; set; }
        public string CancelReason { get; set; }

        public bool IsValid()
        {
            if(OrderId==0)
                return false;
            
            return true;
        }
    }
}