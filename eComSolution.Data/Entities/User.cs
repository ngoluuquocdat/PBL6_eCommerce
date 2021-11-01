using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eComSolution.Data.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address {get; set;}
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public int? ShopId {get; set;}      // null if this user don't own a shop
        public bool Disable { get; set; }
        

        // navigation prop
        public Shop Shop {get; set;}    // 1 shop
        public List<Order> Orders { get; set; }     // many orders
        public List<Cart> Carts { get; set; }       // many carts
        public List<History> Histories { get; set; }    // many history
        public List<GroupUser> GroupUsers { get; set; }     // many group users
    }
}