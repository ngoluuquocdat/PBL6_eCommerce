using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eComSolution.Data.Entities;
using eComSolution.ViewModel.System.Users;
using eShopSolution.ViewModels.Common;

namespace eComSolution.Service.System.Users
{
    public interface IUserService
    {
        Task<ApiResult<string>> Register(RegisterRequest request);

        Task<ApiResult<string>> Login(LoginRequest request);

        Task<ApiResult<UserViewModel>> GetUserById(int userId);

        Task<List<Function>> GetPermissions(int userId);
        
        Task<ApiResult<string>> ChangePassword(int userId, ChangePasswordVm request);

        Task<ApiResult<string>> ForgetPassword(string email);

        Task<ApiResult<string>> ResetPassword(string email, string password);

        Task<ApiResult<string>> ComfirmResetPassword(string email, string key);

        Task<ApiResult<string>> UpdateUser(int userId, UpdateUserVm updateUser);

        Task<ApiResult<List<UserViewModel>>> GetAllUsers();

        Task<ApiResult<List<UserViewModel>>> GetUserDisable();

        Task<ApiResult<string>> DisableUser(int userId);

        Task<ApiResult<string>> EnableUser(int userId);
        
    }
}