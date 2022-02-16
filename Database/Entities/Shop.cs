using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Database.Entities
{
    public class Shop
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Avatar {get; set;}    // avatar image path
        public string PhoneNumber { get; set; } 
        public string Address { get; set; }
        public string Description { get; set; }
        public bool Disable { get; set; }
        public string DisableReason { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }


        // navigation prop
        public User User;    // 1 user - shop owner
        public List<Product> Products { get; set; } // many products
        public List<Order> Orders {get; set;}   //many orders
    }
}