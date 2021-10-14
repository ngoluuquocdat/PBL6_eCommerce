using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eComSolution.Service.Catalog.Products;
using eComSolution.Service.System.Users;
using eComSolution.ViewModel.Catalog.Products;
using eComSolution.ViewModel.System.Users;
using Microsoft.AspNetCore.Mvc;

namespace eComSolution.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("paging")]
        public async Task<IActionResult> GetProductPaging([FromQuery]GetProductsRequest request)
        {
            var result = await _productService.GetProductPaging(request);
            return Ok(result);
        }      
    }
}