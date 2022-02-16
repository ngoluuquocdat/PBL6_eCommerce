using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductAPI.ViewModels.ProductImages
{
    public class ImageInfo
    {
        public bool IsDefault { get; set; }
        public int SortOrder { get; set; }
        public string ColorName { get; set; }   // nullable
        public bool IsSizeDetail {get; set;}
    }
}