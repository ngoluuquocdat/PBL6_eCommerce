using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eComSolution.ViewModel.Catalog.Carts;
using eShopSolution.ViewModels.Common;

namespace eComSolution.Service.Catalog.Orders
{
    public interface IOrderService
    {
        Task<ApiResult<int>> CreateOrder(int userId, CheckOutRequest request);
        Task<ApiResult<int>> CreateManyOrders(int userId, List<CheckOutRequest> requests);
        
    }
}