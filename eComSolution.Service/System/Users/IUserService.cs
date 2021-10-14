using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eComSolution.ViewModel.System.Users;
using eShopSolution.ViewModels.Common;

namespace eComSolution.Service.System.Users
{
    public interface IUserService
    {
        Task<ApiResult<string>> Register(RegisterRequest request);

        Task<ApiResult<string>> Login(LoginRequest request);

        Task<ApiResult<UserViewModel>> GetUserById(int UserId);
        
    }
}