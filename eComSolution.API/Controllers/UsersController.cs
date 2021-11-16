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
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("me")]
        [Authorize]
        public async Task<IActionResult> Get()                // get info of user (USER)
        {
            var claimsPrincipal = this.User;
            var userId = Int32.Parse(claimsPrincipal.FindFirst("id").Value);
            var result = await _userService.GetUserById(userId);
            if(result.IsSuccessed == false) return BadRequest(result);

            return Ok(result);
        }
        [HttpGet("{userId}")]                 
        [Authorize]
        public async Task<IActionResult> GetById(int userId)  // get info of user (ADMIN) 
        {
            var result = await _userService.GetUserById(userId);
            if(result.IsSuccessed == false) return Unauthorized(result);

            return Ok(result);
        }
        [HttpGet] 
        [Authorize]
        public async Task<IActionResult> GetAll(string name)            // get all user (ADMIN)
        {
            var result = await _userService.GetAllUsers(name);
            if(result.IsSuccessed == false) return BadRequest(result);

            return Ok(result);
        }
        [HttpGet("Disable")]
        [Authorize]
        public async Task<IActionResult> GetUserDisable()
        {
            var result = await _userService.GetUserDisable();
            if(result.IsSuccessed == false) return BadRequest(result);

            return Ok(result);
        }

        [HttpPatch("Disable")]
        [Authorize]
        public async Task<IActionResult> DisableUser(int userId)
        {
            var result = await _userService.DisableUser(userId);
            if(result.IsSuccessed == false) return BadRequest(result);

            return Ok(result);
        }

        [HttpPatch("Enable")]
        [Authorize]
        public async Task<IActionResult> EnableUser(int userId)
        {
            var result = await _userService.EnableUser(userId);
            if(result.IsSuccessed == false) return BadRequest(result);

            return Ok(result);
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> UpdateUser(UpdateUserVm updateUser)
        {
            var claimsPrincipal = this.User;
            var userId = Int32.Parse(claimsPrincipal.FindFirst("id").Value);
            var result = await _userService.UpdateUser(userId, updateUser);
            if(result.IsSuccessed == false) return BadRequest(result);

            return Ok(result);
        }
        [HttpPatch("Password")]
        [Authorize]
        public async Task<IActionResult> ChangePassword(ChangePasswordVm request)
        {
            var claimsPrincipal = this.User;
            var userId = Int32.Parse(claimsPrincipal.FindFirst("id").Value);
            var result = await _userService.ChangePassword(userId, request);
            if(result.IsSuccessed == false) return Unauthorized(result);

            return Ok(result);
        }
        [HttpPost("{email}/ForgetPassword")]
        public async Task<IActionResult> ForgetPassword(string email)
        {
            var result = await _userService.ForgetPassword(email);
            if(result.IsSuccessed == false) return Unauthorized(result);

            return Ok(result);
        }
        [HttpPatch("ResetPassword")]
        public async Task<IActionResult> ResetPassword(string email, string password)
        {
            var result = await _userService.ResetPassword(email, password);
            if(result.IsSuccessed == false) return Unauthorized(result);

            return Ok(result);
        }
        [HttpGet("ConfirmResetPass")]
        public async Task<IActionResult> ConfirmResetPass(string email, string key)
        {
            var result = await _userService.ComfirmResetPassword(email, key);   
            if(result.IsSuccessed == false) return Unauthorized(result);

            return Ok(result);
        }
        [HttpGet("Checkusername")]
        public async Task<IActionResult> CheckUsername(string username)
        {
            var result = await _userService.CheckUsername(username);
            if(result.IsSuccessed == false) return Unauthorized(result);

            return Ok(result);
        }
        [HttpGet("CheckEmail")]
        public async Task<IActionResult> CheckEmail(string email)
        {
            var result = await _userService.CheckEmail(email);
            if(result.IsSuccessed == false) return Unauthorized(result);

            return Ok(result);
        }
        [HttpGet("CheckPhone")]
        public async Task<IActionResult> CheckPhone(string phone)
        {
            var result = await _userService.CheckPhone(phone);
            if(result.IsSuccessed == false) return Unauthorized(result);

            return Ok(result);
        }
    }
}