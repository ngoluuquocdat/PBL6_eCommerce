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

        public async Task<ApiResult<int>> CreateOrders(int userId, CheckOutRequest request)
        {
            // ***** chỉ nhận vào input là list cartIds là đủ
            // 1. tạo list gom các cart items lại theo shopId
            List<ShopCartMap> shop_cart_maps = new List<ShopCartMap>();
            foreach(var cartId in request.CartIds)
            {
                int shopId = await (from c in _context.Carts 
                            join pd in _context.ProductDetails on c.ProductDetail_Id equals pd.Id
                            join p in _context.Products on pd.ProductId equals p.Id
                            where c.Id == cartId
                            select p.ShopId).FirstOrDefaultAsync();
                if(!shop_cart_maps.Any(x=>x.ShopId==shopId))   
                {
                    var new_shopCartMap = new ShopCartMap {ShopId=shopId, CartIds=new List<int>()};
                    new_shopCartMap.CartIds.Add(cartId);
                    shop_cart_maps.Add(new_shopCartMap);
                }         
                else
                {
                    var checkOutModel = shop_cart_maps.Where(x=>x.ShopId==shopId).FirstOrDefault();
                    checkOutModel.CartIds.Add(cartId);
                }
            }

            foreach(var shopCartMap in shop_cart_maps)
            {
                // 2. tạo order mới
                var new_order = new Order
                {
                    OrderDate = DateTime.Now,
                    DateModified = DateTime.Now,
                    UserId = userId,
                    ShopId = shopCartMap.ShopId,
                    ShipName = request.ShipName,
                    ShipAddress = request.ShipAddress,
                    ShipPhone = request.ShipPhone,
                    State = "Chờ xác nhận"
                    //CancelReason=""
                };
                // 2. tạo mới các order details
                var orderDetails = new List<OrderDetail>();
                foreach(var cartId in shopCartMap.CartIds)
                {
                    // get cart item
                    var cartItem = await _context.Carts.Where(x=>x.Id==cartId).FirstOrDefaultAsync();
                    if(cartItem==null)  return new ApiResult<int>(false, Message:$"Không có giỏ hàng với Id: {cartId}");
                    // get product detail of cart item
                    var product_detail = await _context.ProductDetails.Where(x=>x.Id==cartItem.ProductDetail_Id).FirstOrDefaultAsync();
                    // get price 
                    var price = (await _context.Products.Where(p => p.Id== product_detail.ProductId).FirstOrDefaultAsync()).Price;
                    // add order detail mới vào list
                    orderDetails.Add(new OrderDetail
                    {
                        ProductDetail_Id = cartItem.ProductDetail_Id,
                        Quantity = cartItem.Quantity,
                        Price = price
                    });
                    // trừ số lượng ở Db
                    if(cartItem.Quantity > product_detail.Stock)
                    {
                        return new ApiResult<int>(false, Message:"Quá số lượng tồn kho!");
                    }
                    product_detail.Stock -= cartItem.Quantity;
                    _context.ProductDetails.Update(product_detail);
                    // xóa cart item tương ứng trong giỏ hàng
                    _context.Carts.Remove(cartItem);
                }
                // thêm order và orderdetails vào context 
                new_order.OrderDetails = orderDetails;
                _context.Orders.Add(new_order);
            }
            // lưu data mới
            await _context.SaveChangesAsync();

            return new ApiResult<int>(true, Message:"Tạo đơn thành công!");
        } 

        public async Task<ApiResult<List<OrderVm>>> GetUserOrders(int userId, string state = "")
        {
            var query = from o in _context.Orders 
                        join sh in _context.Shops on o.ShopId equals sh.Id
                        where o.UserId == userId
                        select new {o, sh};
                                    
            if(query==null)
                return new ApiResult<List<OrderVm>>(false, Message:"Bạn chưa có đơn hàng nào!");

            if(!string.IsNullOrEmpty(state))
                query = query.Where(x=>x.o.State==state);

            query = query.OrderByDescending(x=>x.o.OrderDate);
            
            var data = await query.Select(x=> new OrderVm
            {
                Id = x.o.Id,
                OrderDate = x.o.OrderDate,
                DateModified = x.o.DateModified,
                UserId = x.o.UserId,
                ShopId = x.o.ShopId,
                ShopName = x.sh.Name,
                ShipName = x.o.ShipName,
                ShipAddress = x.o.ShipAddress,
                ShipPhone = x.o.ShipPhone,
                State = x.o.State,
                CancelReason = x.o.CancelReason
            }).ToListAsync();

            for(int i=0; i<data.Count; i++)
            {
                data[i].OrderDetails = await GetOrderDetails(data[i].Id);
                data[i].TotalPrice = data[i].OrderDetails.Sum(d => d.Price*d.Quantity);
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

        public async Task<ApiResult<int>> CancelUnconfirmedOrder(int orderId, string cancelReason)
        {
            var order = await _context.Orders.Where(x=>x.Id==orderId).FirstOrDefaultAsync();
            if(order==null) return new ApiResult<int>(false, Message:$"Không tìm thấy đơn hàng với Id: {orderId}");
            
            if(!String.Equals(order.State, "Chờ xác nhận")) 
                return new ApiResult<int>(false, Message:$"Bạn chỉ có thể hủy đơn chưa xác nhận");

            var order_details = await _context.OrderDetails
                            .Where(x=>x.OrderId==orderId).ToListAsync();

            if(String.IsNullOrEmpty(cancelReason))  cancelReason = "Lý do khác";
            

            // 1. chuyển trạng thái đơn hàng thành 'đã hủy'
            order.State = "Đã hủy";
            // 2. cập nhật lý do hủy đơn
            order.CancelReason = cancelReason;
            _context.Orders.Update(order);
            // 3. cộng lại số lượng sản phẩm đã đặt vào stock
            foreach(var order_detail in order_details)
            {
                var product_detail = await _context.ProductDetails    
                                    .Where(x=>x.Id==order_detail.ProductDetail_Id)
                                    .FirstOrDefaultAsync();
                product_detail.Stock += order_detail.Quantity;
                _context.ProductDetails.Update(product_detail);
            }
            await _context.SaveChangesAsync();

            
            
            return new ApiResult<int>(true, Message:"Hủy đơn hàng thành công!");
        }

        // public async Task<ApiResult<int>> CreateOrder(int userId, CheckOutRequest request)
        // {
            // ****** bên web tự gom shopId với list cartIds*****
            // // 1. tạo order mới
            // var new_order = new Order
            // {
            //     OrderDate = DateTime.Now,
            //     UserId = userId,
            //     ShopId = request.ShopId,
            //     ShipName = request.ShipName,
            //     ShipAddress = request.ShipAddress,
            //     ShipPhone = request.ShipPhone,
            //     State = "Chờ xử lý"
            // };
            // // 2. tạo mới các order details
            // var orderDetails = new List<OrderDetail>();
            // foreach(var cartId in request.CartIds)
            // {
            //     // get cart item
            //     var cartItem = await _context.Carts.Where(x=>x.Id==cartId).FirstOrDefaultAsync();
            //     if(cartItem==null)  return new ApiResult<int>(false, Message:$"No cart item with this id: {cartId}");
            //     // get product detail of cart item
            //     var product_detail = await _context.ProductDetails.Where(x=>x.Id==cartItem.ProductDetail_Id).FirstOrDefaultAsync();
            //     // add order detail mới vào list
            //     orderDetails.Add(new OrderDetail
            //     {
            //         ProductDetail_Id = cartItem.ProductDetail_Id,
            //         Quantity = cartItem.Quantity,
            //         Price = cartItem.Price
            //     });
            //     // trừ số lượng ở Db
            //     // var productDetail = await _context.ProductDetails
            //     //     .Where(x=>x.Id==item.ProductDetail_Id)
            //     //     .FirstOrDefaultAsync();
            //     if(cartItem.Quantity > product_detail.Stock)
            //     {
            //         return new ApiResult<int>(false, Message:"Out of stock");
            //     }
            //     product_detail.Stock -= cartItem.Quantity;
            //     _context.ProductDetails.Update(product_detail);
            //     // xóa cart item tương ứng trong giỏ hàng
            //     _context.Carts.Remove(cartItem);
            // }
            // // lưu thông tin 
            // new_order.OrderDetails = orderDetails;
            // _context.Orders.Add(new_order);
            // await _context.SaveChangesAsync();

        //      return new ApiResult<int>(true, Message:"Create order successful");
        // }

        // public async Task<ApiResult<int>> CreateOrder(int userId, CheckOutRequest request)
        // {
        //     // 1. tạo order mới
        //     var new_order = new Order
        //     {
        //         OrderDate = DateTime.Now,
        //         UserId = userId,
        //         ShopId = request.ShopId,
        //         ShipName = request.ShipName,
        //         ShipAddress = request.ShipAddress,
        //         ShipPhone = request.ShipPhone,
        //         State = "Chờ xử lý"
        //     };
        //     // 2. tạo mới các order details
        //     var orderDetails = new List<OrderDetail>();
        //     foreach(var item in request.OrderDetails)
        //     {
        //         // add order detail mới vào list
        //         orderDetails.Add(new OrderDetail
        //         {
        //             ProductDetail_Id = item.ProductDetail_Id,
        //             Quantity = item.Quantity,
        //             Price = item.Price
        //         });
        //         // trừ số lượng ở Db
        //         var productDetail = await _context.ProductDetails
        //             .Where(x=>x.Id==item.ProductDetail_Id)
        //             .FirstOrDefaultAsync();
        //         if(item.Quantity > productDetail.Stock)
        //         {
        //             return new ApiResult<int>(false, Message:"Out of stock");
        //         }
        //         productDetail.Stock -= item.Quantity;
        //         _context.ProductDetails.Update(productDetail);
        //     }
        //     new_order.OrderDetails = orderDetails;
        //     _context.Orders.Add(new_order);
        //     await _context.SaveChangesAsync();

        //     return new ApiResult<int>(true, Message:"Create order successful");
        // }
    }
}

        

