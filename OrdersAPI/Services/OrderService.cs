using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Database;
using Database.Entities;
using Microsoft.EntityFrameworkCore;
using OrdersAPI.ViewModels;
using OrdersAPI.ViewModels.Common;

namespace OrdersAPI.Services
{
    public class OrderService : IOrderService
    {
        private readonly EComDbContext _context;

        public OrderService(EComDbContext context)
        {
            _context = context;
        }
        public async Task<List<Function>> GetPermissions(int userId){
            var user =  await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if(user == null) return null;

            var query = from _user in _context.Users
            join _groupuser in _context.GroupUsers on _user.Id equals _groupuser.UserId
            join _permission in _context.Permissions on _groupuser.GroupId equals _permission.GroupId
            join _function in _context.Functions on _permission.FunctionId equals _function.Id
            where _user.Id == userId
            select new Function { 
                ActionName = _function.ActionName
            }; 
            return query.Distinct().ToList();
        }

        public async Task<ApiResult<int>> CheckCartItems(int userId, List<int> cartIds)
        {
            foreach(var cartId in cartIds)
            {
                var query = await (from c in _context.Carts
                        join pd in _context.ProductDetails on c.ProductDetail_Id equals pd.Id
                        join p in _context.Products on pd.ProductId equals p.Id
                        join sh in _context.Shops on p.ShopId equals sh.Id
                        where c.Id == cartId
                        select new {c, pd, p, sh}).FirstOrDefaultAsync();

                if(query==null)
                {
                    return new ApiResult<int>(false, Message:$"Cart item với Id: {cartId} không tồn tại, vui lòng kiểm tra lại giỏ hàng");
                }    
                if(query.c.UserId != userId)   
                {
                    return new ApiResult<int>(false, Message:$"Bạn không có cart item với Id: {query.c.Id}, vui lòng kiểm tra lại giỏ hàng");
                } 
                if(query.sh.Disable==true)
                {
                    return new ApiResult<int>(false, Message:$"Shop: {query.sh.Name} đã ngừng hoạt động, vui lòng kiểm tra lại giỏ hàng");
                }
                if(query.pd.IsDeleted==true)
                {
                    return new ApiResult<int>(false, Message:"Có phân loại hàng đã bị xóa, vui lòng kiểm tra lại giỏ hàng");
                }
                if(query.pd.Stock==0)
                {
                    return new ApiResult<int>(false, Message:"Có phân loại hàng đã hết hàng, vui lòng kiểm tra lại giỏ hàng");
                }
                if(query.c.Quantity > query.pd.Stock)
                {
                    return new ApiResult<int>(false, Message:"Đặt quá số lượng tồn, vui lòng kiểm tra lại giỏ hàng");
                }
            }

            return null;
        }


        public async Task<ApiResult<int>> CreateOrders(int userId, CheckOutRequest request)
        {
            // ***** chỉ nhận vào input là list cartIds là đủ
            // check valid properties request
            if(request.IsValid()==false)
                return new ApiResult<int>(false, Message:"Thông tin không hợp lệ, vui lòng nhập lại");
            // check các cart items
            var error_result = await CheckCartItems(userId, request.CartIds);
            if(error_result!=null) return error_result;
            // cho phép tạo các đơn hàng
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
                    //if(cartItem==null)  return new ApiResult<int>(false, Message:$"Không có giỏ hàng với Id: {cartId}");
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
            if(!(string.IsNullOrEmpty(state)) && (!String.Equals(state, "Chờ xác nhận")
                    &&!String.Equals(state, "Đã xác nhận")
                        &&!String.Equals(state, "Đã hủy")))
                return new ApiResult<List<OrderVm>>(false, Message:"Thông tin không hợp lệ, vui lòng nhập lại");

            var query = from o in _context.Orders 
                        join sh in _context.Shops on o.ShopId equals sh.Id
                        where o.UserId == userId
                        select new {o, sh};

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

        public async Task<ApiResult<List<OrderVm>>> GetShopOrders(int userId, string state = "")
        {
            if(!(string.IsNullOrEmpty(state)) && (!String.Equals(state, "Chờ xác nhận")
                    &&!String.Equals(state, "Đã xác nhận")
                        &&!String.Equals(state, "Đã hủy")))
                return new ApiResult<List<OrderVm>>(false, Message:"Thông tin không hợp lệ, vui lòng nhập lại");

            // get shop
            var shop_query = await (from sh in _context.Shops
                       join u in _context.Users on sh.Id equals u.ShopId
                       where u.Id == userId
                       select sh).FirstOrDefaultAsync();  

            if(shop_query==null)  
                return new ApiResult<List<OrderVm>>(false, Message:"Chỉ có tài khoản chủ shop mới được thực hiện hành động này");

            var query = from o in _context.Orders 
                        join sh in _context.Shops on o.ShopId equals sh.Id
                        where o.ShopId == shop_query.Id
                        select new {o, sh};

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

        public async Task<ApiResult<int>> CancelUnconfirmedOrder(int userId, CancelOrderRequest request)
        {
            if(!request.IsValid())
                return new ApiResult<int>(false, Message:$"Thông tin không hợp lệ");
                
            var order = await _context.Orders.Where(x=>x.Id==request.OrderId).FirstOrDefaultAsync();
            if(order==null) return new ApiResult<int>(false, Message:$"Không tìm thấy đơn hàng với Id: {request.OrderId}");

            // check đơn hàng chính chủ
            if(order.UserId!=userId) 
                return new ApiResult<int>(false, Message:$"Bạn không có đơn hàng với Id: {request.OrderId}");
            
            if(!String.Equals(order.State, "Chờ xác nhận")) 
                return new ApiResult<int>(false, Message:$"Bạn chỉ có thể hủy đơn chưa xác nhận");

            var order_details = await _context.OrderDetails
                            .Where(x=>x.OrderId==request.OrderId).ToListAsync();

            if(String.IsNullOrEmpty(request.CancelReason))  request.CancelReason = "Lý do khác";
            

            // 1. chuyển trạng thái đơn hàng thành 'đã hủy'
            order.State = "Đã hủy";
            order.DateModified = DateTime.Now;
            // 2. cập nhật lý do hủy đơn
            order.CancelReason = request.CancelReason;
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

        public async Task<ApiResult<int>> ConfirmOrder(int userId, int orderId)
        {
            // get shop & check
            var shop = await (from sh in _context.Shops
                       join u in _context.Users on sh.Id equals u.ShopId
                       where u.Id == userId
                       select sh).FirstOrDefaultAsync();  

            if(shop==null)  
                return new ApiResult<int>(false, Message:"Chỉ có tài khoản chủ shop mới được thực hiện hành động này");

            // get order
            var order = await _context.Orders.Where(x=>x.Id==orderId).FirstOrDefaultAsync();
            if(order==null) return new ApiResult<int>(false, Message:$"Không tìm thấy đơn hàng với Id: {orderId}");  

            // check đơn hàng chính chủ của shop
            if(order.ShopId!=shop.Id) 
                return new ApiResult<int>(false, Message:$"Shop của bạn không có đơn hàng với Id: {orderId}");
            
            if(!String.Equals(order.State, "Chờ xác nhận")) 
                return new ApiResult<int>(false, Message:$"Bạn chỉ có thể xác nhận đơn chưa xác nhận");  

            // chuyển trạng thái đơn hàng thành 'Đã xác nhận'
            order.State = "Đã xác nhận";
            order.DateModified = DateTime.Now;
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
            
            return new ApiResult<int>(true, Message:"Xác nhận đơn hàng thành công!");
        }

        public async Task<ApiResult<int>> CancelOrder(int userId, CancelOrderRequest request)
        {
            // get shop & check
            var shop = await (from sh in _context.Shops
                       join u in _context.Users on sh.Id equals u.ShopId
                       where u.Id == userId
                       select sh).FirstOrDefaultAsync();  

            if(shop==null)  
                return new ApiResult<int>(false, Message:"Chỉ có tài khoản chủ shop mới được thực hiện hành động này");

            // get order
            var order = await _context.Orders.Where(x=>x.Id==request.OrderId).FirstOrDefaultAsync();
            if(order==null) return new ApiResult<int>(false, Message:$"Không tìm thấy đơn hàng với Id: {request.OrderId}");  

            // check đơn hàng chính chủ của shop
            if(order.ShopId!=shop.Id) 
                return new ApiResult<int>(false, Message:$"Shop của bạn không có đơn hàng với Id: {request.OrderId}");
            
            if(String.Equals(order.State, "Đã hủy")) 
                return new ApiResult<int>(false, Message:$"Đơn hàng này đã bị hủy trước đó");  

            var order_details = await _context.OrderDetails
                            .Where(x=>x.OrderId==request.OrderId).ToListAsync();

            if(String.IsNullOrEmpty(request.CancelReason))  request.CancelReason = "Lý do khác";    

            // 1. chuyển trạng thái đơn hàng thành 'đã hủy'
            order.State = "Đã hủy";
            order.DateModified = DateTime.Now;
            // 2. cập nhật lý do hủy đơn
            order.CancelReason = request.CancelReason;
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
    }
}

        

