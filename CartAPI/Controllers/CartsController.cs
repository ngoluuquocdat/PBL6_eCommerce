using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CartAPI.Services;
using CartAPI.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CartAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CartsController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartsController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet("me")]
        public async Task<IActionResult> GetCart()
        {
            try
            {
                var claimsPrincipal = this.User;
                var userId = Int32.Parse(claimsPrincipal.FindFirst("id").Value);

                var result = await _cartService.GetCartItems(userId);

                if (result.IsSuccessed == false)
                    return NoContent();        

                return Ok(result);
            }
            catch(Exception ex)
            {
                Console.WriteLine(DateTime.Now + "- Server Error: " + ex);
                return StatusCode(500, "Lỗi server");
            }            
        }

        [HttpGet]
        public async Task<IActionResult> GetCartItemsByIds([FromQuery] List<int> cartIds)
        {
            try
            {
                var claimsPrincipal = this.User;
                var userId = Int32.Parse(claimsPrincipal.FindFirst("id").Value);

                var result = await _cartService.GetCartItemsByIds(userId, cartIds);

                if (result.ResultObj.Count == 0 || result.ResultObj==null)
                    return NoContent();        

                return Ok(result);
            }
            catch(Exception ex)
            {
                Console.WriteLine(DateTime.Now + "- Server Error: " + ex);
                return StatusCode(500, "Lỗi server");
            }            
        } 

        [HttpGet("count")]
        public async Task<IActionResult> GetCartCount()
        {
            try
            {
                var claimsPrincipal = this.User;
                var userId = Int32.Parse(claimsPrincipal.FindFirst("id").Value);

                var result = await _cartService.GetCartItemsCount(userId);

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
        [HttpPost]
        public async Task<IActionResult> AddCart(AddToCartRequest request)
        {
            try
            {
                var claimsPrincipal = this.User;
                var userId = Int32.Parse(claimsPrincipal.FindFirst("id").Value);

                var result = await _cartService.AddToCart(userId, request);
                if(result.IsSuccessed==false)
                    return BadRequest(result.Message);
                return Ok(result);
            }
            catch(Exception ex)
            {
                Console.WriteLine(DateTime.Now + "- Server Error: " + ex);
                return StatusCode(500, "Lỗi server");
            } 
        }

        [HttpDelete("{cartId}")]
        public async Task<IActionResult> DeleteCart(int cartId)
        {
            try
            {
                var claimsPrincipal = this.User;
                var userId = Int32.Parse(claimsPrincipal.FindFirst("id").Value);

                var result = await _cartService.RemoveFromCart(userId, cartId);
                if(result.IsSuccessed==false)
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
        public async Task<IActionResult> DeleteMultiCarts(List<int> cartIds)
        {
            try
            {
                var claimsPrincipal = this.User;
                var userId = Int32.Parse(claimsPrincipal.FindFirst("id").Value);
                
                var result = await _cartService.RemoveMultiCarts(userId, cartIds);       
                if(result.IsSuccessed==false)
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
        public async Task<IActionResult> UpdateCartItem(UpdateCartItemRequest request)
        {
            try
            {
                var claimsPrincipal = this.User;
                var userId = Int32.Parse(claimsPrincipal.FindFirst("id").Value);

                var result = await _cartService.UpdateCartItem(userId, request);   
                if(result.IsSuccessed==false)
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