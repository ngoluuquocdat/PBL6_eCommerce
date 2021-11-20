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
            var result = await _userService.Login(request);
            if(result.IsSuccessed == false) return Unauthorized(result);

            return Ok(result);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var result = await _userService.Register(request);
            if(result.IsSuccessed == false) return BadRequest(result);

            return Ok(result);
        }
        [HttpGet("CheckUsername")]
        public async Task<IActionResult> CheckUsername(string username)
        {
            var result = await _userService.CheckUsername(username);
            if(result.IsSuccessed == false) return BadRequest(result);

            return Ok(result);
        }

        [HttpGet("CheckPhone")]
        public async Task<IActionResult> CheckPhone(string phone)
        {
            var result = await _userService.CheckPhone(phone);
            if(result.IsSuccessed == false) return BadRequest(result);

            return Ok(result);
        }
        [HttpGet("CheckEmail")]
        public async Task<IActionResult> CheckEmail(string email)
        {
            var result = await _userService.CheckEmail(email);
            if(result.IsSuccessed == false) return BadRequest(result);

            return Ok(result);
        }
    }
}