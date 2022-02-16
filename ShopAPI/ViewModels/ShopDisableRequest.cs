using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ShopAPI.ViewModels
{
    public class ShopDisableRequest
    {
        public int ShopId { get; set; }
        public string DisableReason { get; set; }
    }
}