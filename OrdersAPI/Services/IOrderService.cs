using System.Collections.Generic;
using System.Threading.Tasks;
using Database.Entities;
using OrdersAPI.ViewModels;
using OrdersAPI.ViewModels.Common;

namespace OrdersAPI.Services
{
    public interface IOrderService
    {
        Task<ApiResult<int>> CreateOrders(int userId, CheckOutRequest request);
        //Task<ApiResult<int>> CreateOrderManyShop(int userId, CheckOutRequest request);

        // get order cho user xem lịch sử order
        Task<ApiResult<List<OrderVm>>> GetUserOrders(int userId, string state="");

        // get order cho shop
        //Task<ApiResult<List<OrderVm>>> GetShopOrders(int shopId, string state="");

        // get order cho shop theo ngày để thống kê doanh thu
        //Task<ApiResult<List<OrderVm>>> GetOrdersByDate(int shopId, DateTime fromdate, DateTime todate);
        Task<ApiResult<int>> CancelUnconfirmedOrder(int userId, CancelOrderRequest request);
        Task<List<Function>> GetPermissions(int userId);
    }
}