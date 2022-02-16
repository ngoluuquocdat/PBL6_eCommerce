using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eComSolution.ViewModel.Catalog.Products
{
    public class GetProductsManageRequest
    {
        public int PageIndex { get; set; }  // index của trang, VD: page 1, page 2,...
        public int PageSize { get; set; }   // kích cỡ của trang
        public int ProductId { get; set; }  // id sản phẩm
        public string Keyword { get; set; }          // keyword để tìm kiếm    
    }
}