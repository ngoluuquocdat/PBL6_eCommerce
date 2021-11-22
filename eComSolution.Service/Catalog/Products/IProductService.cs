using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eComSolution.ViewModel.Catalog.ProductDetails;
using eComSolution.ViewModel.Catalog.ProductImages;
using eComSolution.ViewModel.Catalog.Products;
using eShopSolution.ViewModels.Common;

namespace eComSolution.Service.Catalog.Products
{
    public interface IProductService
    {
        Task<ApiResult<PagedResult<ProductMainInfoVm>>> GetProductPaging(GetProductsRequest request);

        Task<List<ProductDetailVm>> GetProductDetails(int productId);

        Task<List<ProductImageVm>> GetProductImages(int productId);

        Task<ApiResult<ProductVm>> GetProductById(int productId);

        Task<ApiResult<int>> Create(int userId, CreateProductRequest request);

        Task<ApiResult<int>> Delete(int userId, int productId);

        Task<ApiResult<int>> Update(int userId, UpdateProductRequest request);

        Task<int> AddDetail(int productId, ProductDetailVm dto);

        Task<int> UpdateDetail(int productId, ProductDetailVm dto);

        Task<int> DeleteDetail(int productId, string color, string size);

        Task<ApiResult<int>> AddImage(int productId, CreateProductImageRequest request);

        Task<ApiResult<int>> RemoveImage(int imageId);

    }
}