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

        [HttpDelete]
        public async Task<IActionResult> Delete(int cartId)
        {
            // var claimsPrincipal = this.User;
            // var userId = Int32.Parse(claimsPrincipal.FindFirst("id").Value);

            var result = await _cartService.RemoveFromCart(cartId);
            return Ok(result);
        }

        [HttpPost("checkout")]
        public async Task<IActionResult> CheckOut(List<CheckOutRequest> requests)
        {
            // foreach(var request in requests)
            // {
            //     var result = await _cartService.CheckOut(request.ShopId, request.ProductDetail_Ids);
            // }
            return null;
        }

        // [HttpPost("test")]
        // public IActionResult CreateCheckOutRequests(List<CartItem> items)
        // {
        //     List<int> list_shopIds = new List<int>();

        //     // get list of shopIds
        //     foreach(var item in items)
        //     {
        //         if(!list_shopIds.Contains(item.ShopId))
        //         {
        //             list_shopIds.Add(item.ShopId);
        //         }
        //     }
        //     // get list of check out requests
        //     var list_checkouts = new List<CheckOutRequest>();
        //     foreach(var shopId in list_shopIds)
        //     {
        //         list_checkouts.Add(new CheckOutRequest
        //         {
        //             ShopId = shopId,
        //             ProductDetail_Ids = items
        //                 .Where(x=>x.ShopId==shopId)
        //                 .Select(x=>x.ProductDetail_Id).ToList()
        //         });
        //     }
        //     return Ok(list_checkouts);
        // }
    }
}