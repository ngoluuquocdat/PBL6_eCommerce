using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using eComSolution.Data.EF;
using eComSolution.Data.Entities;
using eComSolution.Service.Common;
using eComSolution.ViewModel.Catalog.ProductDetails;
using eComSolution.ViewModel.Catalog.ProductImages;
using eComSolution.ViewModel.Catalog.Products;
using eShopSolution.ViewModels.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace eComSolution.Service.Catalog.Products
{
    public class ProductService : IProductService
    {
        private readonly EComDbContext _context;
        private readonly IStorageService _storageService;

        public ProductService(EComDbContext context, IStorageService storageService)
        {
            _context = context;
            _storageService = storageService;
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
                Id = x.Id,
                ProductId = x.ProductId,
                ImagePath = "/storage/"+x.ImagePath,
                IsDefault = x.IsDefault,
                ColorName = x.ColorName,
                IsSizeDetail = x.IsSizeDetail
            }).ToListAsync();

            return data;
        }

        public async Task<ApiResult<int>> Create(CreateProductRequest request)
        {
            // 1. tạo list các product details
            var product_details = new List<ProductDetail>();
            if(request.Details==null || request.Details.Count==0)
                return new ApiResult<int>(false, Message:"Details must not null");

            foreach(var productDetailVm in request.Details)
            {
                product_details.Add(new ProductDetail()
                {
                    Color = productDetailVm.Color,
                    Size = productDetailVm.Size,
                    Stock = productDetailVm.Stock,
                    IsDeleted = false
                });
            }

            // 2. tạo list các product images
            //var product_images = new List<ProductImage>();
            // cách 1:
            // if(request.Images==null || request.Images.Count==0)
            //     return new ApiResult<int>(false, Message:"Details must not null");
            // if(request.NewImages!=null)
            // {
            //     foreach(var image in request.NewImages)
            //     {
            //         product_images.Add(new ProductImage()
            //         {
            //             IsDefault = image.IsDefault,
            //             ColorName = image.ColorName,
            //             IsSizeDetail = image.IsSizeDetail,
            //             ImagePath =  await this.SaveFile(image.ImageFile)
            //         });            
            //     }
            // }
            // cách 2:
            // for(int i=0; i<request.ImageInfos.Count; i++)
            // {
            //     product_images.Add(new ProductImage()
            //         {
            //             IsDefault = request.ImageInfos[i].IsDefault,
            //             ColorName = request.ImageInfos[i].ColorName,
            //             IsSizeDetail = request.ImageInfos[i].IsSizeDetail,
            //             ImagePath =  await this.SaveFile(request.NewImages[i])
            //         });   
            // }

            // 3. tạo product mới
            var product = new Product()
            {
                Name = request.Name,
                Description = request.Description,
                OriginalPrice = request.OriginalPrice,
                Price = request.Price,
                ViewCount = 0,
                DateCreated = DateTime.Now,
                CategoryId = request.CategoryId,
                ShopId = request.ShopId,
                IsDeleted = false,
                ProductDetails = product_details
                //ProductImages = product_images
            };
             _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return new ApiResult<int>(true, product.Id);    // trả về Id của product mới
        }

        public async Task<ApiResult<int>> AddImage(int productId, CreateProductImageRequest request)
        {
            var productImage = new ProductImage()
            {
                ProductId = productId,
                IsDefault = request.IsDefault,                
                SortOrder = request.SortOrder,
                ColorName = request.ColorName,
                IsSizeDetail = request.IsSizeDetail
            };

            if (request.ImageFile != null)      // nếu có file ảnh thì mới add
            {
                productImage.ImagePath = await this.SaveFile(request.ImageFile);
                _context.ProductImages.Add(productImage);
            }

            await _context.SaveChangesAsync();

            return new ApiResult<int>(true, ResultObj:productImage.Id);
        }

        private async Task<string> SaveFile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _storageService.SaveFileAsync(file.OpenReadStream(), fileName);
            return fileName;
        }
    }
}