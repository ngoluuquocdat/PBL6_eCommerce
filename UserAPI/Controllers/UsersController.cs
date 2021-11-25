using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserAPI.Services.Users;
using UserAPI.ViewModels;

namespace UserAPI.Controllers
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
            try
            {
                var claimsPrincipal = this.User;
                var userId = Int32.Parse(claimsPrincipal.FindFirst("id").Value);
                var result = await _userService.GetUserById(userId);
                if(result.IsSuccessed == false) return BadRequest(result.Message);

                return Ok(result);
            }
            catch(Exception ex)
            {
                Console.WriteLine(DateTime.Now + "- Server Error: " + ex);
                return StatusCode(500, "Lỗi server");
            }
        }

        [HttpGet("{userId}")]                 
        [Authorize]
        public async Task<IActionResult> GetById(int userId)  // get info of user (ADMIN) 
        {
            try
            {
                var result = await _userService.GetUserById(userId);
                if(result.IsSuccessed == false) 
                    return NoContent();

                return Ok(result);
            }
            catch(Exception ex)
            {
                Console.WriteLine(DateTime.Now + "- Server Error: " + ex);
                return StatusCode(500, "Lỗi server");
            }
        }
        [HttpGet] 
        [Authorize]
        public async Task<IActionResult> GetAll(string name)            // get all user (ADMIN)
        {
            try
            {
                var result = await _userService.GetAllUsers(name);
                if(result.IsSuccessed == false) return BadRequest(result.Message);
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
        [HttpGet("Disable")]
        [Authorize]
        public async Task<IActionResult> GetUserDisable()
        {
            try
            {
                var result = await _userService.GetUserDisable();
                if(result.IsSuccessed == false) return BadRequest(result.Message);

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
        public async Task<IActionResult> DisableUser(int userId)
        {
            try
            {
                var result = await _userService.DisableUser(userId);
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
        public async Task<IActionResult> EnableUser(int userId)
        {
            try
            {
                var result = await _userService.EnableUser(userId);
                if(result.IsSuccessed == false) return BadRequest(result.Message);

                return Ok(result);
            }
            catch(Exception ex)
            {
                Console.WriteLine(DateTime.Now + "- Server Error: " + ex);
                return StatusCode(500, "Lỗi server");
            }
        }

        [HttpPut("me")]
        [Authorize]
        public async Task<IActionResult> UpdateUser(UpdateUserVm updateUser)
        {
            try
            {
                var claimsPrincipal = this.User;
                var userId = Int32.Parse(claimsPrincipal.FindFirst("id").Value);
                var result = await _userService.UpdateUser(userId, updateUser);
                if(result.IsSuccessed == false) return BadRequest(result.Message);

                return Ok(result);
            }
            catch(Exception ex)
            {
                Console.WriteLine(DateTime.Now + "- Server Error: " + ex);
                return StatusCode(500, "Lỗi server");
            }
        }
        [HttpPut()]
        [Authorize]
        public async Task<IActionResult> UpdateUser(int userId, UpdateUserVm updateUser)
        {
            try
            {
                var result = await _userService.UpdateUser(userId, updateUser);
                if(result.IsSuccessed == false) return BadRequest(result.Message);

                return Ok(result);
            }
            catch(Exception ex)
            {
                Console.WriteLine(DateTime.Now + "- Server Error: " + ex);
                return StatusCode(500, "Lỗi server");
            }
        }
        [HttpPatch("Password")]
        [Authorize]
        public async Task<IActionResult> ChangePassword(ChangePasswordVm request)
        {
            try
            {
                var claimsPrincipal = this.User;
                var userId = Int32.Parse(claimsPrincipal.FindFirst("id").Value);
                var result = await _userService.ChangePassword(userId, request);
                if(result.IsSuccessed == false) return BadRequest(result.Message);

                return Ok(result);
            }
            catch(Exception ex)
            {
                Console.WriteLine(DateTime.Now + "- Server Error: " + ex);
                return StatusCode(500, "Lỗi server");
            }   
        }
        [HttpPost("{email}/ForgetPassword")]
        public async Task<IActionResult> ForgetPassword(string email)
        {
            try
            {
                var result = await _userService.ForgetPassword(email);
                if(result.IsSuccessed == false) return BadRequest(result.Message);

                return Ok(result);
            }
            catch(Exception ex)
            {
                Console.WriteLine(DateTime.Now + "- Server Error: " + ex);
                return StatusCode(500, "Lỗi server");
            } 
        }
        [HttpPatch("ResetPassword")]
        public async Task<IActionResult> ResetPassword(string email, string password)
        {
            try
            {
                var result = await _userService.ResetPassword(email, password);
                if(result.IsSuccessed == false) return BadRequest(result.Message);

                return Ok(result);
            }
            catch(Exception ex)
            {
                Console.WriteLine(DateTime.Now + "- Server Error: " + ex);
                return StatusCode(500, "Lỗi server");
            }
        }
        [HttpGet("ConfirmResetPass")]
        public async Task<IActionResult> ConfirmResetPass(string email, string key)
        {
            try
            {
                var result = await _userService.ComfirmResetPassword(email, key);   
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