using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace OrdersAPI.ViewModels
{
    public class CheckOutRequest
    {
        //public int UserId { get; set; }
        //public int ShopId { get; set; }
        public List<int> CartIds { get; set; } 
        public string ShipName { get; set; }
        public string ShipAddress { get; set; }
        public string ShipPhone { get; set; }

        public bool IsValid()
        {
            // valid null or empty
            if(CartIds.Count==0 || String.IsNullOrEmpty(ShipName) || String.IsNullOrEmpty(ShipAddress)
                                || String.IsNullOrEmpty(ShipPhone) || CartIds.Contains(0))
            {
                return false;
            } 
            // valid ship phone
            if(!Regex.Match(ShipPhone, @"^([\+]?61[-]?|[0])?[1-9][0-9]{8}$").Success)
            {
                return false;
            }

            return true;
        }        
    }
}