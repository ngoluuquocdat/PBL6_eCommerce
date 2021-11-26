using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eComSolution.Service.System.Users;
using eComSolution.ViewModel.System.Users;
using Microsoft.AspNetCore.Mvc;

namespace eComSolution.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IUserService _userService;

        public AccountsController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            try
            {
                var result = await _userService.Login(request);
                if(result.IsSuccessed == false) 
                    return BadRequest(result.Message);

                return Ok(result);
            }
            catch(Exception ex)
            {
                Console.WriteLine(DateTime.Now + "- Server Error: " + ex);
                return StatusCode(500, "Lỗi server");
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            try
            {
                var result = await _userService.Register(request);
                if(result.IsSuccessed == false) return BadRequest(result);

                return Ok(result);
            }
            catch(Exception ex)
            {
                Console.WriteLine(DateTime.Now + "- Server Error: " + ex);
                return StatusCode(500, "Lỗi server");
            }
        }

        [HttpGet("CheckUsername")]
        public async Task<IActionResult> CheckUsername(string username)
        {
            try
            {
                var result = await _userService.CheckUsername(username);
                if(result.IsSuccessed == false) 
                    return BadRequest(result.Message);

                return Ok(result);
            }
            catch(Exception ex)
            {
                Console.WriteLine(DateTime.Now + "- Server Error: " + ex);
                return StatusCode(500, "Lỗi server");
            }
        }

        [HttpGet("CheckEmail")]
        public async Task<IActionResult> CheckEmail(string email)
        {
            try
            {
                var result = await _userService.CheckEmail(email);
                if(result.IsSuccessed == false) return BadRequest(result.Message);

                return Ok(result);
            }
            catch(Exception ex)
            {
                Console.WriteLine(DateTime.Now + "- Server Error: " + ex);
                return StatusCode(500, "Lỗi server");
            }
        }
        [HttpGet("CheckPhone")]
        public async Task<IActionResult> CheckPhone(string phone)
        {
            try
            {
                var result = await _userService.CheckPhone(phone);
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