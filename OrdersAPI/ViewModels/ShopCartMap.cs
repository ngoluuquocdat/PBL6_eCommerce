using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrdersAPI.ViewModels
{
    public class ShopCartMap
    {
        public int ShopId {get; set;}
        public List<int> CartIds { get; set; }

        public override string ToString()
        {
            string result = "ShopId: "+ShopId+"; CartId:";
            foreach(int cartId in CartIds)
            {
                result += " "+cartId;
            }
            return result;
        }
    }
}