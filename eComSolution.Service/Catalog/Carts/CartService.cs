using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eComSolution.Data.EF;
using eComSolution.Data.Entities;
using eComSolution.ViewModel.Catalog.Carts;
using eShopSolution.ViewModels.Common;
using Microsoft.EntityFrameworkCore;

namespace eComSolution.Service.Catalog.Carts
{
    public class CartService : ICartService
    {
        private readonly EComDbContext _context;
        public CartService(EComDbContext context)
        {
            _context = context;
        }
        public async Task<ApiResult<int>> AddToCart(int userId, AddToCartRequest request)
        {    
            // check valid properties request
            if(request.IsValid()==false)
                return new ApiResult<int>(false, Message:"Thông tin không hợp lệ, vui lòng nhập lại");  
            // check product detail
            var product_detail = await _context.ProductDetails.FirstOrDefaultAsync(x=>x.Id==request.ProductDetail_Id);     
            if(product_detail==null)
                return new ApiResult<int>(false, Message:"Phân loại hàng không tồn tại"); 
            if(product_detail.IsDeleted==true)
                return new ApiResult<int>(false, Message:"Phân loại hàng đã bị xóa"); 
                
            int stock = (await _context.ProductDetails
                    .Where(x=>x.Id == request.ProductDetail_Id)
                    .FirstOrDefaultAsync()).Stock; 

            if(request.Quantity > stock )
                return new ApiResult<int>(false, Message:"Quá số lượng tồn kho!");

            var current_item = await _context.Carts
                .Where(x => x.ProductDetail_Id == request.ProductDetail_Id && x.UserId==userId)
                .FirstOrDefaultAsync();
            
            if(current_item != null)
            {
                current_item.Quantity += request.Quantity;               

                if(current_item.Quantity > stock)
                {
                    return new ApiResult<int>(false, Message:"Quá số lượng tồn kho!");
                }
                else
                {
                    _context.Carts.Update(current_item);
                }
            }
            else
            {
                Cart cart_item = new Cart()
                {
                    UserId = userId,
                    ProductDetail_Id = request.ProductDetail_Id,
                    Quantity = request.Quantity
                    // Price = request.Price
                }; 
                _context.Carts.Add(cart_item);
            }
            
            await _context.SaveChangesAsync();
            return new ApiResult<int>(true, Message:"Thêm vào giỏ hàng thành công!");
        }


        public async Task<ApiResult<List<CartItem>>> GetCartItems(int userId)
        {
            List<CartItem> data = new List<CartItem>();
            var query = from c in _context.Carts
                        join pd in _context.ProductDetails on c.ProductDetail_Id equals pd.Id
                        join p in _context.Products on pd.ProductId equals p.Id
                        join sh in _context.Shops on p.ShopId equals sh.Id
                        where c.UserId == userId
                        select new {c, pd, p, sh};

            if(query==null || query.Count()==0)
                return new ApiResult<List<CartItem>>(false, Message:"Bạn chưa có sản phẩm nào trong giỏ hàng!");
            
            var list_records = await query.OrderByDescending(x=>x.c.Id).ToListAsync();  

            foreach(var record in list_records)
            {
                //var cart_item = record.c;
                if(record.c.Quantity > record.pd.Stock && record.pd.Stock>0)
                {
                    record.c.Quantity = record.pd.Stock;
                    await _context.SaveChangesAsync();
                }
                string path = "";
                int productId = record.p.Id;
                // ảnh thumbnail
                // var image =  await _context.ProductImages
                //     .Where(x=>x.ProductId==productId && x.IsDefault==true)
                //     .FirstOrDefaultAsync();
                // ảnh theo color
                var image =  await _context.ProductImages
                    .Where(x=>x.ProductId==productId && x.ColorName==record.pd.Color)
                    .FirstOrDefaultAsync();
                
                if(image!=null) path = image.ImagePath;
                
                data.Add(new CartItem()
                {
                    Id = record.c.Id,
                    UserId = record.c.UserId,
                    ProductId = record.p.Id,
                    ProductDetail_Id = record.c.ProductDetail_Id,
                    ShopId = record.p.ShopId,
                    ProductName = record.p.Name,
                    Color = record.pd.Color,
                    Size = record.pd.Size,
                    ShopName = record.sh.Name,
                    Quantity = record.c.Quantity,
                    Stock = record.pd.Stock,
                    Price = record.p.Price,
                    Image = path,
                    IsShopAvailable = !record.sh.Disable,
                    IsProductDetailAvailable = !record.pd.IsDeleted,
                    IsRemainInStock = !(record.pd.Stock == 0)
                });
            }
            
            return new ApiResult<List<CartItem>>(true, ResultObj:data);
        }

        public async Task<ApiResult<int>> GetCartItemsCount(int userId)
        {
            int count = await _context.Carts.CountAsync(x=>x.UserId==userId);
            return new ApiResult<int>(true, ResultObj: count); 
        }

        public async Task<ApiResult<int>> RemoveFromCart(int userId, int cartId)
        {
            var cart_item = await _context.Carts.Where(x=>x.Id == cartId).FirstOrDefaultAsync();
            if(cart_item==null)
                return new ApiResult<int>(false, Message:$"Không tồn tại cart item với Id:{cartId}"); 
            if(cart_item.UserId!=userId)
                return new ApiResult<int>(false, Message:$"Bạn không có cart item với Id: {cartId}"); 

            _context.Carts.Remove(cart_item);
            await _context.SaveChangesAsync();
            return new ApiResult<int>(true, Message:"Bỏ sản phẩm khỏi giỏ thành công!");
        }

        public async Task<ApiResult<int>> RemoveMultiCarts(int userId, List<int> cartIds)
        {

            foreach(var cartId in cartIds)
            {
                var cart_item = await _context.Carts.Where(x=>x.Id == cartId).FirstOrDefaultAsync();
                if(cart_item==null)
                    return new ApiResult<int>(false, Message:$"Không tồn tại cart item với Id:{cartId}"); 
                if(cart_item.UserId!=userId)
                    return new ApiResult<int>(false, Message:$"Bạn không có cart item với Id: {cartId}");

                _context.Carts.Remove(cart_item);          
            }
    
            await _context.SaveChangesAsync();
            return new ApiResult<int>(true, Message:"Bỏ sản phẩm khỏi giỏ thành công!");
        }

        public async Task<ApiResult<int>> UpdateCartItem(int userId, UpdateCartItemRequest request)
        {
            // check valid properties request
            if(request.IsValid()==false)
                return new ApiResult<int>(false, Message:"Thông tin không hợp lệ, vui lòng nhập lại"); 
            var cart_item = await _context.Carts.Where(x=>x.Id==request.CartId).FirstOrDefaultAsync();
            if(cart_item==null)
                return new ApiResult<int>(false, Message:$"Không tồn tại cart item với Id:{request.CartId}"); 
            if(cart_item.UserId!=userId)
                return new ApiResult<int>(false, Message:$"Bạn không có cart item với Id: {request.CartId}"); 

            int stock = (await _context.ProductDetails
                    .Where(x=>x.Id == cart_item.ProductDetail_Id)
                    .FirstOrDefaultAsync()).Stock; 

            if(request.Quantity > stock)
                return new ApiResult<int>(false, Message:"Quá số lượng tồn kho!");

            cart_item.Quantity = request.Quantity;
            _context.Carts.Update(cart_item);
            await _context.SaveChangesAsync();
            return new ApiResult<int>(true, Message:"Cập nhật giỏ hàng thành công!");
        }
    }
}