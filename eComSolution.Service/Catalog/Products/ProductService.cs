using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eComSolution.Data.EF;
using eComSolution.ViewModel.Catalog.ProductDetails;
using eComSolution.ViewModel.Catalog.ProductImages;
using eComSolution.ViewModel.Catalog.Products;
using eShopSolution.ViewModels.Common;
using Microsoft.EntityFrameworkCore;

namespace eComSolution.Service.Catalog.Products
{
    public class ProductService : IProductService
    {
        private readonly EComDbContext _context;

        public ProductService(EComDbContext context)
        {
            _context = context;
        }

        public async Task<ApiResult<PagedResult<ProductVm>>> GetProductPaging(GetProductsRequest request)
        {
            // 1. join bảng
            // var query = from p in _context.Products
            //             join c in _context.Categories on p.CategoryId equals c.Id
            //             join pd in _context.ProductDetails on p.Id equals pd.ProductId
            //             join pi in _context.ProductImages on p.Id equals pi.ProductId
            //             select new {p, c, pd, pi};
            var query = from p in _context.Products
                        join c in _context.Categories on p.CategoryId equals c.Id
                        join sh in _context.Shops on p.ShopId equals sh.Id
                        where p.IsDeleted == false
                        select new {p, c, sh};

            // 2. filter
            // theo keyword
            if (!string.IsNullOrEmpty(request.Keyword))
                query = query.Where(x => x.p.Name.Contains(request.Keyword));
            // theo categoryId
            if(request.CategoryId != 0)
                query = query.Where(x => x.p.CategoryId == request.CategoryId);           
            // theo shopId
            if(request.ShopId !=0 )
                query = query.Where(x => x.p.ShopId == request.ShopId);
            
            // 3. sắp xếp theo SortBy
            switch(request.SortBy)
            {
                case "latest":
                    query = query.OrderByDescending(x => x.p.DateCreated);
                    break;
                case "priceUp":
                    query = query.OrderBy(x => x.p.Price);
                    break;    
                case "priceDown":
                    query = query.OrderByDescending(x => x.p.Price);
                    break;
                default:
                    query = query.OrderByDescending(x => x.p.ViewCount); 
                    break;    
            }

            // 4. phân trang
            int totalRow = await query.CountAsync();

            var data = await query.Skip((request.PageIndex - 1)*request.PageSize)
                    .Take(request.PageSize)
                    .Select(x => new ProductVm()
                    {
                        Id = x.p.Id,
                        Name = x.p.Name,
                        Description = x.p.Description,
                        Price = x.p.Price,
                        OriginalPrice = x.p.OriginalPrice,
                        ViewCount = x.p.ViewCount,
                        DateCreated = x.p.DateCreated                              
                    }).ToListAsync();

            for(int i=0; i<data.Count; i++)
            {
                data[i].Details = await GetProductDetails(data[i].Id);
                data[i].Images = await GetProductImages(data[i].Id);
                data[i].TotalStock = data[i].Details.Sum(d => d.Stock);
            }
            // 5. tạo paged result 
            var pagedResult = new PagedResult<ProductVm>()
            {
                TotalRecords = totalRow,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Items = data
            };
            
            return new ApiResult<PagedResult<ProductVm>>(true, ResultObj:pagedResult);
        }

        public async Task<List<ProductDetailVm>> GetProductDetails(int productId)
        {
            var query = _context.ProductDetails
            .Where(pd => pd.ProductId == productId && pd.IsDeleted == false);

            var data = await query.Select(x => new ProductDetailVm()
            {
                ProductId = x.ProductId,
                Color = x.Color,
                Size = x.Size,
                Stock = x.Stock
            }).ToListAsync();

            return data;
        }

        public async Task<List<ProductImageVm>> GetProductImages(int productId)
        {
            var query = _context.ProductImages
            .Where(pi => pi.ProductId == productId);

            if(query == null)   return null;

            var data = await query.OrderBy(x => x.SortOrder).Select(x => new ProductImageVm()
            {
                ProductId = x.ProductId,
                ImagePath = x.ImagePath,
                IsDefault = x.IsDefault,
                ColorName = x.ColorName,
                IsSizeDetail = x.IsSizeDetail
            }).ToListAsync();

            return data;
        }
    }
}