using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eComSolution.Service.System.Users;
using eComSolution.ViewModel.System.Users;
using eShopSolution.ViewModels.Common;
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

        [HttpGet("{UserId}")]
        public async Task<IActionResult> GetUserBuId(int UserId)
        {
            var user = await _userService.GetUserById(UserId);
            if(user.IsSuccessed == false) return Unauthorized("User is not exist!");

            return Ok(user);
        }
    }
}