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

        [HttpPost("order")]
        public async Task<IActionResult> Create(CheckOutRequest request)
        {
            var claimsPrincipal = this.User;
            var userId = Int32.Parse(claimsPrincipal.FindFirst("id").Value);
            
            var result = await _orderService.CreateOrder(userId, request);

            return Ok(result);
        }

        [HttpPost("orders")]
        public async Task<IActionResult> CreateManyOrders(List<CheckOutRequest> requests)
        {
            var claimsPrincipal = this.User;
            var userId = Int32.Parse(claimsPrincipal.FindFirst("id").Value);
            
            var result = await _orderService.CreateManyOrders(userId, requests);

            return Ok(result);
        }

        
    }
}