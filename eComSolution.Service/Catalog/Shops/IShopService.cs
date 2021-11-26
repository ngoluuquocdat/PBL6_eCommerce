using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eComSolution.ViewModel.Catalog.Shops;
using eShopSolution.ViewModels.Common;

namespace eComSolution.Service.Catalog.Shops
{
    public interface IShopService
    {
        Task<ApiResult<string>> Create(int userId, CreateShopVm request);
        Task<ApiResult<ShopVm>> Get(int userId);
        Task<ApiResult<ShopVm>> GetByShopId(int shopId);
        Task<ApiResult<List<ShopVm>>> GetAll(string name);
        Task<ApiResult<string>> Update(int userId, CreateShopVm request);
        Task<ApiResult<string>> DisableShop(ShopDisableRequest request);
        Task<ApiResult<string>> EnableShop(int shopId);
    }
}