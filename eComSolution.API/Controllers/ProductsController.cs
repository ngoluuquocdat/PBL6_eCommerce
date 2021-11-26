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
    [Authorize]
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
            try
            {
                var result = await _productService.GetProductPaging(request);
                if (result.ResultObj == null || result.ResultObj.Items.Count==0)
                    return NoContent();  
                return Ok(result);
            }
            catch(Exception ex)
            {
                Console.WriteLine(DateTime.Now + "- Server Error: " + ex);
                return StatusCode(500, "Lỗi server");
            }     
        }  

        [HttpGet("{productId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetProductById(int productId)
        {
            try
            {
                var result = await _productService.GetProductById(productId);
                if(result.IsSuccessed==false)   return NoContent();
                // if (result.ResultObj == null || result.ResultObj.Count==0)
                //     return NoContent();
                return Ok(result);
            }
            catch(Exception ex)
            {
                Console.WriteLine(DateTime.Now + "- Server Error: " + ex);
                return StatusCode(500, "Lỗi server");
            } 
        }  

        [HttpPost]
        //[Consumes("multipart/form-data")]
        public async Task<IActionResult> Create([FromBody]CreateProductRequest request) 
        {
            try
            {
                var claimsPrincipal = this.User;
                var userId = Int32.Parse(claimsPrincipal.FindFirst("id").Value);

                var result = await _productService.Create(userId, request);
                if (result.IsSuccessed == false)
                    return BadRequest(result.Message);

                return Ok(result);
            }
            catch(Exception ex)
            {
                Console.WriteLine(DateTime.Now + "- Server Error: " + ex);
                return StatusCode(500, "Lỗi server");
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int productId) 
        {
            try
            {
                var claimsPrincipal = this.User;
                var userId = Int32.Parse(claimsPrincipal.FindFirst("id").Value);
            
                var result = await _productService.Delete(userId, productId);
                if (result.IsSuccessed == false)
                    return BadRequest(result.Message);    

                return Ok(result);
            }
            catch(Exception ex)
            {
                Console.WriteLine(DateTime.Now + "- Server Error: " + ex);
                return StatusCode(500, "Lỗi server");
            } 
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody]UpdateProductRequest request) 
        {
            try
            {
                var claimsPrincipal = this.User;
                var userId = Int32.Parse(claimsPrincipal.FindFirst("id").Value);

                var result = await _productService.Update(userId, request);
                if (result.IsSuccessed == false)
                    return BadRequest(result.Message);    

                return Ok(result);
            }
            catch(Exception ex)
            {
                Console.WriteLine(DateTime.Now + "- Server Error: " + ex);
                return StatusCode(500, "Lỗi server");
            } 
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateMainInfo([FromBody]UpdateProductMainInfoRequest request) 
        {
            try
            {
                var claimsPrincipal = this.User;
                var userId = Int32.Parse(claimsPrincipal.FindFirst("id").Value);

                var result = await _productService.UpdateMainInfo(userId, request);
                if (result.IsSuccessed == false)
                    return BadRequest(result.Message);    

                return Ok(result);
            }
            catch(Exception ex)
            {
                Console.WriteLine(DateTime.Now + "- Server Error: " + ex);
                return StatusCode(500, "Lỗi server");
            }
            
        }

        [HttpPatch("details")]
        public async Task<IActionResult> UpdateProductDetails([FromBody]UpdateProductDetailsRequest request) 
        {
            try
            {
                var claimsPrincipal = this.User;
                var userId = Int32.Parse(claimsPrincipal.FindFirst("id").Value);

                var result = await _productService.UpdateProductDetails(userId, request);
                if (result.IsSuccessed == false)
                    return BadRequest(result.Message);    

                return Ok(result);
            }
            catch(Exception ex)
            {
                Console.WriteLine(DateTime.Now + "- Server Error: " + ex);
                return StatusCode(500, "Lỗi server");
            }
        }

        [HttpPost("{productId}/images")]
        public async Task<IActionResult> AddImage(int productId, [FromForm]CreateProductImageRequest request) 
        {
            try
            {
                var result = await _productService.AddImage(productId, request);
                if (result.IsSuccessed == false)
                    return BadRequest(result.Message);

                return Ok(result);
            }
            catch(Exception ex)
            {
                Console.WriteLine(DateTime.Now + "- Server Error: " + ex);
                return StatusCode(500, "Lỗi server");
            }
        }

        [HttpDelete("{productId}/images/{imageId}")]
        public async Task<IActionResult> DeleteImage(int imageId)
        {
            try
            {
                var result = await _productService.RemoveImage(imageId);
                if (result.IsSuccessed == false)
                    return BadRequest(result.Message);

                return Ok(result);
            }
            catch(Exception ex)
            {
                Console.WriteLine(DateTime.Now + "- Server Error: " + ex);
                return StatusCode(500, "Lỗi server");
            }
        }
    }
}