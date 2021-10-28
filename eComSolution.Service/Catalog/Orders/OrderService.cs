using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eComSolution.Data.EF;
using eComSolution.Data.Entities;
using eComSolution.ViewModel.Catalog.Carts;
using eComSolution.ViewModel.Catalog.Orders;
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
        public async Task<ApiResult<List<OrderVm>>> GetUserOrders(int userId, string state = "")
        {
            var query = from o in _context.Orders 
                        join sh in _context.Shops on o.ShopId equals sh.Id
                        where o.UserId == userId
                        select new {o, sh};
                                    
            if(query==null)
                return new ApiResult<List<OrderVm>>(false, Message:"this user has no order");

            if(!string.IsNullOrEmpty(state))
                query = query.Where(x=>x.o.State==state);

            query = query.OrderByDescending(x=>x.o.OrderDate);
            
            var data = await query.Select(x=> new OrderVm
            {
                Id = x.o.Id,
                OrderDate = x.o.OrderDate,
                UserId = x.o.UserId,
                ShopId = x.o.ShopId,
                ShopName = x.sh.Name,
                ShipName = x.o.ShipName,
                ShipAddress = x.o.ShipAddress,
                ShipPhone = x.o.ShipPhone,
                State = x.o.State
            }).ToListAsync();

            for(int i=0; i<data.Count; i++)
            {
                data[i].OrderDetails = await GetOrderDetails(data[i].Id);
                data[i].TotalPrice = data[i].OrderDetails.Sum(d => d.Price);
            }
                        
            return new ApiResult<List<OrderVm>>(true, ResultObj:data);
        }
        public async Task<List<OrderDetailVm>> GetOrderDetails(int orderId)
        {
            List<OrderDetailVm> data = new List<OrderDetailVm>();
            var query = from od in _context.OrderDetails
                        join pd in _context.ProductDetails on od.ProductDetail_Id equals pd.Id
                        join p in _context.Products on pd.ProductId equals p.Id
                        where od.OrderId == orderId
                        select new {od, pd, p};

            var list_records = await query.ToListAsync();
            foreach(var record in list_records)
            {
                // ảnh theo color
                string path = "";                
                var image =  await _context.ProductImages
                    .Where(x=>x.ProductId==record.p.Id && x.ColorName==record.pd.Color)
                    .FirstOrDefaultAsync();
                if(image!=null) path = image.ImagePath;

                data.Add(new OrderDetailVm()
                {
                    OrderId = record.od.OrderId,
                    ProductDetail_Id = record.od.ProductDetail_Id,
                    ProductName = record.p.Name,
                    Color = record.pd.Color,
                    Size = record.pd.Size,
                    Quantity = record.od.Quantity,
                    Price = record.od.Price,
                    Image = path
                });
            }
            return data;
        }

        public async Task<ApiResult<int>> CancelOrder(int orderId)
        {
            var order = await _context.Orders.Where(x=>x.Id==orderId).FirstOrDefaultAsync();
            if(order==null) return new ApiResult<int>(false, Message:$"Cannot find order with this id: {orderId}");
            var order_details = await _context.OrderDetails
                            .Where(x=>x.OrderId==orderId).ToListAsync();
            try
            {
                // 1. chuyển trạng thái đơn hàng thành 'đã hủy'
                order.State = "Đã hủy";
                _context.Orders.Update(order);
                // 2. cộng lại số lượng sản phẩm đã đặt vào stock
                foreach(var order_detail in order_details)
                {
                    var product_detail = await _context.ProductDetails    
                                        .Where(x=>x.Id==order_detail.ProductDetail_Id)
                                        .FirstOrDefaultAsync();
                    product_detail.Stock += order_detail.Quantity;
                    _context.ProductDetails.Update(product_detail);
                }
                await _context.SaveChangesAsync();

            }
            catch(Exception e) 
            {
                return new ApiResult<int>(false, Message:"Cancel order failed:\n"+e.Message);
            }
            
            return new ApiResult<int>(true, Message:"Cancel order successful");
        }
    }
}

        

