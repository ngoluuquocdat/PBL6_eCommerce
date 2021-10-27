using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eComSolution.ViewModel.Catalog.Carts;
using eComSolution.ViewModel.Catalog.Histories;
using eShopSolution.ViewModels.Common;

namespace eComSolution.Service.Catalog.Carts
{
    public interface ICartService
    {
        Task<ApiResult<List<CartItem>>> GetCartItems(int userId);

        Task<ApiResult<int>> AddToCart(int userId, AddToCartRequest request);

        Task<ApiResult<int>> UpdateCartItem(UpdateCartItemRequest request);

        Task<ApiResult<int>> RemoveFromCart(int cartId);

        Task<ApiResult<int>> RemoveMultiCarts(List<int> cartIds);

    }
}