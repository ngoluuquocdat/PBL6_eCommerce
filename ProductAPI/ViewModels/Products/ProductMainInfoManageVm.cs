using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace ProductAPI.ViewModels.Products
{
    public class ProductMainInfoManageVm
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int OriginalPrice { get; set; }
        public int Price { get; set; }
        public DateTime DateCreated {get; set;}
        public int TotalStock {get; set;}
        //public string ThumbnailImage {get; set;}      
    }
}