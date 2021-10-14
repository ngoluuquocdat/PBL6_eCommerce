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
using eShopSolution.ViewModels.Common;
using Microsoft.EntityFrameworkCore;

namespace eComSolution.Service.System.Users
{
    public class UserService : IUserService
    {
        private readonly EComDbContext _context;
        private readonly ITokenService _tokenService;

        public UserService(EComDbContext context, ITokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        public async Task<ApiResult<LoginRequest>> Login(LoginRequest request)
        {
            var user =  await _context.Users.FirstOrDefaultAsync(u => u.Username == request.Username);
            if(user == null) return new ApiResult<LoginRequest>(false);   // return Unauthorized("Invalid Username.");

            using var hmac = new HMACSHA512(user.PasswordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(request.Password));

            // so sánh 2 mảng byte: password hash từ request VS p   assword hash của user trong Db
            for(int i = 0; i<computedHash.Length; i++)
            {
                if(computedHash[i] != user.PasswordHash[i]) return null; //Unauthorized("Invalid Password.");
            }

            return new ApiResult<LoginRequest>(true, "Successed Login!");
        }

        public async Task<ApiResult<RegisterRequest>> Register(RegisterRequest request)
        {
           if(_context.Users.Any(u => u.Username == request.Username.ToLower()))
            {
                return new ApiResult<RegisterRequest>(false);
            }

            using var hmac = new HMACSHA512();

            var new_user = new User
            {
                Username = request.Username.ToLower(),
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(request.Password)),
                PasswordSalt = hmac.Key,
                Fullname = request.Fullname,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber
            };
            _context.Users.Add(new_user);
            await _context.SaveChangesAsync();

            return new ApiResult<RegisterRequest>(true, "Successed Register!");
        }
        public async Task<ApiResult<UserViewModel>> GetUserById(int UserId)
        {
            var user =  await _context.Users.FirstOrDefaultAsync(u => u.Id == UserId);
            if(user == null) return new ApiResult<UserViewModel>(false);

            var userViewModel = new UserViewModel 
            {
                Id = user.Id,
                Fullname = user.Fullname,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber
            };

            return new ApiResult<UserViewModel>(true, userViewModel);
        }



    }
}