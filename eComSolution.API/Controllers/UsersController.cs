using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eComSolution.Service.System.Users;
using eComSolution.ViewModel.System.Users;
using eShopSolution.ViewModels.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eComSolution.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("{UserId}")]
        public async Task<IActionResult> GetUserBuId(int UserId)
        {
            var result = await _userService.GetUserById(UserId);
            if(result.IsSuccessed == false) return BadRequest(result.Message);

            return Ok(result);
        }
        [HttpGet("permission/{UserId}")]
        public async Task<IActionResult> GetPermissions(int UserId)
        {
            var result = await _userService.GetPermissions(UserId);
            if(result.IsSuccessed == false) return Unauthorized(result.Message);

            return Ok(result);
        }
        [HttpPut("{UserId}/changePassword")]
        public async Task<IActionResult> ChangePassword(int UserId, ChangePasswordVm request)
        {
            var result = await _userService.ChangePassword(UserId, request);
            if(result.IsSuccessed == false) return Unauthorized(result.Message);

            return Ok(result);
        }
        [HttpGet("{email}/forgetPassword")]
        public async Task<IActionResult> ForgetPassword(string email)
        {
            var result = await _userService.ForgetPassword(email);
            if(result.IsSuccessed == false) return Unauthorized(result.Message);

            return Ok(result);
        }
        [HttpPost("resetPassword")]
        public async Task<IActionResult> ResetPassword(string email, string password)
        {
            var result = await _userService.ResetPassword(email, password);
            if(result.IsSuccessed == false) return Unauthorized(result.Message);

            return Ok(result);
        }
        [HttpGet("ConfirmResetPass")]
        public async Task<IActionResult> ConfirmResetPass(string email, string key)
        {
            var result = await _userService.ComfirmResetPassword(email, key);
            if(result.IsSuccessed == false) return Unauthorized(result.Message);

            return Ok(result);
        }
    }
}