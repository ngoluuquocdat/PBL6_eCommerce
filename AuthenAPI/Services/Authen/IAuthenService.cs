using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthenAPI.ViewModels;
using AuthenAPI.ViewModels.Common;
using Database.Entities;

namespace AuthenAPI.Services.Authen
{
    public interface IAuthenService
    {
        Task<ApiResult<string>> Register(RegisterRequest request);

        Task<ApiResult<string>> Login(LoginRequest request);

        Task<ApiResult<string>> CheckUsername(string username);

        Task<ApiResult<string>> CheckEmail(string email);
        
        Task<ApiResult<string>> CheckPhone(string phonenumber);

        Task<List<Function>> GetPermissions(int userId);
        
    }
}