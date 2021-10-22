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

        Task<ApiResult<UserViewModel>> GetUserById(int userId);

        Task<ApiResult<UserPermission>> GetPermissions(int userId);
        
        Task<ApiResult<string>> ChangePassword(int userId, ChangePasswordVm request);

        Task<ApiResult<string>> ForgetPassword(string email);

        Task<ApiResult<string>> ResetPassword(string email, string password);

        Task<ApiResult<string>> ComfirmResetPassword(string email, string key);

        Task<ApiResult<string>> UpdateUser(int userId, UpdateUserVm updateUser);
        
    }
}