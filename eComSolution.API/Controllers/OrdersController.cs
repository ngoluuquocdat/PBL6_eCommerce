using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eComSolution.Service.Catalog.Orders;
using eComSolution.ViewModel.Catalog.Carts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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

        [HttpPost()]
        public async Task<IActionResult> Create(CheckOutRequest request)
        {
            var claimsPrincipal = this.User;
            var userId = Int32.Parse(claimsPrincipal.FindFirst("id").Value);
            
            var result = await _orderService.CreateOrders(userId, request);

            return Ok(result);
        }
        
        [HttpGet]
        public async Task<IActionResult> GetUserOrders(string state)
        {
            var claimsPrincipal = this.User;
            var userId = Int32.Parse(claimsPrincipal.FindFirst("id").Value);
            
            var result = await _orderService.GetUserOrders(userId, state);

            return Ok(result);
        }

        [HttpPatch]
        public async Task<IActionResult> CancelOrder(int orderId)
        {   
            var result = await _orderService.CancelOrder(orderId);
            return Ok(result);
        }    
    }
}