using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using eComSolution.Service.Catalog.Carts;
using eComSolution.Service.Catalog.Histories;
using eComSolution.ViewModel.Catalog.Carts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eComSolution.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet]
        public async Task<IActionResult> Show()
        {
            var claimsPrincipal = this.User;
            var userId = Int32.Parse(claimsPrincipal.FindFirst("id").Value);

            var result = await _cartService.GetCartItems(userId);

            if (result.IsSuccessed == false)
                return BadRequest(result.Message);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddToCartRequest request)
        {
            var claimsPrincipal = this.User;
            var userId = Int32.Parse(claimsPrincipal.FindFirst("id").Value);

            var result = await _cartService.AddToCart(userId, request);
           
            return Ok(result);
        }
    }
}