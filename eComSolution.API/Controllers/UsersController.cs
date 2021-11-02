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

        [HttpGet]
        public async Task<IActionResult> GetUserById()
        {
            var claimsPrincipal = this.User;
            var userId = Int32.Parse(claimsPrincipal.FindFirst("id").Value);
            var result = await _userService.GetUserById(userId);
            if(result.IsSuccessed == false) return BadRequest(result.Message);

            return Ok(result);
        }
        [HttpGet("getAll")]
        [Authorize]
        public async Task<IActionResult> GetAllUsers()
        {
            var result = await _userService.GetAllUsers();
            if(result.IsSuccessed == false) return BadRequest(result.Message);

            return Ok(result);
        }
        [HttpGet("getUserDisable")]
        [Authorize]
        public async Task<IActionResult> GetUserDisable()
        {
            var result = await _userService.GetUserDisable();
            if(result.IsSuccessed == false) return BadRequest(result.Message);

            return Ok(result);
        }

        [HttpPatch("Disable")]
        [Authorize]
        public async Task<IActionResult> DisableUser(int userId)
        {
            var result = await _userService.DisableUser(userId);
            if(result.IsSuccessed == false) return BadRequest(result.Message);

            return Ok(result);
        }

        [HttpPatch("Enable")]
        [Authorize]
        public async Task<IActionResult> EnableUser(int userId)
        {
            var result = await _userService.EnableUser(userId);
            if(result.IsSuccessed == false) return BadRequest(result.Message);

            return Ok(result);
        }

        [HttpPost("Update")]
        [Authorize]
        public async Task<IActionResult> UpdateUser(UpdateUserVm updateUser)
        {
            var claimsPrincipal = this.User;
            var userId = Int32.Parse(claimsPrincipal.FindFirst("id").Value);
            var result = await _userService.UpdateUser(userId, updateUser);
            if(result.IsSuccessed == false) return BadRequest(result.Message);

            return Ok(result);
        }
        [HttpPut("changePassword")]
        [Authorize]
        public async Task<IActionResult> ChangePassword(ChangePasswordVm request)
        {
            var claimsPrincipal = this.User;
            var userId = Int32.Parse(claimsPrincipal.FindFirst("id").Value);
            var result = await _userService.ChangePassword(userId, request);
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