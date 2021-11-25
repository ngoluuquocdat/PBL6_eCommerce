using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Database.Entities;
using ShopAPI.ViewModels;
using ShopAPI.ViewModels.Common;

namespace ShopAPI.Services
{
    public interface IShopService
    {
        Task<ApiResult<string>> Create(int userId, CreateShopVm request);
        Task<ApiResult<ShopVm>> Get(int userId);
        Task<ApiResult<ShopVm>> GetByShopId(int shopId);
        Task<ApiResult<List<ShopVm>>> GetAll(string name);
        Task<ApiResult<string>> Update(int userId, CreateShopVm request);
        Task<ApiResult<string>> DisableShop(int shopId, string disable_reason);
        Task<ApiResult<string>> EnableShop(int shopId);
        Task<List<Function>> GetPermissions(int userId);
    }
}