using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Database.Entities;
using eComSolution.ViewModel.Catalog.Products;
using ProductAPI.ViewModels.ProductImages;
using ProductAPI.ViewModels.Common;
using ProductAPI.ViewModels.ProductDetails;
using ProductAPI.ViewModels.Products;

namespace ProductAPI.Services
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

        Task<ApiResult<int>> UpdateMainInfo(int userId, UpdateProductMainInfoRequest request);

        Task<ApiResult<int>> UpdateProductDetails(int userId, UpdateProductDetailsRequest request);

        Task<ApiResult<int>> AddImage(int productId, CreateProductImageRequest request);

        Task<ApiResult<int>> RemoveImage(int imageId);

        Task<List<Function>> GetPermissions(int userId);
    }
}