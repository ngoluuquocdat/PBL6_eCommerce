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
        public async Task<IActionResult> create([FromForm]CreateShopVm request)
        {
            var claimsPrincipal = this.User;
            var userId = Int32.Parse(claimsPrincipal.FindFirst("id").Value);

            var result = await _shopService.Create(userId, request);
            if (result.IsSuccessed == false)
                return BadRequest(result.Message);    

            return Ok(result);
        }
        [HttpPost("update")]
        public async Task<IActionResult> update([FromForm]CreateShopVm request)
        {
            var claimsPrincipal = this.User;
            var userId = Int32.Parse(claimsPrincipal.FindFirst("id").Value);

            var result = await _shopService.Update(userId, request);
            if (result.IsSuccessed == false)
                return BadRequest(result.Message);    

            return Ok(result);
        }
        [HttpGet]
        public async Task<ActionResult> Get(){
            var claimsPrincipal = this.User;
            var userId = Int32.Parse(claimsPrincipal.FindFirst("id").Value);

            var result = await _shopService.Get(userId);
            if (result.IsSuccessed == false)
                return BadRequest(result.Message);    

            return Ok(result);
        }
        [HttpGet("getAll")]
        public async Task<ActionResult> GetAll(){

            var result = await _shopService.GetAll();
            if (result.IsSuccessed == false)
                return BadRequest(result.Message);    

            return Ok(result);
        }
        [HttpPatch("Disable")]
        [Authorize]
        public async Task<IActionResult> DisableShop(int shopId)
        {
            var result = await _shopService.DisableShop(shopId);
            if(result.IsSuccessed == false) return BadRequest(result.Message);

            return Ok(result);
        }

        [HttpPatch("Enable")]
        [Authorize]
        public async Task<IActionResult> EnableShop(int shopId)
        {
            var result = await _shopService.EnableShop(shopId);
            if(result.IsSuccessed == false) return BadRequest(result.Message);

            return Ok(result);
        }
    }
}