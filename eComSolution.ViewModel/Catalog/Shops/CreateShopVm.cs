using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace eComSolution.ViewModel.Catalog.Shops
{
    public class CreateShopVm
    {
        public string Name { get; set; }
        public IFormFile ImageFile {get; set;} 
        public string PhoneNumber { get; set; } 
        public string Address { get; set; }
        public string Description { get; set; }

    }
}