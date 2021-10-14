using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eComSolution.Service.Catalog.Products;
using eComSolution.Service.System.Users;
using eComSolution.ViewModel.Catalog.ProductImages;
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

        [HttpPost]
        //[Consumes("multipart/form-data")]
        public async Task<IActionResult> Create([FromBody]CreateProductRequest request) 
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _productService.Create(request);
            if (result.IsSuccessed == false)
                return BadRequest(result.Message);    

            return Ok(result);
        }

        [HttpPost("{productId}/images")]
        public async Task<IActionResult> AddImage(int productId, [FromForm]CreateProductImageRequest request) 
        {
            // if (!ModelState.IsValid)
            //     return BadRequest(ModelState);

            var result = await _productService.AddImage(productId, request);
            if (result.IsSuccessed == false)
                return BadRequest(result.Message);

            return Ok(result);
        }

    }
}