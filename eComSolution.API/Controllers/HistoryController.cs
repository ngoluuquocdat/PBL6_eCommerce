using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using eComSolution.Service.Catalog.Histories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eComSolution.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class HistoryController : ControllerBase
    {
         private readonly IHistoryService _historyService;

        public HistoryController(IHistoryService historyService)
        {
            _historyService = historyService;
        }

        [HttpGet("me")]
        public async Task<IActionResult> Get()
        {
            try
            {
                var claimsPrincipal = this.User;
                var userId = Int32.Parse(claimsPrincipal.FindFirst("id").Value);

                var result = await _historyService.GetHistory(userId);

                if (result.IsSuccessed == false)
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

        [HttpPost("me")]
        public async Task<IActionResult> Add([FromBody]int productId)
        {
            try
            {
                var claimsPrincipal = this.User;
                var userId = Int32.Parse(claimsPrincipal.FindFirst("id").Value);

                var result = await _historyService.AddHistory(userId, productId);

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