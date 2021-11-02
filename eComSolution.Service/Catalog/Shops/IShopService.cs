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
        Task<ApiResult<List<ShopVm>>> GetAll();
        Task<ApiResult<string>> Update(int userId, CreateShopVm request);
        Task<ApiResult<string>> DisableShop(int shopId);
        Task<ApiResult<string>> EnableShop(int shopId);
    }
}