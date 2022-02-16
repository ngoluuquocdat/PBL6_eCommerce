using System;
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

        // get order cho user xem lịch sử order
        Task<ApiResult<List<OrderVm>>> GetUserOrders(int userId, string state="");

        // get order cho shop
        Task<ApiResult<List<OrderVm>>> GetShopOrders(int userId, string state="");

        // get order theo Id
        Task<ApiResult<OrderVm>> GetOrderById(int orderId);

        // get all orders cho admin
        Task<ApiResult<List<OrderVm>>> GetAllOrders(DateTime fromDate, DateTime toDate);
        
        // cho member hủy đơn hàng Chưa xác nhận
        Task<ApiResult<int>> CancelUnconfirmedOrder(int userId, CancelOrderRequest request);
        
        // cho shop xác nhận đơn hàng
        Task<ApiResult<int>> ConfirmOrder(int userId, int orderId);

        // cho shop hủy đơn hàng khách đặt cho shop
        Task<ApiResult<int>> CancelOrder(int userId, CancelOrderRequest request);

        Task<List<Function>> GetPermissions(int userId);
    }
}