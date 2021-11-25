using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthenAPI.Services.Authen;
using AuthenAPI.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AuthenAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenController : ControllerBase
    {
        private readonly IAuthenService _authenService;

        public AuthenController(IAuthenService authenService)
        {
            _authenService = authenService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            try
            {
                var result = await _authenService.Login(request);
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
                var result = await _authenService.Register(request);
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
                var result = await _authenService.CheckUsername(username);
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
                var result = await _authenService.CheckEmail(email);
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
                var result = await _authenService.CheckPhone(phone);
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