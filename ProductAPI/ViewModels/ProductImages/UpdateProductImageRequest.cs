using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductAPI.ViewModels.ProductImages
{
    public class UpdateProductImageRequest
    {
        public int Id { get; set; } // id của product
        public bool IsDefault { get; set; }
        public int SortOrder { get; set; }
        public string ColorName { get; set; }   // nullable
        public bool IsSizeDetail { get; set; }
        public IFormFile ImageFile { get; set; }
    }
}
