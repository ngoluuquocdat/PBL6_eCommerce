using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using eComSolution.Data.EF;
using eComSolution.Data.Entities;
using eComSolution.Service.System.Token;
using eShopSolution.ViewModels.Common;
using Microsoft.EntityFrameworkCore;
using eComSolution.Service.Common;
using System.IO;
using eComSolution.Service.Catalog.Shops;
using eComSolution.ViewModel.Catalog.Shops;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http;
using System.Text.RegularExpressions;

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
                    ShopId = shop.Id,
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

            bool IsNull = (String.IsNullOrEmpty(request.Name) || String.IsNullOrEmpty(request.PhoneNumber) || request.ImageFile == null
                          || String.IsNullOrEmpty(request.Address) || String.IsNullOrEmpty(request.Description));

            if(IsNull) return new ApiResult<string>(false, "Dữ liệu đầu vào không được để trống!");

            var user =  await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if(user == null) return new ApiResult<string>(false, "Người dùng này không tồn tại trong hệ thống!");

            if(user.ShopId == null){
            
                if(!IsValid("", "", "",request.PhoneNumber)) // valid phone number
                {
                    return new ApiResult<string>(false, "Số điện thoại không hợp lệ. Vui lòng nhập lại!");
                }
                // check phone number is used
                var phone =  await _context.Shops.FirstOrDefaultAsync(sh => sh.PhoneNumber == request.PhoneNumber);
                if(phone != null) return new ApiResult<string>(false, "Số điện thoại này đã được sử dụng. Vui lòng thử với số điện thoại khác!");

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

        public async Task<ApiResult<List<ShopVm>>> GetAll(string name){
            var query = from sh in _context.Shops
                        join u in _context.Users on sh.Id equals u.ShopId
                        select new {sh, u};

            if(name != null)
            {
                query = query.Where(x => x.sh.Name.Contains(name));
            }

            var data = await query.Select(x => new ShopVm{
                ShopId = x.sh.Id,
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
        public async Task<ApiResult<string>> DisableShop(int shopId, string disable_reason){
            var shop = await _context.Shops.FirstOrDefaultAsync(s => s.Id == shopId);
            if(shop == null) return new ApiResult<string>(false, "Không tồn tại cửa hàng này!");
            
            shop.Disable = true;
            shop.DisableReason = disable_reason;
            shop.DateModified = DateTime.Now;
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
            shop.DisableReason = "";
            _context.Shops.Update(shop);

            if(await _context.SaveChangesAsync() > 0)
            return new ApiResult<string>(true, Message: "Tái kích hoạt cửa hàng thành công!");
            else
            return new ApiResult<string>(false, Message: "Đã xảy ra lỗi. Vui lòng thử lại!");

        }
        public async Task<ApiResult<string>> Update(int userId, CreateShopVm request){
            var user =  await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if(user == null) return new ApiResult<string>(false, "Người dùng này không tồn tại trong hệ thống!");

            if(user.ShopId == null){
                return new ApiResult<string>(false, "Bạn chưa đăng ký cửa hàng nào. Vui lòng tạo mới một cửa hàng vào thử lại!");
            }else{

                bool IsNull = (String.IsNullOrEmpty(request.Name) || String.IsNullOrEmpty(request.PhoneNumber) || request.ImageFile == null
                          || String.IsNullOrEmpty(request.Address) || String.IsNullOrEmpty(request.Description));

                if(IsNull) return new ApiResult<string>(false, "Dữ liệu đầu vào không được để trống!");
            
                if(!IsValid("", "", "",request.PhoneNumber)) // valid phone number
                {
                    return new ApiResult<string>(false, "Số điện thoại không hợp lệ. Vui lòng nhập lại!");
                }
                // check phone number is used
                var phone =  await _context.Shops.FirstOrDefaultAsync(sh => sh.PhoneNumber == request.PhoneNumber &&  sh.Id != user.ShopId);
                if(phone != null) return new ApiResult<string>(false, "Số điện thoại này đã được sử dụng. Vui lòng thử với số điện thoại khác!");
                
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
        public async Task<ApiResult<ShopVm>> GetByShopId(int shopId)
        {
            var shop =  await _context.Shops.FirstOrDefaultAsync(sh => sh.Id == shopId);
            if(shop == null) return new ApiResult<ShopVm>(false, "Shop này không tồn tại trong hệ thống!");

            var user = await _context.Users.FirstOrDefaultAsync(x => x.ShopId == shopId);
 
            var shopInfo = new ShopVm{
                ShopId = shop.Id,
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
        public  bool IsValid(string username, string password, string email, string phonenumber) 
        {
            if(!String.IsNullOrEmpty(username)) return Regex.Match(username, @"^(?=.{8,}$)(?![_.])(?!.*[_.]{2})[a-zA-Z0-9._]+(?<![_.])$").Success;
            if(!String.IsNullOrEmpty(password)) return Regex.Match(password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$").Success;
            if(!String.IsNullOrEmpty(email)) return Regex.Match(email, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$").Success; 
            if(!String.IsNullOrEmpty(phonenumber)) return Regex.Match(phonenumber, @"^([\+]?61[-]?|[0])?[1-9][0-9]{8}$").Success;

            return false;
        }
    }
    
}