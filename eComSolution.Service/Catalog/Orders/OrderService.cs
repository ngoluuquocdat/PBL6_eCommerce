using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eComSolution.Data.EF;
using eComSolution.Data.Entities;
using eComSolution.ViewModel.Catalog.Carts;
using eShopSolution.ViewModels.Common;
using Microsoft.EntityFrameworkCore;

namespace eComSolution.Service.Catalog.Orders
{
    public class OrderService : IOrderService
    {
        private readonly EComDbContext _context;

        public OrderService(EComDbContext context)
        {
            _context = context;
        }
        public async Task<ApiResult<int>> CreateOrder(int userId, CheckOutRequest request)
        {
            // 1. tạo order mới
            var new_order = new Order
            {
                OrderDate = DateTime.Now,
                UserId = userId,
                ShopId = request.ShopId,
                ShipName = request.ShipName,
                ShipAddress = request.ShipAddress,
                ShipPhone = request.ShipPhone,
                State = "Chờ xử lý"
            };
            // 2. tạo mới các order details
            var orderDetails = new List<OrderDetail>();
            foreach(var item in request.OrderDetails)
            {
                // add order detail mới vào list
                orderDetails.Add(new OrderDetail
                {
                    ProductDetail_Id = item.ProductDetail_Id,
                    Quantity = item.Quantity,
                    Price = item.Price
                });
                // trừ số lượng ở Db
                var productDetail = await _context.ProductDetails
                    .Where(x=>x.Id==item.ProductDetail_Id)
                    .FirstOrDefaultAsync();
                if(item.Quantity > productDetail.Stock)
                {
                    return new ApiResult<int>(false, Message:"Out of stock");
                }
                productDetail.Stock -= item.Quantity;
                _context.ProductDetails.Update(productDetail);
            }
            new_order.OrderDetails = orderDetails;
            _context.Orders.Add(new_order);
            await _context.SaveChangesAsync();

            return new ApiResult<int>(true, Message:"Create order successful");
        }

        public async Task<ApiResult<int>> CreateManyOrders(int userId, List<CheckOutRequest> requests)
        {
            foreach(var request in requests)
            {
                // 1. tạo order mới
                var new_order = new Order
                {
                    OrderDate = DateTime.Now,
                    UserId = userId,
                    ShopId = request.ShopId,
                    ShipName = request.ShipName,
                    ShipAddress = request.ShipAddress,
                    ShipPhone = request.ShipPhone,
                    State = "Chờ xử lý"
                };
                // 2. tạo mới các order details
                var orderDetails = new List<OrderDetail>();
                foreach(var item in request.OrderDetails)
                {
                    // add order detail mới vào list
                    orderDetails.Add(new OrderDetail
                    {
                        ProductDetail_Id = item.ProductDetail_Id,
                        Quantity = item.Quantity,
                        Price = item.Price
                    });
                    // trừ số lượng ở Db
                    var productDetail = await _context.ProductDetails
                        .Where(x=>x.Id==item.ProductDetail_Id)
                        .FirstOrDefaultAsync();
                    if(item.Quantity > productDetail.Stock)
                    {
                        return new ApiResult<int>(false, Message:"Out of stock");
                    }
                    productDetail.Stock -= item.Quantity;
                    _context.ProductDetails.Update(productDetail);
                }
                new_order.OrderDetails = orderDetails;
                _context.Orders.Add(new_order);
            }
            await _context.SaveChangesAsync();

            return new ApiResult<int>(true, Message:"Create order successful");
        }        
    }
}