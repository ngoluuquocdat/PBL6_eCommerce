using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrdersAPI.Services;
using OrdersAPI.ViewModels;

namespace eComSolution.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CheckOutRequest request)
        {
            try
            {
                var claimsPrincipal = this.User;
                var userId = Int32.Parse(claimsPrincipal.FindFirst("id").Value);
            
                var result = await _orderService.CreateOrders(userId, request);

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
        
        [HttpGet("me")]
        public async Task<IActionResult> GetUserOrders(string state)
        {
            try
            {
                var claimsPrincipal = this.User;
                var userId = Int32.Parse(claimsPrincipal.FindFirst("id").Value);
                
                var result = await _orderService.GetUserOrders(userId, state);

                if(result.IsSuccessed==false)
                    return BadRequest(result.Message);

                if (result.ResultObj == null || result.ResultObj.Count==0)
                    return NoContent();     

                return Ok(result);
            }
            catch(Exception ex)
            {
                Console.WriteLine(DateTime.Now + "- Server Error: " + ex);
                return StatusCode(500, "Lỗi server");
            }
        }

        [HttpDelete("me")]
        public async Task<IActionResult> CancelUnconfirmedOrder(CancelOrderRequest request)
        {   
            try
            {
                var claimsPrincipal = this.User;
                var userId = Int32.Parse(claimsPrincipal.FindFirst("id").Value);

                var result = await _orderService.CancelUnconfirmedOrder(userId, request);
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
        [HttpGet("shop")]
        public async Task<IActionResult> GetShopOrders(string state)
        {
            try
            {
                var claimsPrincipal = this.User;
                var userId = Int32.Parse(claimsPrincipal.FindFirst("id").Value);
                
                var result = await _orderService.GetShopOrders(userId, state);

                if(result.IsSuccessed==false)
                    return BadRequest(result.Message);

                if (result.ResultObj == null || result.ResultObj.Count==0)
                    return NoContent();     

                return Ok(result);
            }
            catch(Exception ex)
            {
                Console.WriteLine(DateTime.Now + "- Server Error: " + ex);
                return StatusCode(500, "Lỗi server");
            }
        }  
        [HttpPatch("shop")]
        public async Task<IActionResult> ConfirmOrder(int orderId)
        {   
            try
            {
                var claimsPrincipal = this.User;
                var userId = Int32.Parse(claimsPrincipal.FindFirst("id").Value);

                var result = await _orderService.ConfirmOrder(userId, orderId);
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
        [HttpDelete("shop")]
        public async Task<IActionResult> CancelOrder(CancelOrderRequest request)
        {   
            try
            {
                var claimsPrincipal = this.User;
                var userId = Int32.Parse(claimsPrincipal.FindFirst("id").Value);

                var result = await _orderService.CancelOrder(userId, request);
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