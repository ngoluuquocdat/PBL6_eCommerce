using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Database.Entities;
using UserAPI.ViewModels;
using UserAPI.ViewModels.Common;

namespace UserAPI.Services.Users
{
    public interface IUserService
    {
        Task<ApiResult<UserViewModel>> GetUserById(int userId);

        Task<List<Function>> GetPermissions(int userId);
        
        Task<ApiResult<string>> ChangePassword(int userId, ChangePasswordVm request);

        Task<ApiResult<string>> ForgetPassword(string email);

        Task<ApiResult<string>> ResetPassword(string email, string password);

        Task<ApiResult<string>> ComfirmResetPassword(string email, string key);

        Task<ApiResult<string>> UpdateUser(int userId, UpdateUserVm updateUser);

        Task<ApiResult<List<UserViewModel>>> GetAllUsers(string name);

        Task<ApiResult<List<UserViewModel>>> GetUserDisable();

        Task<ApiResult<string>> DisableUser(int userId);

        Task<ApiResult<string>> EnableUser(int userId);
    }
}