using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductAPI.ViewModels.Products
{
    public class UpdateProductMainInfoRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Gender {get; set;}
        public int Price { get; set; }
        public int OriginalPrice { get; set; }
        public int CategoryId { get; set; }
        
        public bool IsValid()
        {
            // null or empty check
            if(String.IsNullOrEmpty(Name) || String.IsNullOrEmpty(Description)
                        || Id==0 || Gender==0 || Price==0 || OriginalPrice==0 || CategoryId==0)
            {
                return false;
            }
            // valid gender 
            if(Gender!=1 && Gender!=2 && Gender!= 3)
            {
                return false;
            }
            // valid price & original price
            if(Price<0 || OriginalPrice<0)
            {
                return false;
            }

            return true;
        }
    }
}