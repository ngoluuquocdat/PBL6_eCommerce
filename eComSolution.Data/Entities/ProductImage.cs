using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eComSolution.Data.Entities
{
    public class ProductImage
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ImagePath {get; set; }
        public bool IsDefault { get; set; }
        public int SortOrder { get; set; }
        public string ColorName { get; set; }   // nullable
        public bool IsSizeDetail {get; set;}
        //public bool IsDeleted {get; set;}

        // navigation prop
        public Product Product { get; set; }    // 1 product
    }
}