using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ProductAPI.ViewModels.ProductImages
{
    public class CreateProductImageRequest
    {
        public bool IsDefault { get; set; }
        public int SortOrder { get; set; }
        public string ColorName { get; set; }   // nullable
        public bool IsSizeDetail {get; set;}
        public IFormFile ImageFile {get; set;}
    }
}