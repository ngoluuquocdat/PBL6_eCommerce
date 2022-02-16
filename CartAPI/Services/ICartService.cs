using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CartAPI.ViewModels;
using CartAPI.ViewModels.Common;
using Database.Entities;

namespace CartAPI.Services
{
    public interface ICartService
    {
        Task<ApiResult<List<CartItem>>> GetCartItems(int userId);

        Task<ApiResult<List<CartItem>>> GetCartItemsByIds(int userId, List<int> cartIds);

        Task<ApiResult<int>> AddToCart(int userId, AddToCartRequest request);
        
        Task<ApiResult<int>> GetCartItemsCount(int userId);

        Task<ApiResult<int>> UpdateCartItem(int userId, UpdateCartItemRequest request);

        Task<ApiResult<int>> RemoveFromCart(int userId, int cartId);

        Task<ApiResult<int>> RemoveMultiCarts(int userId, List<int> cartIds);

        Task<List<Function>> GetPermissions(int userId);
    }
}