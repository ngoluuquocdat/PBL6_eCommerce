using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using eComSolution.Data.EF;
using eComSolution.Data.Entities;
using eComSolution.Service.System.Token;
using eComSolution.ViewModel.System.Users;
using EmailValidation;
using eShopSolution.ViewModels.Common;
using Microsoft.EntityFrameworkCore;
using eComSolution.Service.Common;
using System.IO;
using eComSolution.Service.Catalog.Shops;
using eComSolution.ViewModel.Catalog.Shops;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http;

namespace eComSolution.Service.System.Users
{
    public class ShopService : IShopService
    {
        private readonly EComDbContext _context;
        private readonly ITokenService _tokenService;
        private readonly IStorageService _storageService;

        public ShopService(EComDbContext context, ITokenService tokenService, IStorageService storage)
        {
            _context = context;
            _tokenService = tokenService;
            _storageService = storage;
        }
        public async Task<ApiResult<ShopVm>> Get(int userId){
            var user =  await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if(user == null) return new ApiResult<ShopVm>(false, "Người dùng này không tồn tại trong hệ thống!");

            if(user.ShopId == null)
            {
                return new ApiResult<ShopVm>(false, "Bạn chưa đăng ký cửa hàng nào. Vui lòng tạo mới một cửa hàng vào thử lại!");
            }
            else
            {
                var shop = await _context.Shops.FirstOrDefaultAsync(s => s.Id == user.ShopId);
                var shopInfo = new ShopVm{
                    NameOfShop = shop.Name,
                    NameOfUser = user.Fullname,
                    Avatar = "/storage/"+ shop.Avatar,
                    PhoneNumber = shop.PhoneNumber,
                    Address = shop.Address,
                    Description = shop.Description,
                    DateCreated = shop.DateCreated,
                    DateModified = shop.DateModified
                };
                return new ApiResult<ShopVm>(true, shopInfo);
            }

        }
        public async Task<ApiResult<string>> Create(int userId, CreateShopVm request){
            var user =  await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if(user == null) return new ApiResult<string>(false, "Người dùng này không tồn tại trong hệ thống!");

            if(user.ShopId == null){
                Shop shop = new Shop{
                    Name = request.Name,
                    Avatar = await this.SaveFile(request.ImageFile),
                    PhoneNumber = request.PhoneNumber,
                    Address = request.Address,
                    Description = request.Description,
                    DateCreated = DateTime.Now,
                    DateModified = DateTime.Now
                };  

                _context.Shops.Add(shop);
                if(await _context.SaveChangesAsync() > 0)
                {
                    user.ShopId = shop.Id;
                
                    GroupUser gu = new GroupUser{
                        GroupId = 2,
                        UserId = userId
                    };
                    _context.GroupUsers.Add(gu);

                    _context.Users.Update(user);
                    await _context.SaveChangesAsync();
                    return new ApiResult<string>(true, Message: "Tạo mới cửa hàng thành công!");
                }
                else
                return new ApiResult<string>(false, Message: "Đã xảy ra lỗi. Vui lòng thử lại!");

            }else{
                return new ApiResult<string>(false, Message: "Bạn đã đăng ký 1 cửa hàng trước đó!");
            }
        }

        public async Task<ApiResult<List<ShopVm>>> GetAll(){
            var query = from sh in _context.Shops
                        join u in _context.Users on sh.Id equals u.ShopId
                        select new {sh, u};

            var data = await query.Select(x => new ShopVm{
                NameOfShop = x.sh.Name,
                NameOfUser = x.u.Fullname,
                Avatar = x.sh.Avatar,
                PhoneNumber = x.sh.PhoneNumber, 
                Address = x.sh.Address,
                Description = x.sh.Description,
                DateCreated = x.sh.DateCreated,
                DateModified = x.sh.DateModified,
                Disable = x.sh.Disable
            }).ToListAsync();

            if(data.Count > 0){
                return new ApiResult<List<ShopVm>>(true, data);
            }else{
                return new ApiResult<List<ShopVm>>(false, "Chưa có cửa hàng nào được tạo!");
            }

        }
        public async Task<ApiResult<string>> DisableShop(int shopId){
            var shop = await _context.Shops.FirstOrDefaultAsync(s => s.Id == shopId);
            if(shop == null) return new ApiResult<string>(false, "Không tồn tại cửa hàng này!");
            
            shop.Disable = true;
            // shop.DisableReason = "";
            _context.Shops.Update(shop);

            if(await _context.SaveChangesAsync() > 0)
            return new ApiResult<string>(true, Message: "Đã vô hiệu hóa cửa hàng này!");
            else
            return new ApiResult<string>(false, Message: "Đã xảy ra lỗi. Vui lòng thử lại!");

        }
        public async Task<ApiResult<string>> EnableShop(int shopId){
            var shop = await _context.Shops.FirstOrDefaultAsync(s => s.Id == shopId);
            if(shop == null) return new ApiResult<string>(false, "Không tồn tại cửa hàng này!");

            shop.Disable = false;
            _context.Shops.Update(shop);

            if(await _context.SaveChangesAsync() > 0)
            return new ApiResult<string>(true, Message: "Bỏ vô hiệu hóa cửa hàng thành công!");
            else
            return new ApiResult<string>(false, Message: "Đã xảy ra lỗi. Vui lòng thử lại!");

        }
        public async Task<ApiResult<string>> Update(int userId, CreateShopVm request){
            var user =  await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if(user == null) return new ApiResult<string>(false, "Người dùng này không tồn tại trong hệ thống!");

            if(user.ShopId == null){
                return new ApiResult<string>(false, "Bạn chưa đăng ký cửa hàng nào. Vui lòng tạo mới một cửa hàng vào thử lại!");
            }else{
                var shop = await _context.Shops.FirstOrDefaultAsync(s => s.Id == user.ShopId);

                shop.Name = request.Name;
                shop.Avatar =  await this.SaveFile(request.ImageFile);
                shop.PhoneNumber = request.PhoneNumber;
                shop.Address = request.Address;
                shop.Description = request.Description;
                shop.DateModified = DateTime.Now;

                _context.Shops.Update(shop);

                if(await _context.SaveChangesAsync() > 0)
                return new ApiResult<string>(true, Message: "Cập nhật thông tin thành công!");
                else
                return new ApiResult<string>(false, Message: "Đã xảy ra lỗi. Vui lòng thử lại!");
            }
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