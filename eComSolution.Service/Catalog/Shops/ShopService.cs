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
            if(user == null) return new ApiResult<ShopVm>(false, "User is not exist!");

            if(user.ShopId == null)
            {
                return new ApiResult<ShopVm>(false, "You haven't registered shop yet!");
            }
            else
            {
                var shop = await _context.Shops.FirstOrDefaultAsync(s => s.Id == user.ShopId);
                var shopInfo = new ShopVm{
                    Name = shop.Name,
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
            if(user == null) return new ApiResult<string>(false, "User is not exist!");

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
                    return new ApiResult<string>(true, Message: "Success create shop!");
                }
                else
                return new ApiResult<string>(false, Message: "Fail! Please try again.");

            }else{
                return new ApiResult<string>(false, Message: "You have registered a shop before!");
            }

        }

        public async Task<ApiResult<List<ShopVm>>> GetAll(){
            var query = await _context.Shops.ToArrayAsync();

            List<ShopVm> listShop = new List<ShopVm>();
            foreach (var shop in query){
                listShop.Add(new ShopVm {Name = shop.Name, Avatar = shop.Avatar, PhoneNumber = shop.PhoneNumber, 
                                        Address = shop.Address, Description = shop.Description, DateCreated = shop.DateCreated, Disable = shop.Disable});
            }
            if(listShop.Count > 0){
                return new ApiResult<List<ShopVm>>(true, listShop);
            }else{
                return new ApiResult<List<ShopVm>>(false, "Can't find any shop!");
            }

        }
        public async Task<ApiResult<string>> DisableShop(int shopId){
            var shop = await _context.Shops.FirstOrDefaultAsync(s => s.Id == shopId);
            if(shop == null) return new ApiResult<string>(false, "Shop is not exist!");
            
            shop.Disable = true;
            _context.Shops.Update(shop);

            if(await _context.SaveChangesAsync() > 0)
            return new ApiResult<string>(true, Message: "Disable shop successfully!");
            else
            return new ApiResult<string>(false, Message: "Fail! Please try again.");

        }
        public async Task<ApiResult<string>> EnableShop(int shopId){
            var shop = await _context.Shops.FirstOrDefaultAsync(s => s.Id == shopId);
            if(shop == null) return new ApiResult<string>(false, "Shop is not exist!");

            shop.Disable = false;
            _context.Shops.Update(shop);

            if(await _context.SaveChangesAsync() > 0)
            return new ApiResult<string>(true, Message: "Enable shop successfully!");
            else
            return new ApiResult<string>(false, Message: "Fail! Please try again.");

        }
        public async Task<ApiResult<string>> Update(int userId, CreateShopVm request){
            var user =  await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if(user == null) return new ApiResult<string>(false, "User is not exist!");

            if(user.ShopId == null){
                return new ApiResult<string>(false, "You haven't registered shop yet!");
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
                return new ApiResult<string>(true, Message: "Update shop successfully!");
                else
                return new ApiResult<string>(false, Message: "Fail! Please try again.");
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