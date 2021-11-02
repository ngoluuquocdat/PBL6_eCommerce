using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace eComSolution.ViewModel.Catalog.Shops
{
    public class ShopVm
    {
        public string Name { get; set; }
        public string Avatar {get; set;} 
        public string PhoneNumber { get; set; } 
        public string Address { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }
        public bool Disable { get; set; }
    }
}