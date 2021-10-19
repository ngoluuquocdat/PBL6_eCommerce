using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eComSolution.Service.Catalog.Products;
using eComSolution.Service.System.Users;
using eComSolution.ViewModel.Catalog.ProductImages;
using eComSolution.ViewModel.Catalog.Products;
using eComSolution.ViewModel.System.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eComSolution.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("paging")]
        [AllowAnonymous]
        public async Task<IActionResult> GetProductPaging([FromQuery]GetProductsRequest request)
        {
            var result = await _productService.GetProductPaging(request);
            return Ok(result);
        }  

        [HttpGet("{productId}")]
        //[AllowAnonymous]
        public async Task<IActionResult> GetProductById(int productId)
        {
            var result = await _productService.GetProductById(productId);
            if(result.IsSuccessed==false)   return BadRequest(result.Message);
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

        [HttpDelete]
        public async Task<IActionResult> Delete(int productId) 
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _productService.Delete(productId);
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