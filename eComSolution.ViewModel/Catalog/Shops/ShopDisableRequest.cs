using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace eComSolution.ViewModel.Catalog.Shops
{
    public class ShopDisableRequest
    {
        public int ShopId { get; set; }
        public string DisableReason { get; set; }
    }
}