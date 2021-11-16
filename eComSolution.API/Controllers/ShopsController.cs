using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eComSolution.Service.Catalog.Shops;
using eComSolution.ViewModel.Catalog.Shops;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eComSolution.API.Controllers
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
            var claimsPrincipal = this.User;
            var userId = Int32.Parse(claimsPrincipal.FindFirst("id").Value);

            var result = await _shopService.Create(userId, request);
            if (result.IsSuccessed == false)
                return BadRequest(result.Message);    

            return Ok(result);
        }
        [HttpPut]
        public async Task<IActionResult> Update([FromForm]CreateShopVm request)   // cập nhật thông tin shop 
        {
            var claimsPrincipal = this.User;
            var userId = Int32.Parse(claimsPrincipal.FindFirst("id").Value);

            var result = await _shopService.Update(userId, request);
            if (result.IsSuccessed == false)
                return BadRequest(result.Message);    

            return Ok(result);
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
        [HttpGet("Id")]
        public  Task<ActionResult> GetShopById(int userId, int shopId) =>  // xem thông tin shop by userId (ADMIN)
            userId == 0 ? GetByShopId(shopId) : GetByUserId(userId);
        private async Task<ActionResult> GetByUserId(int userId){
            var result = await _shopService.Get(userId);

            if (result.IsSuccessed == false)
                return BadRequest(result.Message);    

            return Ok(result);
        }
        private async Task<ActionResult> GetByShopId(int shopId){
            var result = await _shopService.GetByShopId(shopId);

            if (result.IsSuccessed == false)
                return BadRequest(result.Message);    

            return Ok(result);
        }
        [HttpGet]                                             // xem thông tin tất cả các shop
        public async Task<ActionResult> GetAll(string name){

            var result = await _shopService.GetAll(name);
            if (result.IsSuccessed == false)
                return BadRequest(result.Message);    

            return Ok(result);
        }
        [HttpPatch("Disable")]
        [Authorize]
        public async Task<IActionResult> DisableShop(int shopId, string disable_reason)   // vô hiệu hóa shop (ADMIN)
        {
            var result = await _shopService.DisableShop(shopId, disable_reason);
            if(result.IsSuccessed == false) return BadRequest(result.Message);

            return Ok(result);
        }

        [HttpPatch("Enable")]
        [Authorize]
        public async Task<IActionResult> EnableShop(int shopId)               // tái kích hoạt shop (ADMIN) 
        {
            var result = await _shopService.EnableShop(shopId);
            if(result.IsSuccessed == false) return BadRequest(result.Message);

            return Ok(result);
        }
    }
}