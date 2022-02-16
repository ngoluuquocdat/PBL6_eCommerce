using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Database.Entities
{
    public class History
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public DateTime Date { get; set; }
        public int Count {get; set;}    // how many time user view this product


        // navigation prop  
        public User User { get; set; }      // 1 user
        public Product Product { get; set; }    // 1 product
    }
}