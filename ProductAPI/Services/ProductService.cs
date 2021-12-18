using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using eComSolution.ViewModel.Catalog.Products;
using ProductAPI.ViewModels.ProductImages;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Database;
using Database.Entities;
using ProductAPI.ViewModels.Common;
using ProductAPI.ViewModels.ProductDetails;
using ProductAPI.ViewModels.Products;

namespace ProductAPI.Services
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

        public async Task<ApiResult<PagedResult<ProductMainInfoVm>>> GetProductPaging(GetProductsRequest request)
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
                        where p.IsDeleted == false && sh.Disable == false
                        select new {p, c, sh};

            // 2. filter
            // theo id
            if(request.ProductId != 0)
                query = query.Where(x => x.p.Id == request.ProductId);
            // theo keyword
            if (!string.IsNullOrEmpty(request.Keyword))
                query = query.Where(x => x.p.Name.Contains(request.Keyword));
            // theo categoryId
            if(request.CategoryId != 0)
                query = query.Where(x => x.p.CategoryId == request.CategoryId); 
            // theo gender
            if(request.Gender != 0)
                query = query.Where(x => x.p.Gender == request.Gender);           
            // theo shopId
            if(request.ShopId !=0 )
                query = query.Where(x => x.p.ShopId == request.ShopId);
            
            // 3. sắp xếp theo SortBy
            switch(request.SortBy)
            {
                case "popular":
                    query = query.OrderByDescending(x => x.p.ViewCount);
                    break;
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
                    query = query.OrderByDescending(x => x.p.DateCreated); 
                    break;    
            }

            // 4. phân trang
            int totalRow = await query.CountAsync();

            var data = await query.Skip((request.PageIndex - 1)*request.PageSize)
                    .Take(request.PageSize)
                    .Select(x => new ProductMainInfoVm()
                    {
                        Id = x.p.Id,
                        Name = x.p.Name,
                        Price = x.p.Price,
                        ViewCount = x.p.ViewCount                         
                    }).ToListAsync();

            // lấy path của ảnh thumbnail + totalstock
            for(int i=0; i<data.Count; i++)
            {
                var thumbnail_image = await _context.ProductImages
                    .Where(x=>x.ProductId==data[i].Id&&x.IsDefault==true)
                    .FirstOrDefaultAsync();
                if(thumbnail_image!=null)
                {
                    data[i].ThumbnailImage = "/storage/"+thumbnail_image.ImagePath;
                }
                else
                {
                    data[i].ThumbnailImage = "";
                }
                data[i].TotalStock =  (await GetProductDetails(data[i].Id)).Sum(d=>d.Stock);          
            }
            // 5. tạo paged result 
            var pagedResult = new PagedResult<ProductMainInfoVm>()
            {
                TotalRecords = totalRow,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Items = data
            };
            
            return new ApiResult<PagedResult<ProductMainInfoVm>>(true, ResultObj:pagedResult);
        }

        public async Task<ApiResult<PagedResult<ProductMainInfoManageVm>>> GetProductPagingManage(int userId, GetProductsManageRequest request)
        {
            // lấy ra shopId của chủ shop
            var shopId = (await _context.Users.FirstOrDefaultAsync(x=>x.Id==userId)).ShopId;
            if(shopId==null)
                return new ApiResult<PagedResult<ProductMainInfoManageVm>>(false, Message:"Chỉ có tài khoản chủ shop mới được thực hiện hành động này");

            // 1. join bảng
            // var query = from p in _context.Products
            //             join c in _context.Categories on p.CategoryId equals c.Id
            //             join pd in _context.ProductDetails on p.Id equals pd.ProductId
            //             join pi in _context.ProductImages on p.Id equals pi.ProductId
            //             select new {p, c, pd, pi};
            var query = from p in _context.Products
                        join c in _context.Categories on p.CategoryId equals c.Id
                        join sh in _context.Shops on p.ShopId equals sh.Id
                        where p.IsDeleted == false && p.ShopId == shopId
                        select new {p, c, sh};

            // 2. filter
            // theo id
            if(request.ProductId != 0)
                query = query.Where(x => x.p.Id == request.ProductId);
            // theo keyword
            if (!string.IsNullOrEmpty(request.Keyword))
                query = query.Where(x => x.p.Name.Contains(request.Keyword));
            
            // 3. sắp xếp theo DateCreated
            query = query.OrderByDescending(x => x.p.DateCreated); 

            // 4. phân trang
            int totalRow = await query.CountAsync();

            var data = await query.Skip((request.PageIndex - 1)*request.PageSize)
                    .Take(request.PageSize)
                    .Select(x => new ProductMainInfoManageVm()
                    {
                        Id = x.p.Id,
                        Name = x.p.Name,
                        OriginalPrice = x.p.OriginalPrice,
                        Price = x.p.Price,
                        DateCreated = x.p.DateCreated                        
                    }).ToListAsync();

            // lấy totalstock
            for(int i=0; i<data.Count; i++)
            {
                data[i].TotalStock =  (await GetProductDetails(data[i].Id)).Sum(d=>d.Stock);          
            }
            // 5. tạo paged result 
            var pagedResult = new PagedResult<ProductMainInfoManageVm>()
            {
                TotalRecords = totalRow,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Items = data
            };
            
            return new ApiResult<PagedResult<ProductMainInfoManageVm>>(true, ResultObj:pagedResult);
        }
        public async Task<ApiResult<ProductVm>> GetProductById(int productId)
        {
            var product = await _context.Products.Where(x=>x.Id==productId).FirstOrDefaultAsync();
            if(product == null || product.IsDeleted == true)
                return new ApiResult<ProductVm>(false, Message:$"Không tìm thấy sản phẩm có Id: {productId}");
            
            // tăng view count cho product
            product.ViewCount +=1;
            await _context.SaveChangesAsync();
            // lấy thông tin shop, category để hiển thị
            var shop = await _context.Shops.Where(x=>x.Id==product.ShopId).FirstOrDefaultAsync();
            var category = await _context.Categories.Where(x=>x.Id==product.CategoryId).FirstOrDefaultAsync();
            var productVm = new ProductVm()
            {
                Id = product.Id,                
                Name = product.Name,
                Description = product.Description,
                Gender = product.Gender,
                Price = product.Price,
                OriginalPrice = product.OriginalPrice,
                ViewCount = product.ViewCount,
                DateCreated = product.DateCreated,
                CategoryId =  product.CategoryId,
                CategoryName = category.Name,
                ShopId = shop.Id,
                ShopName = shop.Name,
                ShopDescription = shop.Description,
                IsDisableShop = shop.Disable,
                Details = await GetProductDetails(product.Id),
                Images = await GetProductImages(product.Id)
            };
            // tính tổng số lượng tồn kho 
            productVm.TotalStock = productVm.Details.Sum(d => d.Stock);

            return new ApiResult<ProductVm>(true, ResultObj:productVm);
        }

        public async Task<List<ProductDetailVm>> GetProductDetails(int productId)
        {
            var query = _context.ProductDetails
            .Where(pd => pd.ProductId == productId && pd.IsDeleted == false);

            var data = await query.Select(x => new ProductDetailVm()
            {
                Id = x.Id,
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
                SortOrder = x.SortOrder,
                ColorName = x.ColorName,
                IsSizeDetail = x.IsSizeDetail
            }).ToListAsync();

            return data;
        }

        // public async Task<ApiResult<int>> Create(int userId, CreateProductRequest request)
        // {
        //     // check valid properties request
        //     if(request.IsValid()==false)
        //         return new ApiResult<int>(false, Message:"Thông tin không hợp lệ, vui lòng nhập lại");
        //     // check categories hợp lệ
        //     var category = await _context.Categories.FirstOrDefaultAsync(x=>x.Id==request.CategoryId);
        //     if(category==null) 
        //         return new ApiResult<int>(false, Message:$"Không tồn tại category với Id: {request.CategoryId}");
                
        //     // 1. tạo list các product details
        //     var product_details = new List<ProductDetail>();
        //     if(request.Details==null || request.Details.Count==0)
        //         return new ApiResult<int>(false, Message:"Chi tiết sản phẩm không được phép để trống!");

        //     foreach(var productDetailVm in request.Details)
        //     {
        //         product_details.Add(new ProductDetail()
        //         {
        //             Color = productDetailVm.Color,
        //             Size = productDetailVm.Size,
        //             Stock = productDetailVm.Stock,
        //             IsDeleted = false
        //         });
        //     }

        //     // 2. tạo list các product images
        //     //var product_images = new List<ProductImage>();
        //     // cách 1:
        //     // if(request.Images==null || request.Images.Count==0)
        //     //     return new ApiResult<int>(false, Message:"Details must not null");
        //     // if(request.NewImages!=null)
        //     // {
        //     //     foreach(var image in request.NewImages)
        //     //     {
        //     //         product_images.Add(new ProductImage()
        //     //         {
        //     //             IsDefault = image.IsDefault,
        //     //             ColorName = image.ColorName,
        //     //             IsSizeDetail = image.IsSizeDetail,
        //     //             ImagePath =  await this.SaveFile(image.ImageFile)
        //     //         });            
        //     //     }
        //     // }
        //     // cách 2:
        //     // for(int i=0; i<request.ImageInfos.Count; i++)
        //     // {
        //     //     product_images.Add(new ProductImage()
        //     //         {
        //     //             IsDefault = request.ImageInfos[i].IsDefault,
        //     //             ColorName = request.ImageInfos[i].ColorName,
        //     //             IsSizeDetail = request.ImageInfos[i].IsSizeDetail,
        //     //             ImagePath =  await this.SaveFile(request.NewImages[i])
        //     //         });   
        //     // }

        //     // 3. tạo product mới
        //     // get shopId
        //     var shopId = (await _context.Users.FirstOrDefaultAsync(x=>x.Id==userId)).ShopId;
        //     if(shopId==null)  
        //         return new ApiResult<int>(false, Message:"Chỉ có tài khoản chủ shop mới được thực hiện hành động này");
        //     // thông tin mới 
        //     var product = new Product()
        //     {
        //         Name = request.Name,
        //         Description = request.Description,
        //         Gender = request.Gender,
        //         OriginalPrice = request.OriginalPrice,
        //         Price = request.Price,
        //         ViewCount = 0,
        //         DateCreated = DateTime.Now,
        //         CategoryId = request.CategoryId,
        //         ShopId = shopId.GetValueOrDefault(),
        //         IsDeleted = false,
        //         ProductDetails = product_details
        //         //ProductImages = product_images
        //     };
        //      _context.Products.Add(product);
        //     await _context.SaveChangesAsync();

        //     return new ApiResult<int>(true, product.Id);    // trả về Id của product mới
        // }

        public async Task<ApiResult<int>> Create(int userId, CreateProductRequest request)
        {
            // check valid properties request
            if(request.IsValid()==false)
                return new ApiResult<int>(false, Message:"Thông tin không hợp lệ, vui lòng nhập lại");
            // check categories hợp lệ
            var category = await _context.Categories.FirstOrDefaultAsync(x=>x.Id==request.CategoryId);
            if(category==null) 
                return new ApiResult<int>(false, Message:$"Không tồn tại category với Id: {request.CategoryId}");
                
            // 1. tạo list các product details
            var product_details = new List<ProductDetail>();
            if(request.Details==null || request.Details.Count==0)
                return new ApiResult<int>(false, Message:"Chi tiết sản phẩm không được phép để trống!");

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
            var product_images = new List<ProductImage>();
            //cách 1:
            if(request.NewImages==null || request.NewImages.Count==0)
                return new ApiResult<int>(false, Message:"NewImages không được phép để trống!");
            if(request.NewImages!=null)
            {
                foreach(var image in request.NewImages)
                {
                    product_images.Add(new ProductImage()
                    {
                        IsDefault = image.IsDefault,
                        ColorName = image.ColorName,
                        SortOrder = image.SortOrder,
                        IsSizeDetail = image.IsSizeDetail,
                        ImagePath =  await this.SaveFile(image.ImageFile)
                    });            
                }
            }
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
            // get shopId
            var shopId = (await _context.Users.FirstOrDefaultAsync(x=>x.Id==userId)).ShopId;
            if(shopId==null)  
                return new ApiResult<int>(false, Message:"Chỉ có tài khoản chủ shop mới được thực hiện hành động này");
            // thông tin mới 
            var product = new Product()
            {
                Name = request.Name,
                Description = request.Description,
                Gender = request.Gender,
                OriginalPrice = request.OriginalPrice,
                Price = request.Price,
                ViewCount = 0,
                DateCreated = DateTime.Now,
                CategoryId = request.CategoryId,
                ShopId = shopId.GetValueOrDefault(),
                IsDeleted = false,
                ProductDetails = product_details,
                ProductImages = product_images
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

            if(request.ImageFile==null)
                return new ApiResult<int>(false, Message:"Không có file ảnh nào được chọn");

            productImage.ImagePath = await this.SaveFile(request.ImageFile);
            _context.ProductImages.Add(productImage);

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

        

        public async Task<ApiResult<int>> Delete(int userId, int productId)
        {
            var product = await _context.Products.Where(x=>x.Id==productId).FirstOrDefaultAsync();
            if(product == null || product.IsDeleted == true)
                return new ApiResult<int>(false, Message:$"Không tìm thấy sản phẩm có Id: {productId}"); 

            // check có phải sản phẩm của chủ shop không
            var shopId = (await _context.Users.FirstOrDefaultAsync(x=>x.Id==userId)).ShopId;
            if(shopId==null)
                return new ApiResult<int>(false, Message:"Chỉ có tài khoản chủ shop mới được thực hiện hành động này");
            if(shopId!=product.ShopId)
                return new ApiResult<int>(false, Message:$"Shop của bạn không có sản phẩm với Id: {productId}");
                    
            // 1. xóa mềm product
            product.IsDeleted = true;
            _context.Products.Update(product);
            // 2. xóa các product details
            var product_details = await _context.ProductDetails.Where(x=>x.ProductId==productId).ToListAsync();
            foreach(var product_detail in product_details)
            {
                if(product_detail.IsDeleted == false)
                {
                    product_detail.IsDeleted = true;
                    _context.ProductDetails.Update(product_detail);
                }           
            }
            // 3. lưu thay đổi
            await _context.SaveChangesAsync();

            return new ApiResult<int>(true, Message:"Xóa sản phẩm thành công!"); 
        }

        public async Task<ApiResult<int>> Update(int userId, UpdateProductRequest request)
        {
            // check valid properties request
            if(request.IsValid()==false)
                return new ApiResult<int>(false, Message:"Thông tin không hợp lệ, vui lòng nhập lại");
            // check categories hợp lệ
            var category = await _context.Categories.FirstOrDefaultAsync(x=>x.Id==request.CategoryId);
            if(category==null) 
                return new ApiResult<int>(false, Message:$"Không tồn tại category với Id: {request.CategoryId}");
            // check list details trống
            if (request.Details == null || request.Details.Count == 0)
                return new ApiResult<int>(false, Message: "Chi tiết sản phẩm không được phép để trống!");

            // 1. update các thông tin của product
            var product = await _context.Products.Where(x=>x.Id==request.Id&&x.IsDeleted==false).FirstOrDefaultAsync();
            if(product==null) return new ApiResult<int>(false, Message:$"Không tìm thấy sản phẩm có Id: {request.Id}");
            // check có phải sản phẩm của chủ shop không
            var shopId = (await _context.Users.FirstOrDefaultAsync(x=>x.Id==userId)).ShopId;
            if(shopId==null)
                return new ApiResult<int>(false, Message:"Chỉ có tài khoản chủ shop mới được thực hiện hành động này");
            if(shopId!=product.ShopId)
                return new ApiResult<int>(false, Message:$"Shop của bạn không có sản phẩm với Id: {request.Id}");
            
            product.Name = request.Name;
            product.Description = request.Description;
            product.Gender = request.Gender;
            product.OriginalPrice = request.OriginalPrice;
            product.Price = request.Price;
            product.CategoryId = request.CategoryId;

            // 2. update list product details
            var current_details = _context.ProductDetails.Where(x=>x.ProductId==product.Id).ToList();
            // // duyệt qua list details của request, tìm update list, add list
            foreach(var dto in request.Details)
            {
                if(current_details.Any(x=>x.Color==dto.Color&&x.Size==dto.Size))
                {
                    // update stock, isDelete của product detail
                    var product_detail = _context.ProductDetails    
                        .Where(x=>x.ProductId==product.Id && x.Color==dto.Color && x.Size==dto.Size)
                        .FirstOrDefault();
                    product_detail.Stock = dto.Stock;
                    product_detail.IsDeleted = false;
                    _context.ProductDetails.Update(product_detail);                                           
                } 
                else
                {
                    // add product detail mới
                    var product_detail = new ProductDetail
                    {
                        ProductId = product.Id,
                        Color = dto.Color,
                        Size = dto.Size,
                        Stock = dto.Stock
                    };
                    _context.ProductDetails.Add(product_detail);
                }
            }
            // // duyệt qua list_current, tìm delete list
            foreach(var pd in current_details)
            {
                if(!request.Details.Any(x=>x.Color==pd.Color&&x.Size==pd.Size))
                {
                    var product_detail = _context.ProductDetails    
                    .Where(x=>x.ProductId==product.Id && x.Color==pd.Color && x.Size==pd.Size)
                    .FirstOrDefault();
                    // xóa mềm
                    product_detail.IsDeleted = true;
                    _context.ProductDetails.Update(product_detail);
                }
            }
            // 3. update các product images
            if(request.UpdateImages != null)
            {
                foreach (var image in request.UpdateImages)
                {
                    if(image.Id == 0)
                    {
                        // trường hợp thêm ảnh mới
                        if(image.ImageFile != null)
                        {
                            var new_image = new ProductImage
                            {
                                ProductId = request.Id,
                                IsDefault = image.IsDefault,
                                SortOrder = image.SortOrder,
                                ColorName = image.ColorName,
                                IsSizeDetail = false,
                                ImagePath = await this.SaveFile(image.ImageFile)
                            };
                            _context.ProductImages.Add(new_image);
                        }
                    }   
                    else
                    {
                        if(image.ImageFile != null)
                        {
                            // trường hợp sửa ảnh có sẵn
                            var _image = await _context.ProductImages.FirstOrDefaultAsync(x => x.Id == image.Id);
                            if (_image == null) return new ApiResult<int>(false, Message: $"Không tìm thấy image với Id: {image.Id}");
                            // xóa ảnh cũ trong image có sẵn
                            await _storageService.DeleteFileAsync(_image.ImagePath);
                            // update lại ảnh mới cho image có sẵn
                            _image.ImagePath = await this.SaveFile(image.ImageFile);
                        }
                    }    
                }
            }    
               
            // 4. lưu thay đổi
            await _context.SaveChangesAsync();

            return new ApiResult<int>(true, Message:"Cập nhật sản phẩm thành công!");
        }

        public async Task<ApiResult<int>> UpdateMainInfo(int userId, UpdateProductMainInfoRequest request)
        {
            // check valid properties request
            if(request.IsValid()==false)
                return new ApiResult<int>(false, Message:"Thông tin không hợp lệ, vui lòng nhập lại");
            // check categories hợp lệ
            var category = await _context.Categories.FirstOrDefaultAsync(x=>x.Id==request.CategoryId);
            if(category==null) 
                return new ApiResult<int>(false, Message:$"Không tồn tại category với Id: {request.CategoryId}");
            

            // 1. update các thông tin của product
            var product = await _context.Products.Where(x=>x.Id==request.Id&&x.IsDeleted==false).FirstOrDefaultAsync();
            if(product==null) return new ApiResult<int>(false, Message:$"Không tìm thấy sản phẩm có Id: {request.Id}");
            // check có phải sản phẩm của chủ shop không
            var shopId = (await _context.Users.FirstOrDefaultAsync(x=>x.Id==userId)).ShopId;
            if(shopId==null)
                return new ApiResult<int>(false, Message:"Chỉ có tài khoản chủ shop mới được thực hiện hành động này");
            if(shopId!=product.ShopId)
                return new ApiResult<int>(false, Message:$"Shop của bạn không có sản phẩm với Id: {request.Id}");
            
            product.Name = request.Name;
            product.Description = request.Description;
            product.Gender = request.Gender;
            product.OriginalPrice = request.OriginalPrice;
            product.Price = request.Price;
            product.CategoryId = request.CategoryId;

            // 2. lưu thay đổi
            await _context.SaveChangesAsync();

            return new ApiResult<int>(true, Message:"Cập nhật sản phẩm thành công!");
        }
        
        public async Task<ApiResult<int>> UpdateProductDetails(int userId, UpdateProductDetailsRequest request)
        {
            // check valid properties request
            if(request.IsValid()==false)
                return new ApiResult<int>(false, Message:"Thông tin không hợp lệ, vui lòng nhập lại");
            

            // 1. check các thông tin của product
            var product = await _context.Products.Where(x=>x.Id==request.Id&&x.IsDeleted==false).FirstOrDefaultAsync();
            if(product==null) return new ApiResult<int>(false, Message:$"Không tìm thấy sản phẩm có Id: {request.Id}");
            // check có phải sản phẩm của chủ shop không
            var shopId = (await _context.Users.FirstOrDefaultAsync(x=>x.Id==userId)).ShopId;
            if(shopId==null)
                return new ApiResult<int>(false, Message:"Chỉ có tài khoản chủ shop mới được thực hiện hành động này");
            if(shopId!=product.ShopId)
                return new ApiResult<int>(false, Message:$"Shop của bạn không có sản phẩm với Id: {request.Id}");
            

            // 2. update list product details
            var current_details = _context.ProductDetails.Where(x=>x.ProductId==product.Id).ToList();
            // // duyệt qua list details của request, tìm update list, add list
            foreach(var dto in request.Details)
            {
                if(current_details.Any(x=>x.Color==dto.Color&&x.Size==dto.Size))
                {
                    // update stock, isDelete của product detail
                    var product_detail = _context.ProductDetails    
                        .Where(x=>x.ProductId==product.Id && x.Color==dto.Color && x.Size==dto.Size)
                        .FirstOrDefault();
                    product_detail.Stock = dto.Stock;
                    product_detail.IsDeleted = false;
                    _context.ProductDetails.Update(product_detail);  
                                          
                } 
                else
                {
                    // add product detail mới
                    var product_detail = new ProductDetail
                    {
                        ProductId = product.Id,
                        Color = dto.Color,
                        Size = dto.Size,
                        Stock = dto.Stock
                    };
                    _context.ProductDetails.Add(product_detail);
                }
            }
            // // duyệt qua list_current, tìm delete list
            foreach(var pd in current_details)
            {
                if(!request.Details.Any(x=>x.Color==pd.Color&&x.Size==pd.Size))
                {
                    var product_detail = _context.ProductDetails    
                    .Where(x=>x.ProductId==product.Id && x.Color==pd.Color && x.Size==pd.Size)
                    .FirstOrDefault();
                    // xóa mềm
                    product_detail.IsDeleted = true;
                    _context.ProductDetails.Update(product_detail);
                }
            }
            // 3. lưu thay đổi
            await _context.SaveChangesAsync();

            return new ApiResult<int>(true, Message:"Cập nhật sản phẩm thành công!");
        }

        public async Task<ApiResult<int>> RemoveImage(int imageId)
        {
            var productImage = await _context.ProductImages.FindAsync(imageId);
            if (productImage == null)
                return new ApiResult<int>(false, Message:$"Không tìm thấy hình ảnh có Id: {imageId}");
            // xóa file vật lý
            await _storageService.DeleteFileAsync(productImage.ImagePath);
            // xóa record trong db
            _context.ProductImages.Remove(productImage);
            await _context.SaveChangesAsync();
            return new ApiResult<int>(true, Message:"Xóa hình ảnh thành công!");
        }

        public async Task<List<Function>> GetPermissions(int userId){
            var user =  await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if(user == null) return null;

            if (user.Disable == true) return new List<Function>{new Function() {ActionName = "Users.Get"}};

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
    }
}






