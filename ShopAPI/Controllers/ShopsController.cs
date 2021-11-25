using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopAPI.Services;
using ShopAPI.ViewModels;

namespace ShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ShopsController : ControllerBase
    {
        private readonly IShopService _shopService;

        public ShopsController(IShopService shopService)
        {
            _shopService = shopService;
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromForm]CreateShopVm request)   // tạo shop mới
        {
            try
            {
                var claimsPrincipal = this.User;
                var userId = Int32.Parse(claimsPrincipal.FindFirst("id").Value);

                var result = await _shopService.Create(userId, request);
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
        public async Task<IActionResult> Update([FromForm]CreateShopVm request)   // cập nhật thông tin shop 
        {
            try
            {
                var claimsPrincipal = this.User;
                var userId = Int32.Parse(claimsPrincipal.FindFirst("id").Value);

                var result = await _shopService.Update(userId, request);
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
        [HttpGet("me")]
        public async Task<ActionResult> Get()  // xem thông tin shop (USER)
        {
            var claimsPrincipal = this.User;
            var userId = Int32.Parse(claimsPrincipal.FindFirst("id").Value);

            var result = await _shopService.Get(userId);
            if (result.IsSuccessed == false)
                return BadRequest(result.Message);    

            return Ok(result);
        }
        [HttpGet("{shopId}")]
        [AllowAnonymous]
        public async Task<ActionResult> GetShopById(int shopId)  // xem thông tin shop by userId (ADMIN)
        {
            try
            {
                var result = await _shopService.GetByShopId(shopId);

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
        [HttpGet]                                             // xem thông tin tất cả các shop
        public async Task<ActionResult> GetAll(string name)
        {
            try
            {
                var result = await _shopService.GetAll(name);
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
        [HttpPatch("Disable")]
        [Authorize]
        public async Task<IActionResult> DisableShop(ShopDisableRequest request)   // vô hiệu hóa shop (ADMIN)
        {
            try
            {
                var result = await _shopService.DisableShop(request);
                if(result.IsSuccessed == false) return BadRequest(result.Message);

                return Ok(result);
            }
            catch(Exception ex)
            {
                Console.WriteLine(DateTime.Now + "- Server Error: " + ex);
                return StatusCode(500, "Lỗi server");
            }
        }

        [HttpPatch("Enable")]
        [Authorize]
        public async Task<IActionResult> EnableShop(int shopId)               // tái kích hoạt shop (ADMIN) 
        {
            try
            {
                var result = await _shopService.EnableShop(shopId);
                if(result.IsSuccessed == false) return BadRequest(result.Message);

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