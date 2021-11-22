using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eComSolution.ViewModel.Catalog.Carts;
using eComSolution.ViewModel.Catalog.Orders;
using eShopSolution.ViewModels.Common;

namespace eComSolution.Service.Catalog.Orders
{
    public interface IOrderService
    {
        //Task<int> CheckCartItems(List<int> cartIds);
        Task<ApiResult<int>> CreateOrders(int userId, CheckOutRequest request);
        //Task<ApiResult<int>> CreateOrderManyShop(int userId, CheckOutRequest request);

        // get order cho user xem lịch sử order
        Task<ApiResult<List<OrderVm>>> GetUserOrders(int userId, string state="");

        // get order cho shop
        //Task<ApiResult<List<OrderVm>>> GetShopOrders(int shopId, string state="");

        // get order cho shop theo ngày để thống kê doanh thu
        //Task<ApiResult<List<OrderVm>>> GetOrdersByDate(int shopId, DateTime fromdate, DateTime todate);
        Task<ApiResult<int>> CancelUnconfirmedOrder(int orderId, string cancelReason);
    }
}